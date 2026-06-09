using DragoSharp.Models.Elements;

namespace DragoSharp.Models.Dragons;

/// <summary>
/// Builder class for creating attack pattern configurations.
/// Uses the builder pattern to construct attacks with name, description, damage, infliction, and chance.
/// </summary>
public class AttackPattern
{
    public string AttackName { get; private set; } = "Unknown Attack";
    public string AttackDescription { get; private set; } = "A mysterious attack.";
    public int AttackDmg { get; private set; }
    public ElementalInfliction? ElementalInfliction { get; private set; }
    public int SelectionChance { get; private set; } = 1;

    /// <summary>
    /// Sets the name of the attack.
    /// </summary>
    public AttackPattern WithName(string name)
    {
        AttackName = name;
        return this;
    }

    /// <summary>
    /// Sets the description of the attack.
    /// </summary>
    public AttackPattern WithDescription(string description)
    {
        AttackDescription = description;
        return this;
    }

    /// <summary>
    /// Sets the damage value of this attack.
    /// </summary>
    public AttackPattern WithDamage(int dmg)
    {
        AttackDmg = dmg;
        return this;
    }

    /// <summary>
    /// Sets the optional elemental infliction for this attack.
    /// </summary>
    public AttackPattern WithInfliction(ElementalInfliction? infliction)
    {
        ElementalInfliction = infliction;
        return this;
    }

    /// <summary>
    /// Sets the relative chance/weight for this attack to be selected.
    /// Higher values increase the likelihood of selection.
    /// </summary>
    public AttackPattern WithChance(int chance)
    {
        SelectionChance = chance;
        return this;
    }
}
