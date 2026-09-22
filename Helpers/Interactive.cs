/* ==========================================================================
 *  HELPER CLASS – the base class for everything that has a menu:
 *  the Game itself, every Location and every Npc.
 *
 *  It is short, so feel free to read it, but you don't have to.
 *  What you need to know is which members you can OVERRIDE in your subclass:
 *
 *    public override string   Name        => "Rune room";
 *    public override string[] Description => ["Line one.", "Line two."];
 *    public override string[] Actions     => ["Look around:LookAround", ...];
 *
 *  ...and that Run() shows the description + menu and calls your methods.
 *  Actions uses the same string format as Menu (see Menu.cs).
 * ========================================================================== */

abstract class Interactive
{
    /// <summary>Shown as the menu title. Defaults to the class name.</summary>
    public virtual string Name => GetType().Name;

    /// <summary>Printed above the menu, one string per line.</summary>
    public virtual string[] Description => [];

    /// <summary>Menu lines in "Text:MethodName" format (see Menu.cs).</summary>
    public virtual string[] Actions => [];

    /// <summary>Text for the "0." choice that leaves this menu.</summary>
    protected virtual string ExitLabel => "Back";

    /// <summary>Lets subclasses (e.g. Location) add lines after Actions.</summary>
    internal virtual List<string> AllActions() => [.. Actions];

    /// <summary>Shows the description and the menu until the player leaves.</summary>
    public void Run()
    {
        // The menu is rebuilt after every action, so changes to
        // Description or Actions made by an action show up immediately.
        while (true)
        {
            var header = string.Join("\n", [Name.ToUpper(), "", .. Description, ""]);
            var menu = Menu.Create([header, .. AllActions()], this);
            if (!menu.RunOnce(ExitLabel)) return;
        }
    }
}
