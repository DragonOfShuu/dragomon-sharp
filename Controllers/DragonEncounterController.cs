using DragoSharp.Models.Events;
using DragoSharp.Models.Player;
using DragoSharp.Views;

namespace DragoSharp.Controllers;

/// <summary>
/// Orchestrates a single dragon encounter.
/// Gets user input via the view layer, applies outcomes via the model layer.
/// </summary>
public static class DragonEncounterController
{
    /// <summary>
    /// Runs a complete dragon encounter interaction (fight / catch / run).
    /// Mutates <paramref name="player"/> as appropriate (dragons added/removed, XP gained).
    /// </summary>
    public static void HandleEncounter(Models.Dragons.Dragon wildDragon, Player player)
    {
        Screen.Typed($"You found a {wildDragon}!");
        Screen.Typed($"You sent out {player.ActiveDragon}!");

        // Subscribe to the level-up event while the encounter is active
        player.OnLevelUp += OnPlayerLevelUp;

        try
        {
            int action = Question.ChooseItem(
                "Choose an action:",
                "Fight!", "Catch!", "Run!");

            switch (action)
            {
                case 0: Fight(wildDragon, player); break;
                case 1: Catch(wildDragon, player); break;
                default:
                    Screen.Typed("You ran away!"); break;
            }
        }
        finally
        {
            player.OnLevelUp -= OnPlayerLevelUp;
        }
    }

    // ── Actions ───────────────────────────────────────────────────────────────

    private static void Fight(Models.Dragons.Dragon wild, Player player)
    {
        bool won = DragonEncounterModel.EncounterBattle(wild, player);

        if (won)
        {
            Screen.Typed("You won the battle!");
            bool wantCatch = Question.RequestBoolean("Would you like to catch the wild Dragon?");

            if (wantCatch)
            {
                Screen.Typed("You caught the dragon!");
                player.AddDragon(wild);
                player.AddXP(1);
            }
            else
            {
                Screen.Typed("You left the poor thing to die. :(");
            }
        }
        else
        {
            Screen.Typed("You lost the battle, and your Dragon died!");
            if (player.ActiveDragon != null)
                player.RemoveDragon(player.ActiveDragon);
        }
    }

    private static void Catch(Models.Dragons.Dragon wild, Player player)
    {
        bool caught = DragonEncounterModel.CalculateCatch(wild, player);

        if (caught)
        {
            Screen.Typed("You caught the dragon!");
            player.AddDragon(wild);
            player.AddXP(1);
        }
        else
        {
            Screen.Typed("You failed to catch the dragon :(");
        }
    }

    // ── Event handlers ────────────────────────────────────────────────────────

    private static void OnPlayerLevelUp(int newLevel)
    {
        Screen.Typed($"You leveled up! Your new player level is: {newLevel}");
    }
}
