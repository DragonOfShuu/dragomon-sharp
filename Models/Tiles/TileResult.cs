namespace DragoSharp.Models.Tiles;

/// <summary>
/// The result of a tile's Activation.
/// Carries narrative messages (to be displayed by the view) and
/// an optional dragon to encounter (handled by the controller).
/// Pure data — no I/O.
/// </summary>
public sealed class TileResult
{
    /// <summary>
    /// Narrative messages to display sequentially.
    /// These are shown before the encounter (or as the full tile story for
    /// non-encounter tiles like Home).
    /// </summary>
    public string[] Messages { get; }

    /// <summary>
    /// The dragon to encounter, or null if there is none.
    /// When non-null the controller initiates a dragon encounter.
    /// When null and HasEncounterChance is true the controller shows "nothing found".
    /// </summary>
    public Dragons.Dragon? Dragon { get; }

    /// <summary>
    /// True when this tile type normally spawns encounters.
    /// Lets the controller distinguish "no dragon spawned this visit" from
    /// "this tile never has encounters" (e.g. Home).
    /// </summary>
    public bool HasEncounterChance { get; }

    public TileResult(string[] messages, Dragons.Dragon? dragon = null, bool hasEncounterChance = false)
    {
        Messages = messages;
        Dragon = dragon;
        HasEncounterChance = hasEncounterChance;
    }

    /// <summary>Convenience constructor for a single-message, no-encounter result.</summary>
    public TileResult(string message)
        : this(new[] { message }) { }
}
