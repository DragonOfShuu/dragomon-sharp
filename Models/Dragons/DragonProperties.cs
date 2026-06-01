namespace DragoSharp.Models.Dragons;

/// <summary>
/// Builder class for creating dragon property configurations.
/// Uses the builder pattern to construct attack patterns and metadata for dragons.
/// Instances are registered into a collection via the AddTo() method.
/// </summary>
public class DragonProperties
{
    private string? _nameValue;
    private string? _descriptionValue;
    private List<AttackPattern> _attacksValue = [];
    private int _chanceValue = 1;

    /// <summary>
    /// Sets the name of the dragon species.
    /// </summary>
    public DragonProperties Name(string name)
    {
        _nameValue = name;
        return this;
    }

    /// <summary>
    /// Sets the description of the dragon species.
    /// </summary>
    public DragonProperties Description(string description)
    {
        _descriptionValue = description;
        return this;
    }

    /// <summary>
    /// Sets the list of possible attack patterns for this dragon.
    /// </summary>
    public DragonProperties Attacks(List<AttackPattern> attacks)
    {
        _attacksValue = attacks;
        return this;
    }

    /// <summary>
    /// Sets the relative chance/weight for this dragon to be selected when spawning randomly.
    /// Higher values increase the likelihood of selection.
    /// </summary>
    public DragonProperties Chance(int chance)
    {
        _chanceValue = chance;
        return this;
    }

    /// <summary>
    /// Adds this dragon properties to the provided collection.
    /// This should be called at the end of the builder chain.
    /// </summary>
    public DragonProperties AddTo(List<DragonProperties> collection)
    {
        collection.Add(this);
        return this;
    }

    // Public properties for accessing built values
    public string SpeciesName => _nameValue ?? "Unknown";
    public string SpeciesDescription => _descriptionValue ?? "A mysterious dragon.";
    public IReadOnlyList<AttackPattern> PossibleAttacks => _attacksValue.AsReadOnly();
    public int SelectionChance => _chanceValue;
}

