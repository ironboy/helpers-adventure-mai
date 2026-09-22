/* ==========================================================================
 *  HELPER CLASS – base class for every room/place in the game.
 *  You are NOT expected to understand the movement code below.
 *
 *  How to use: inherit from Location and override Name, Description
 *  and Actions (see Locations/RuneRoom.cs). That's it.
 *
 *  Exits ("Go north" etc.) are added to the menu automatically based on
 *  where the location sits in the map in World/Game – you don't write them.
 * ========================================================================== */

abstract class Location : Interactive
{
    // Filled in by World when the map is built
    internal World World { get; set; } = null!;
    internal int X { get; set; }
    internal int Y { get; set; }

    protected override string ExitLabel => "Quit to main menu";

    internal override List<string> AllActions()
    {
        var lines = base.AllActions();
        if (World.At(X, Y - 1) != null) lines.Add("Go north:North");
        if (World.At(X + 1, Y) != null) lines.Add("Go east:East");
        if (World.At(X, Y + 1) != null) lines.Add("Go south:South");
        if (World.At(X - 1, Y) != null) lines.Add("Go west:West");

        // Always show "Show inventory" at all locations
        lines.Add("Show inventory:ShowInventory");

        return lines;
    }

    // These are the methods the exit lines above refer to
    public virtual void North() => Move(0, -1);
    public virtual void East() => Move(1, 0);
    public virtual void South() => Move(0, 1);
    public virtual void West() => Move(-1, 0);

    public void ShowInventory()
    {
        Console.Clear();
        Console.WriteLine("INVENTORY");
        if (Player.Inventory.Count == 0)
        {
            Console.WriteLine("You don't own anything in this world!");
        }
        else
        {
            Console.WriteLine(String.Join("\n", Player.Inventory));
        }
        Console.ReadLine();
    }

    private void Move(int dx, int dy)
    {
        World.MoveTo(X + dx, Y + dy);
        Menu.Close();   // leave this location's menu so the new one can be shown
    }
}
