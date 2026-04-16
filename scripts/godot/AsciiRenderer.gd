extends Node
class_name AsciiRenderer

const COLOR_ENEMY   = "#E74C3C"
const COLOR_PLAYER  = "#3498DB"
const COLOR_GOLD    = "#F39C12"
const COLOR_HEAL    = "#2ECC71"
const COLOR_NEUTRAL = "#BDC3C7"
const COLOR_HEADER  = "#FFFFFF"

func load_art(scene_id: String) -> String:
	var path = "res://art/scenes/%s.txt" % scene_id
	if not FileAccess.file_exists(path):
		return "[color=%s][ART NOT FOUND: %s][/color]" % [COLOR_ENEMY, scene_id]
	var file = FileAccess.open(path, FileAccess.READ)
	return file.get_as_text()