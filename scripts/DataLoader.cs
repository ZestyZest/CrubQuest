using Godot;
using System.Text.Json;

public partial class DataLoader : Godot.Node
{
	private static readonly JsonSerializerOptions Options = new()
	{
		PropertyNameCaseInsensitive = true
	};

	private static SceneData? _townSquare;
	private static MonsterData? _goblin;

	public override void _Ready() => LoadAll();

	private void LoadAll()
	{
		_townSquare = LoadFile<SceneData>("res://data/scenes/town_square.json");
		_goblin     = LoadFile<MonsterData>("res://data/monsters/goblin.json");

		// Phase 1 verification — print to Godot Output panel
		GD.Print("=== DataLoader: Phase 1 Load ===");
		GD.Print($"Scene loaded:   {_townSquare?.Name} (id: {_townSquare?.Id})");
		GD.Print($"Monster loaded: {_goblin?.Name} (id: {_goblin?.Id}, HP: {_goblin?.Stats.MaxHp})");
	}

	private static T? LoadFile<T>(string godotPath)
	{
		if (!Godot.FileAccess.FileExists(godotPath))
		{
			GD.PrintErr($"DataLoader: file not found — {godotPath}");
			return default;
		}
		using var file = Godot.FileAccess.Open(godotPath, Godot.FileAccess.ModeFlags.Read);
		return JsonSerializer.Deserialize<T>(file.GetAsText(), Options);
	}

	public static SceneData GetScene(string id)
	{
		// Phase 1: only town_square is loaded
		if (id == "town_square" && _townSquare != null) return _townSquare;
		throw new KeyNotFoundException($"Scene not found: {id}");
	}

	public static MonsterData GetMonster(string id)
	{
		if (id == "goblin" && _goblin != null) return _goblin;
		throw new KeyNotFoundException($"Monster not found: {id}");
	}
}
