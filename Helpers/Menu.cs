/* ==========================================================================
 *  HELPER CLASS – you are NOT expected to understand the code in this file.
 *  (It uses reflection and delegates, which we haven't covered yet.)
 *
 *  You only need to know how to USE it:
 *
 *    Menu.Create([
 *        "MAIN MENU",                 // first line = title
 *        "Start game:Start",          // "Text:MethodName" = calls this.MethodName()
 *        "SETTINGS",                  // a line without ":" = a submenu
 *        "-Sound on/off:ToggleSound", // leading "-" = one level deeper
 *        "-Difficulty:SetDifficulty"
 *    ], this).Run();
 *
 *  Rules:
 *    - The method must be public, take no parameters and return nothing.
 *    - The method must exist on the object you pass in (usually "this").
 *    - Want an item that opens some OTHER object's menu? Write a method
 *      that calls that object's Run(): see TalkToGuard() in RuneRoom.cs.
 *    - Menu.Close() inside a method closes the menu the player is in
 *      (otherwise the menu shows again after the method has finished).
 *
 *  Normally you don't call Menu.Create yourself at all – Interactive.Run()
 *  does it for you based on your Actions property.
 * ========================================================================== */

class Menu(string title, Action? action = null)
{
    public string Title { get; } = title;
    private readonly List<Menu> _children = [];

    private static bool _closeRequested;

    /// <summary>Closes the innermost open menu once the current action has finished.</summary>
    public static void Close() => _closeRequested = true;

    public static Menu Create(string[] lines, object instance)
    {
        var root = new Menu(lines[0]);
        var stack = new Stack<Menu>();
        stack.Push(root);

        foreach (var raw in lines.Skip(1))
        {
            int depth = raw.TakeWhile(c => c == '-').Count();
            var parts = raw[depth..].Split(':', 2);
            var title = parts[0].Trim();

            // Pop back to the correct parent for this line
            while (stack.Count > depth + 1) stack.Pop();

            if (parts.Length == 2)
            {
                // Look up the method on the instance by name
                var name = parts[1].Trim();
                Action action;
                try
                {
                    action = (Action)Delegate.CreateDelegate(typeof(Action), instance, name);
                }
                catch (ArgumentException)
                {
                    throw new ArgumentException(
                        $"Menu item \"{title}\" refers to {instance.GetType().Name}.{name}(), " +
                        "but no such public method (no parameters, returns void) exists.");
                }
                stack.Peek()._children.Add(new Menu(title, action));
            }
            else
            {
                var sub = new Menu(title);
                stack.Peek()._children.Add(sub);
                stack.Push(sub);
            }
        }
        return root;
    }

    /// <summary>Shows the menu until the player picks 0 or an action calls Menu.Close().</summary>
    public void Run(string exitLabel = "Back")
    {
        if (action != null)
        {
            action();
            return;
        }
        while (RunOnce(exitLabel)) { }
    }

    /// <summary>
    /// Shows the menu once and runs the chosen item.
    /// Returns false when the player wants to leave this menu.
    /// </summary>
    internal bool RunOnce(string exitLabel)
    {
        Console.Clear();
        Console.WriteLine(Title);
        for (int i = 0; i < _children.Count; i++)
            Console.WriteLine($"{i + 1}. {_children[i].Title}");
        Console.WriteLine($"0. {exitLabel}");
        Console.Write("> ");

        if (!int.TryParse(Console.ReadLine(), out int c) || c == 0) return false;
        if (c >= 1 && c <= _children.Count) _children[c - 1].Run();

        if (_closeRequested)
        {
            _closeRequested = false;
            return false;
        }
        return true;
    }
}
