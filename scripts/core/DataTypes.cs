public interface IIdentifiable
{
    string Id { get; }
}

public record SceneData(
    string Id,
    string Name,
    string ArtFile,
    string Description,
    string AmbientText,
    float EncounterChance,
    List<string> PossibleEncounters,
    List<ExitData> Exits,
    List<EventData> Events
) : IIdentifiable;

public record ExitData(
    string Id,
    string Label,
    string DestinationSceneId,
    float EncounterChance,
    string RequiresFlag
);

public record EventData(
    string Id,
    string Label,
    bool OneTime,
    string RequiresFlag,
    string SetsFlag,
    List<string> Dialogue,
    RewardData Reward
);

public record RewardData(
    int Xp,
    int Gold,
    string ItemId
);

public record MonsterStats(
    int MaxHp,
    int Attack,
    int Defense,
    int Speed,
    int CritChance,
    int Evasion
);

public record LootData(
    int XpReward,
    int GoldMin,
    int GoldMax,
    List<ItemDropData> ItemDrops
);

public record ItemDropData(
    string ItemId,
    float DropChance
);

public record MonsterData(
    string Id,
    string Name,
    string ArtFile,
    string Description,
    List<string> SpawnScenes,
    MonsterStats Stats,
    List<string> AttackIds,
    LootData Loot,
    bool IsBoss,
    string DefeatText,
    string FleeText
) : IIdentifiable;
