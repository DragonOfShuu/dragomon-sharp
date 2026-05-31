using DragoSharp.Models;
using DragoSharp.Models.Persistence;
using DragoSharp.Views;

namespace DragoSharp.Controllers;

/// <summary>
/// Top-level application controller.
/// Handles the start-up sequence: logo, intro, save selection, and the
/// main game loop.  Ties together all other controllers.
/// </summary>
public class AppController
{
    // ── Entry point ───────────────────────────────────────────────────────────

    public void Run()
    {
        Screen.Clear();

        try { RunLogo(); }
        catch { /* swallow Ctrl-C during logo */ }

        Save save = DetermineSave();
        RunGame(save);
    }

    // ── Save selection ────────────────────────────────────────────────────────

    private Save DetermineSave()
    {
        Save[] saves = Save.LoadAll();

        if (saves.Length == 0)
            return new Save(SetupNewGame(playIntro: true));

        while (true)
        {
            var result = EditUI.Run("Please choose a save:", saves);

            if (result.Reason == EditUI.ExitReason.ItemSelected && result.SelectedItem is Save chosen)
                return chosen.UpdateDate();

            if (result.Reason == EditUI.ExitReason.CreatedNew || result.Reason == EditUI.ExitReason.UserExited)
            {
                bool playIntro = Question.RequestBoolean("Run the intro?");
                return new Save(SetupNewGame(playIntro));
            }
        }
    }

    // ── New-game setup ────────────────────────────────────────────────────────

    private Game SetupNewGame(bool playIntro)
    {
        string playerName;

        if (playIntro)
        {
            try { playerName = RunIntroduction(); }
            catch { playerName = Question.RequestString("Give your name:"); }
        }
        else
        {
            playerName = Question.RequestString("Give your name:");
        }

        // Choose world size
        int size = Question.ChooseItem(
            "What would you like the size of your world to be?",
            new[] { "16x16", "32x32", "64x64" },
            new[] { 16, 32, 64 });

        var player = new Models.Player.Player(playerName, size / 2, size / 2);
        var world = new World(size, size, player);
        world.Generate();

        return new Game(world);
    }

    // ── Game loop ─────────────────────────────────────────────────────────────

    private void RunGame(Save save)
    {
        var controller = new GameController(save.Game);

        bool running = true;
        while (running)
        {
            running = controller.MainLoop();
            save.SaveGame();

            if (running)
                Screen.AwaitUser();
        }

        Screen.Typed("Goodbye!");
    }

    // ── Logo ──────────────────────────────────────────────────────────────────

    private void RunLogo()
    {
        string[] logoLines =
        {
            "██████╗ ██████╗  █████╗  ██████╗  ██████╗ ███████╗██╗  ██╗ █████╗ ██████╗ ██████╗ ",
            "██╔══██╗██╔══██╗██╔══██╗██╔════╝ ██╔═══██╗██╔════╝██║  ██║██╔══██╗██╔══██╗██╔══██╗",
            "██║  ██║██████╔╝███████║██║  ███╗██║   ██║███████╗███████║███████║██████╔╝██████╔╝",
            "██║  ██║██╔══██╗██╔══██║██║   ██║██║   ██║╚════██║██╔══██║██╔══██║██╔══██╗██╔═══╝ ",
            "██████╔╝██║  ██║██║  ██║╚██████╔╝╚██████╔╝███████║██║  ██║██║  ██║██║  ██║██║     ",
            "╚═════╝ ╚═╝  ╚═╝╚═╝  ╚═╝ ╚═════╝  ╚═════╝ ╚══════╝╚═╝  ╚═╝╚═╝  ╚═╝╚═╝  ╚═╝╚═╝     "
        };

        foreach (string line in logoLines)
        {
            Screen.WriteLine(line);
            Thread.Sleep(400);
        }

        Screen.WriteLine("                         Press Enter to Continue...");
        Screen.ReadLine();
        Screen.Clear();
    }

    // ── Introduction ──────────────────────────────────────────────────────────

    private string RunIntroduction()
    {
        Screen.Clear();

        Screen.Typed("First, there was nothing.");
        Screen.Typed("Then, there was something.");
        Screen.Typed("That something was you, a Dragon Master, destined for greatness.");
        Screen.Typed("Even though you are eternally 11 years old, and have no dad, you are destined for greatness.");
        Screen.Typed(
            "Your mom is supportive, but she just stands awkwardly in the kitchen " +
            "for the whole game and that's literally her entire purpose.");
        Screen.Typed("One day though, you received a mysterious letter from The Professor.");
        Screen.Typed("It read:");
        Screen.BlankLine();
        Screen.Typed("Dear Dragon Master,");
        Screen.BlankLine();
        Screen.Typed("I am Professor Tree, and I study dragons.");
        Screen.Typed("The Plot requires that you have a goal, so I have entrusted you with this Dragodex.");
        Screen.Typed("Catch 'Em All™, and you will be the greatest Dragon Master of all time.");
        Screen.BlankLine();
        Screen.Typed("Sincerely,");
        Screen.Typed("Professor Tree");
        Screen.BlankLine();
        Screen.Typed("Also inside the package was a Dragon and a Dragodex.");
        Screen.Typed("You named the Dragon 'Shuckle.'");
        Screen.Typed("You named the Dragodex 'Dragodex.'");
        Screen.Typed("You named yourself...");
        Screen.BlankLine();
        Screen.Typed("...wait, what's your name again?");

        string playerName = Screen.ReadLine() ?? "Unknown";

        Screen.BlankLine();
        Screen.Typed($"Oh yeah, it's {playerName}.");
        Screen.Typed($"Well, {playerName}, you're ready to start your journey.");

        Screen.AwaitUser();
        Screen.Clear();

        return playerName;
    }
}
