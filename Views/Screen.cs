namespace DragoSharp.Views;

/// <summary>
/// Low-level terminal output helpers.
/// Everything that writes to Console lives here.
/// </summary>
public static class Screen
{
    /// <summary>Writes text to the terminal without a trailing newline.</summary>
    public static void Write(string text) => Console.Write(text);

    /// <summary>Writes a line to the terminal.</summary>
    public static void WriteLine(string text) => Console.WriteLine(text);

    /// <summary>Reads a line of input from the terminal.</summary>
    public static string? ReadLine() => Console.ReadLine();

    /// <summary>
    /// Clears the terminal (works on most platforms / terminals).
    /// </summary>
    public static void Clear()
    {
        Console.Write("\x1b[H\x1b[2J");
        Console.Out.Flush();
    }

    /// <summary>
    /// Prints text one character at a time, giving a typewriter effect.
    /// </summary>
    /// <param name="text">The text to print.</param>
    /// <param name="msPerChar">Milliseconds between each character (default 10).</param>
    public static void Typed(string text, int msPerChar = 10)
    {
        try
        {
            foreach (char c in text)
            {
                Write(c.ToString());
                Thread.Sleep(msPerChar);
            }
        }
        catch (ThreadInterruptedException)
        {
            // If the thread is interrupted, print the rest immediately.
            BlankLine();
            WriteLine(text);
            return;
        }

        BlankLine();
    }

    /// <summary>Prints a blank line.</summary>
    public static void BlankLine() => Console.WriteLine();

    /// <summary>Waits for the user to press Enter.</summary>
    public static void AwaitUser()
    {
        WriteLine("<Press Enter to Continue>");
        ReadLine();
    }
}
