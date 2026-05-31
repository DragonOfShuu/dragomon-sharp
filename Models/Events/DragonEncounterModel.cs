using DragoSharp.Models.Utils;

namespace DragoSharp.Models.Events;

/// <summary>
/// Pure battle / catch mathematics for dragon encounters.
/// Returns results as plain booleans — no I/O.
/// </summary>
public static class DragonEncounterModel
{
    /// <summary>
    /// Determines the outcome of a direct battle.
    /// The player wins when their active dragon's level is >= the wild dragon's.
    /// </summary>
    public static bool EncounterBattle(Dragons.Dragon wildDragon, Player.Player player)
    {
        return wildDragon.Level <= (player.ActiveDragon?.Level ?? 0);
    }

    /// <summary>
    /// Determines whether a catch attempt succeeds.
    /// A higher wild-dragon level (relative to player level) means harder to catch.
    /// </summary>
    public static bool CalculateCatch(Dragons.Dragon dragon, Player.Player player)
    {
        int catchRate = (dragon.Level - (player.GetLevel() * 5)) / 4;

        int catchChance = catchRate > 0
            ? Statics.GenRNum(1, catchRate + 1)
            : 1;

        return catchChance == 1;
    }
}
