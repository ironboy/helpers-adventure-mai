// Example Npc: has its own menu, remembers what you've said to it.

class Guard : Npc
{
    public bool Bribed;

    public override string Name => "Guard";

    public override string[] Description => [
        Bribed
            ? "The guard pretends not to see you."
            : "\"Halt! Nobody goes through the eastern door.\""
    ];

    public override string[] Actions => Bribed
        ? ["Wink:Wink"]
        : ["Ask about the door:AskAboutDoor",
           "Offer a coin:Bribe"];

    public void AskAboutDoor()
    {
        Console.WriteLine("\"Locked. Has been for years. Move along.\"");
        Console.ReadLine();
    }

    public void Bribe()
    {
        Bribed = true;
        Console.WriteLine("The coin vanishes into a pocket. \"Door? What door?\"");
        Console.ReadLine();
        Menu.Close();   // the conversation is over – back to the room
    }

    public void Wink()
    {
        Console.WriteLine("The guard winks back.");
        Console.ReadLine();
    }
}
