/* ==========================================================================
 *  HELPER CLASS – base class for characters the player can talk to.
 *  Nothing magic here: an Npc is just an Interactive with its own menu.
 *
 *  How to use: inherit from Npc, override Name/Description/Actions
 *  (see Npcs/Guard.cs), and let a Location call npc.Run() from one of
 *  its methods (see TalkToGuard() in Locations/RuneRoom.cs).
 * ========================================================================== */

abstract class Npc : Interactive
{
    protected override string ExitLabel => "Stop talking";
}
