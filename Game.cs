// The game itself: the main menu and the map.
// Add new locations by creating a class in Locations/ and placing it in the map.

class Game : Interactive
{
    public override string Name => "Helpers' Adventure";

    public override string[] Description => ["A tiny text adventure."];

    public override string[] Actions => [
        "Start game:Start",
        "Help:Help"
    ];

    protected override string ExitLabel => "Quit";

    public void Start()
    {
        // Everything is created fresh, so a new game starts from scratch:
        // new locations (their state is reset) and an empty inventory.
        Player.Inventory.Clear();

        // Row 0 is north, column 0 is west. null = nothing there.
        World world = new([
            [new RuneRoom(), new Hallway()],
            [null,           new Courtyard()],
            [null,           new Gate()],
        ]);

        world.Play();
    }

    public void Help()
    {
        Console.WriteLine("Pick a number and press Enter. 0 always takes you back.");
        Console.ReadLine();
    }
}
