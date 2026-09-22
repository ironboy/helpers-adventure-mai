// The player. There is only one, so everything here is static:
// any location or npc can write Player.Inventory or Player.Has("...").

static class Player
{
    public static List<string> Inventory { get; } = [];

    public static bool Has(string item) => Inventory.Contains(item);
}
