extends Node
class_name AsciiRenderer

const COLOR_NEUTRAL = "#BDC3C7"
const COLOR_HEADER  = "#FFFFFF"

func load_art(scene_id: String) -> String:
	var path = "res://art/scenes/%s.txt" % scene_id
	if not FileAccess.file_exists(path):
		return "[color=#E74C3C][ART NOT FOUND: %s][/color]" % scene_id
	var file = FileAccess.open(path, FileAccess.READ)
	return file.get_as_text()