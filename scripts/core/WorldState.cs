using Godot;

[GlobalClass]
public partial class WorldState : RefCounted
{
	private readonly List<string> _visitedSceneIds = new();
	private readonly List<string> _completedEventIds = new();
	private readonly Dictionary<string, bool> _flags = new();

	public bool HasVisited(string sceneId) =>
		_visitedSceneIds.Contains(sceneId);

	public void VisitScene(string sceneId)
	{
		if (!HasVisited(sceneId)) _visitedSceneIds.Add(sceneId);
	}

	public bool HasCompletedEvent(string eventId) =>
		_completedEventIds.Contains(eventId);

	public void CompleteEvent(string eventId)
	{
		if (!HasCompletedEvent(eventId)) _completedEventIds.Add(eventId);
	}

	public bool HasFlag(string flag) =>
		_flags.GetValueOrDefault(flag, false);

	public void SetFlag(string flag, bool value = true) =>
		_flags[flag] = value;
}
