// Example location: shows state that changes the description,
// and how to hand over to an Npc's menu.

class RuneRoom : Location
{
    private readonly Guard _guard = new();
    private bool _runesRead;

    public override string Name => "Rune room";

    public override string[] Description => [
        "A cold stone room. Strange runes cover the far wall.",
        _runesRead ? "You have already read the runes." : "The runes glow faintly.",
        "A guard stands by the eastern door."
    ];

    public override string[] Actions => [
        "Read the runes:ReadRunes",
        "Talk to the guard:TalkToGuard"
    ];

    public void ReadRunes()
    {
        Console.WriteLine(_runesRead
            ? "Nothing new. Still just runes."
            : "\"THE COURTYARD HIDES A KEY\" – the glow fades.");
        _runesRead = true;
        Console.ReadLine();
    }

    // An item that opens ANOTHER object's menu: just call its Run()
    public void TalkToGuard() => _guard.Run();
}
