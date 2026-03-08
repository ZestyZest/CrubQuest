extends Node

# The Player C# object — all GDScript accesses player state through this
var player: Player

func _ready() -> void:
	player = Player.new()
	player.Name = "Hero"

	print("GameManager ready. Player: ", player.Name)
