using Godot;
using System.Text.Json;

public partial class DataLoader : Node
{
	private static readonly JsonSerializerOptions Options = new()
	{
		PropertyNameCaseInsensitive = true
	};

	private static readonly Dictionary<string, SceneData> _scenes = new();
	private static readonly Dictionary<string, MonsterData> _monsters = new();

	public override void _Ready() => LoadAll();

	private void LoadAll()
	{
		LoadDirectory<SceneData>("res://data/scenes/", _scenes);
		LoadDirectory<MonsterData>("res://data/monsters/", _monsters);

		GD.Print("=== DataLoader: Phase 2 Load ===");
		GD.Print($"Scenes loaded:   {_scenes.Count}");
		GD.Print($"Monsters loaded: {_monsters.Count}");
	}

	private static void LoadDirectory<T>(string dirPath, Dictionary<string, T> cache)
		where T : IIdentifiable
	{
		using var dir = DirAccess.Open(dirPath);
		if (dir == null)
		{
			GD.PrintErr($"DataLoader: directory not found — {dirPath}");
			return;
		}

		dir.ListDirBegin();
		string fileName;
		while ((fileName = dir.GetNext()) != "")
		{
			if (!fileName.EndsWith(".json")) continue;
			var fullPath = dirPath + fileName;
			using var file = Godot.FileAccess.Open(fullPath, Godot.FileAccess.ModeFlags.Read);
			var data = JsonSerializer.Deserialize<T>(file.GetAsText(), Options);
			if (data != null) cache[data.Id] = data;
		}
		dir.ListDirEnd();
	}

	public static SceneData GetScene(string id) => _scenes[id];
	public static MonsterData GetMonster(string id) => _monsters[id];
	public static bool SceneExists(string id) => _scenes.ContainsKey(id);

	public static List<MonsterData> GetMonstersForScene(string sceneId) =>
		_monsters.Values.Where(m => m.SpawnScenes.Contains(sceneId)).ToList();
}
