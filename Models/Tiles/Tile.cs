using DragoSharp.Models.Utils;

namespace DragoSharp.Models.Tiles;

/// <summary>
/// Abstract base for every tile on the world map.
/// Activation returns a TileResult so the controller/view handle all output.
/// Pure model — no I/O.
/// </summary>
public abstract class Tile
{
    public bool Explored { get; set; } = false;

    /// <summary>
    /// Tile-specific logic called when the player steps on this tile.
    /// Returns all information the view/controller needs; never calls Console.
    /// </summary>
    public abstract TileResult Activate(Player.Player player);

    /// <summary>
    /// Public entry point: calls Activate and marks the tile as explored.
    /// </summary>
    public TileResult Activation(Player.Player player)
    {
        var result = Activate(player);
        Explored = true;
        return result;
    }

    // ── Dragon generation helpers ─────────────────────────────────────────────

    /// <summary>
    /// Attempts to generate a dragon.
    /// A 1-in-<paramref name="outOfChance"/> roll returns null (no spawn).
    /// </summary>
    protected Dragons.Dragon? GenDragon(int outOfChance, params Dragons.Dragon[] pool)
    {
        int roll = Statics.GenRNum(0, outOfChance);
        if (roll == 0) return null;
        return Statics.PickRItem(pool);
    }

    /// <summary>Default 50 % encounter rate.</summary>
    protected Dragons.Dragon? GenDragon(params Dragons.Dragon[] pool)
        => GenDragon(2, pool);

    // ── Identity ──────────────────────────────────────────────────────────────

    /// <summary>Single-character map symbol for this tile.</summary>
    public abstract char Repr { get; }

    /// <summary>Human-readable tile name.</summary>
    public abstract string Name { get; }
}
