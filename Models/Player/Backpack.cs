namespace DragoSharp.Models.Player;

/// <summary>
/// The player's collection of dragons.
/// Pure model — no I/O.
/// </summary>
public class Backpack : List<Dragons.Dragon>
{
    /// <summary>Returns only the dragons in the backpack (filter-safe accessor).</summary>
    public IReadOnlyList<Dragons.Dragon> Dragons => this.AsReadOnly();
}
