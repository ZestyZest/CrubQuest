extends Node

var player: Player
var world_state: WorldState
var pending_combat_monster_id: String = ""

func _ready() -> void:
	player = Player.new()
	player.Name = "Hero"
	world_state = WorldState.new()

	print("GameManager ready. Player: ", player.Name)

	# Boot into the player's starting scene
	SceneManager.go_to_scene(player.CurrentSceneId)
