extends Control

@onready var art_label: RichTextLabel = %ArtLabel
@onready var scene_name_label: RichTextLabel = %SceneNameLabel
@onready var description_label: RichTextLabel = %DescriptionLabel
@onready var choices_container: VBoxContainer = %ChoicesContainer

var _ascii_renderer := AsciiRenderer.new()
var _scene_data: Dictionary = {}

func _ready() -> void:
	_load_scene(GameManager.player.CurrentSceneId)

# ── Data Loading ─────────────────────────────────────────────────────────────

func _load_scene(scene_id: String) -> void:
	var path = "res://data/scenes/%s.json" % scene_id
	var file = FileAccess.open(path, FileAccess.READ)
	if file == null:
		push_error("ExplorationUI: scene file not found — " + path)
		return

	_scene_data = JSON.parse_string(file.get_as_text())

	art_label.text = _ascii_renderer.load_art(scene_id)
	scene_name_label.text = "[b][color=%s]%s[/color][/b]" % [AsciiRenderer.COLOR_HEADER, _scene_data["name"]]
	description_label.text = "[color=%s]%s[/color]" % [AsciiRenderer.COLOR_NEUTRAL, _scene_data["description"]]

	_populate_choices()

# ── Choice Buttons ────────────────────────────────────────────────────────────

func _populate_choices() -> void:
	for child in choices_container.get_children():
		child.queue_free()

	for exit in _scene_data.get("exits", []):
		var required_flag: String = exit.get("requires_flag", "")
		if required_flag != "" and not GameManager.world_state.HasFlag(required_flag):
			continue
		_add_button(exit["label"], _on_exit_pressed.bind(exit))

	for event in _scene_data.get("events", []):
		if event.get("one_time", false) and GameManager.world_state.HasCompletedEvent(event["id"]):
			continue
		var required_flag: String = event.get("requires_flag", "")
		if required_flag != "" and not GameManager.world_state.HasFlag(required_flag):
			continue
		_add_button(event["label"], _on_event_pressed.bind(event))

func _add_button(label: String, callback: Callable) -> void:
	var btn := Button.new()
	btn.text = label
	btn.alignment = HORIZONTAL_ALIGNMENT_LEFT
	btn.pressed.connect(callback)
	choices_container.add_child(btn)

# ── Choice Handlers ───────────────────────────────────────────────────────────

func _on_exit_pressed(exit: Dictionary) -> void:
	var destination: String = exit["destination_scene_id"]
	var encounter_chance: float = exit.get("encounter_chance", 0.0)
	var possible_encounters: Array = _scene_data.get("possible_encounters", [])

	if encounter_chance > 0.0 and possible_encounters.size() > 0 and randf() < encounter_chance:
		var monster_id: String = possible_encounters[randi() % possible_encounters.size()]
		SceneManager.start_combat(monster_id)
		return

	SceneManager.go_to_scene(destination)

func _on_event_pressed(event: Dictionary) -> void:
	# Show dialogue in the description area
	var lines: Array = event.get("dialogue", [])
	if lines.size() > 0:
		var joined := "\n\n".join(lines)
		description_label.text = "[color=%s]%s[/color]" % [AsciiRenderer.COLOR_NEUTRAL, joined]

	# Apply reward
	var reward: Dictionary = event.get("reward", {})
	GameManager.player.CurrentXP += int(reward.get("xp", 0))
	GameManager.player.Gold     += int(reward.get("gold", 0))

	# Mark complete
	if event.get("one_time", false):
		GameManager.world_state.CompleteEvent(event["id"])

	# Set flag
	var sets_flag: String = event.get("sets_flag", "")
	if sets_flag != "":
		GameManager.world_state.SetFlag(sets_flag, true)

	# Refresh buttons (hides this event if one_time)
	_populate_choices()
