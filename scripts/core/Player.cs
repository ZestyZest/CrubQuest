[Godot.GlobalClass]
public partial class Player : Entity
{
	public int Level { get; set; } = 1;
	public int CurrentXP { get; set; } = 0;
	public int XPToNextLevel => Level * 100;
	public int Gold { get; set; } = 0;
	public int Energy { get; set; } = 10;
	public int MaxEnergy { get; set; } = 10;
	public string Class { get; set; } = "Warrior";
	public List<string> Inventory { get; set; } = new();
	public List<string> UnlockedComboIds { get; set; } = new();
	public string CurrentSceneId { get; set; } = "town_square";
}
