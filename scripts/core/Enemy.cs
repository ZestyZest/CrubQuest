public partial class Enemy : Entity
{
    public string Id { get; set; } = "";
    public string Description { get; set; } = "";
    public string ArtFile { get; set; } = "";
    public List<string> AttackIds { get; set; } = new();
    public int XPReward { get; set; }
    public int GoldMin { get; set; }
    public int GoldMax { get; set; }
}