// Example location that checks the player's inventory (filled by Courtyard).

class Gate : Location
{
    private bool _unlocked;

    public override string Name => "The gate";

    public override string[] Description => [
        "A heavy iron gate blocks the way out.",
        _unlocked ? "It stands open. Freedom!" : "It is locked."
    ];

    public override string[] Actions => _unlocked
        ? ["Walk through:WalkThrough"]
        : Player.Has("rusty key")
            ? ["Try the rusty key:Unlock"]
            : ["Rattle the gate:Rattle"];

    public void Rattle()
    {
        Console.WriteLine("It doesn't budge. Maybe there's a key somewhere?");
        Console.ReadLine();
    }

    public void Unlock()
    {
        _unlocked = true;
        Console.WriteLine("The key turns with a screech. The gate swings open.");
        Console.ReadLine();
    }

    public void WalkThrough()
    {
        Console.WriteLine("You step out into the sunlight. THE END.");
        Console.ReadLine();
        Menu.Close();   // back to the main menu
    }
}
