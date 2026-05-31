namespace DragoSharp.Views;

/// <summary>
/// Prompts the user with questions and returns typed, validated answers.
/// All Console interaction is centralised here.
/// </summary>
public static class Question
{
    private static readonly string[] TrueAnswers =
        { "yes", "indeed", "mhm", "true", "t", "y", "yep", "uh-huh" };

    private static readonly string[] FalseAnswers =
        { "no", "not at all", "nah", "false", "f", "n", "nope", "nuh-uh" };

    // ── Primitives ────────────────────────────────────────────────────────────

    /// <summary>Prints a prompt and returns the raw user input.</summary>
    public static string RequestString(string prompt)
    {
        Console.WriteLine(prompt);
        Console.Write("> ");
        return Console.ReadLine() ?? "";
    }

    /// <summary>
    /// Prints a prompt plus allowed answers, loops until the user picks one.
    /// </summary>
    public static string RequestString(string prompt, params string[] allowed)
    {
        var lower = allowed.Select(a => a.Trim().ToLowerInvariant()).ToArray();
        string answerList = string.Join("\n", allowed.Select(a => $" - {a}"));

        while (true)
        {
            string answer = RequestString($"{prompt}\n{answerList}").Trim().ToLowerInvariant();
            if (lower.Contains(answer)) return answer;
            Console.WriteLine("That was not an allowed answer.");
        }
    }

    /// <summary>Prompts repeatedly until the user enters a valid integer.</summary>
    public static int RequestInt(string prompt)
    {
        while (true)
        {
            string raw = RequestString(prompt);
            if (int.TryParse(raw, out int value)) return value;
            Console.WriteLine("Your answer was not an integer.");
        }
    }

    /// <summary>Prompts repeatedly until the user enters a valid float.</summary>
    public static float RequestFloat(string prompt)
    {
        while (true)
        {
            string raw = RequestString(prompt);
            if (float.TryParse(raw, out float value)) return value;
            Console.WriteLine("Your answer was not a float.");
        }
    }

    /// <summary>
    /// Converts a natural-language yes/no answer into a bool.
    /// Accepts many synonyms (yes/y/yep/indeed…, no/n/nope/nah…).
    /// </summary>
    public static bool RequestBoolean(string prompt)
    {
        while (true)
        {
            string answer = RequestString(prompt).Trim().ToLowerInvariant();
            if (TrueAnswers.Contains(answer)) return true;
            if (FalseAnswers.Contains(answer)) return false;
            Console.WriteLine("Your answer was not a valid yes or no.");
        }
    }

    // ── Choice menus ──────────────────────────────────────────────────────────

    /// <summary>
    /// Displays a numbered menu and returns the zero-based index the user selects.
    /// Lines that are exactly "-" are printed as a blank separator and not counted.
    /// </summary>
    public static int ChooseItem(string prompt, params string[] options)
    {
        while (true)
        {
            PrintIndexedMenu(options);
            int answer = RequestInt(prompt);
            if (answer >= 0 && answer < options.Length) return answer;
            Console.WriteLine("Your answer was not within the range of options.");
        }
    }

    /// <summary>
    /// Displays a numbered menu and returns the object associated with the
    /// chosen index.  <paramref name="options"/> and <paramref name="values"/>
    /// must have the same length.
    /// </summary>
    public static T ChooseItem<T>(string prompt, string[] options, T[] values)
    {
        if (options.Length != values.Length)
            throw new ArgumentException("options and values must be the same length.");

        while (true)
        {
            PrintIndexedMenu(options);
            int answer = RequestInt(prompt);
            if (answer >= 0 && answer < options.Length) return values[answer];
            Console.WriteLine("Your answer was not within the range of options.");
        }
    }

    // ── Helpers ───────────────────────────────────────────────────────────────

    private static void PrintIndexedMenu(string[] options)
    {
        for (int i = 0; i < options.Length; i++)
        {
            if (options[i] == "-")
                Screen.BlankLine();          // separator — prints blank, still occupies index i
            else
                Console.WriteLine($"[{i}] {options[i]}");
        }
    }
}
