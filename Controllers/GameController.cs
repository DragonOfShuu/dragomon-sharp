using DragoSharp.Models;
using DragoSharp.Views;

namespace DragoSharp.Controllers;

/// <summary>
/// Drives the main game loop: reads the player's action from the view,
/// delegates movement / tile activation to the model, and hands
/// encounter results to DragonEncounterController.
/// </summary>
public class GameController
{
    private readonly Game _game;

    public GameController(Game game)
    {
        _game = game;

        // Subscribe once so level-ups are always displayed during normal play
        _game.World.Player.OnLevelUp += newLevel =>
            Screen.Typed($"You leveled up! Your new player level is: {newLevel}");
    }

    // ── Main loop ─────────────────────────────────────────────────────────────

    /// <summary>
    /// Runs one iteration of the main loop.
    /// </summary>
    /// <returns>
    /// <c>true</c> if the game should continue, <c>false</c> if the player chose to exit.
    /// </returns>
    public bool MainLoop()
    {
        Screen.Clear();
        GameView.PrintStatistics(_game.World);

        int action = Question.ChooseItem(
            "What action do you want to take?",
            "Move North",
            "Move East",
            "Move West",
            "Move South",
            "-",              // index 4 — secret easter egg
            "Open Backpack",
            "Open Map",
            "Open Settings",
            "Close Game");

        try
        {
            return HandleAction(action);
        }
        catch (IndexOutOfRangeException)
        {
            // Player walked off the map
            Screen.Typed(
                "You peer over a sudden cliff down towards an infinite void. " +
                "Your brain cannot comprehend its sheer vastness.");
            Screen.Typed(
                "You suddenly awake, about three feet away from the cliff. " +
                "You do not know how much time has passed since you peered over its edge, " +
                "but you get the feeling you should not do it again.");
            return true;
        }
    }

    // ── Action dispatch ───────────────────────────────────────────────────────

    private bool HandleAction(int action)
    {
        var world = _game.World;

        switch (action)
        {
            // Movement — (0,0) is bottom-left; north = +y, east = +x
            case 0: HandleMove(0,  1); break;   // North
            case 1: HandleMove(1,  0); break;   // East
            case 2: HandleMove(-1, 0); break;   // West
            case 3: HandleMove(0, -1); break;   // South

            case 4:
                Screen.Typed("Congrats! You found an easter egg! Now move along...");
                break;

            case 5:
                OpenBackpack();
                break;

            case 6:
                OpenMap();
                break;

            case 7:
                // Settings — placeholder for future expansion
                break;

            case 8:
                return false;
        }

        return true;
    }

    // ── Movement ──────────────────────────────────────────────────────────────

    private void HandleMove(int dx, int dy)
    {
        var result = _game.World.OffsetPlayer(dx, dy);

        // Display tile narrative messages
        foreach (var msg in result.Messages)
            Screen.Typed(msg);

        // If there is an encounter chance, pause then handle it
        if (result.HasEncounterChance)
        {
            Screen.AwaitUser();

            if (result.Dragon != null)
            {
                DragonEncounterController.HandleEncounter(result.Dragon, _game.World.Player);
            }
            else
            {
                Screen.Typed("You found nothing. :(");
            }
        }
    }

    // ── Backpack ──────────────────────────────────────────────────────────────

    private void OpenBackpack()
    {
        var dragons = _game.World.Player.Backpack.Dragons;

        if (dragons.Count == 0)
        {
            Screen.Typed("Your backpack is empty.");
            return;
        }

        var result = EditUI.Run("Choose a dragon:", dragons.Cast<Common.IEditableItem>());

        // EditUI handles renames, favourites, etc. internally.
        // A "selected" result here has no special game action for dragons.
        _ = result;
    }

    // ── Map ───────────────────────────────────────────────────────────────────

    private void OpenMap()
    {
        var world  = _game.World;
        var player = world.Player;

        Screen.Clear();
        Console.WriteLine("=== World Map (radius 8) ===");
        GameView.RenderMiniMap(world, player, radius: 8, hideUnexplored: true);
        Screen.AwaitUser();
    }
}
