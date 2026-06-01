using DragoSharp.Models.Elements;

namespace DragoSharp.Models.Dragons;

/// <summary>
/// Builder class for creating attack pattern configurations.
/// Uses the builder pattern to construct attacks with name, description, damage, infliction, and chance.
/// </summary>
public class AttackPattern
{
    private string? _name;
    private string? _description;
    private int _attackDmg;
    private ElementalInfliction? _elementalInfliction;
    private int _chance = 1;

    /// <summary>
    /// Sets the name of the attack.
    /// </summary>
    public AttackPattern Name(string name)
    {
        _name = name;
        return this;
    }

    /// <summary>
    /// Sets the description of the attack.
    /// </summary>
    public AttackPattern Description(string description)
    {
        _description = description;
        return this;
    }

    /// <summary>
    /// Sets the damage value of this attack.
    /// </summary>
    public AttackPattern Damage(int dmg)
    {
        _attackDmg = dmg;
        return this;
    }

    /// <summary>
    /// Sets the optional elemental infliction for this attack.
    /// </summary>
    public AttackPattern Infliction(ElementalInfliction? infliction)
    {
        _elementalInfliction = infliction;
        return this;
    }

    /// <summary>
    /// Sets the relative chance/weight for this attack to be selected.
    /// Higher values increase the likelihood of selection.
    /// </summary>
    public AttackPattern Chance(int chance)
    {
        _chance = chance;
        return this;
    }

    // Public properties for accessing built values
    public string AttackName => _name ?? "Unknown Attack";
    public string AttackDescription => _description ?? "A mysterious attack.";
    public int AttackDmg => _attackDmg;
    public ElementalInfliction? ElementalInfliction => _elementalInfliction;
    public int SelectionChance => _chance;
}
