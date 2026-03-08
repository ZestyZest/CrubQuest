public abstract class Entity
{
    public string Name { get; set; } = "";
    public int MaxHP { get; set; }
    public int CurrentHP { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public int Speed { get; set; }
    public int CritChance { get; set; }
    public int Evasion { get; set; }

    public bool IsAlive => CurrentHP > 0;
}