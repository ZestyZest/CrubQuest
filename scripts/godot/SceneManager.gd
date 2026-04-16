extends Node

signal scene_changed(scene_id: String)
signal combat_started(monster_id: String)
signal combat_ended(victory: bool)

func go_to_scene(scene_id: String) -> void:
	GameManager.player.CurrentSceneId = scene_id
	GameManager.world_state.VisitScene(scene_id)
	get_tree().change_scene_to_file("res://scenes/ExplorationScene.tscn")
	scene_changed.emit(scene_id)

func start_combat(monster_id: String) -> void:
	GameManager.pending_combat_monster_id = monster_id
	# CombatScene.tscn is built in Phase 3 — log for now
	push_warning("SceneManager: combat triggered with %s (CombatScene not yet implemented)" % monster_id)
	combat_started.emit(monster_id)

func end_combat(victory: bool) -> void:
	combat_ended.emit(victory)
	go_to_scene(GameManager.player.CurrentSceneId)
