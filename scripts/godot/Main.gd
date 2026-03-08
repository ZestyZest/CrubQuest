extends Control

@onready var art_label: RichTextLabel = $ArtLabel
@onready var ascii_renderer: AsciiRenderer = AsciiRenderer.new()

func _ready() -> void:
	# Load and display the town square art
	var art = ascii_renderer.load_art("town_square")
	art_label.text = art
