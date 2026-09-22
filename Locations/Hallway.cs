// The simplest possible location: just a description. Exits are automatic.

class Hallway : Location
{
    public override string Name => "Hallway";

    public override string[] Description => [
        "A long, echoing hallway.",
        "Torches flicker along the walls."
    ];
}
