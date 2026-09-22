// Example location with a submenu (the "-" lines) and an item that disappears.
// The key goes into Player.Inventory so other locations can check for it.

class Courtyard : Location
{
    private bool _fountainSearched;

    public override string Name => "Courtyard";

    public override string[] Description => [
        "An overgrown courtyard under a grey sky.",
        _fountainSearched ? "The fountain is empty." : "Something glints in the fountain."
    ];

    public override string[] Actions => _fountainSearched
        ? ["Look around:LookAround"]
        : ["Look around:LookAround",
           "Search the fountain",
           "-Reach in:TakeKey",
           "-Leave it:LeaveIt"];

    public void LookAround()
    {
        Console.WriteLine("Weeds, a dry fountain, and a locked gate to the south.");
        Console.ReadLine();
    }

    public void TakeKey()
    {
        _fountainSearched = true;
        Player.Inventory.Add("rusty key");
        Console.WriteLine("You pull out a rusty key!");
        Console.ReadLine();
        Menu.Close();   // close the submenu – the Actions list has changed
    }

    public void LeaveIt()
    {
        Console.WriteLine("You leave it. For now.");
        Console.ReadLine();
    }
}
