using DragoSharp.Models.Elements;

namespace DragoSharp.Models;

/// <summary>
/// Base class for anything that exists in the world and has health.
/// Pure model — no I/O.
/// </summary>
public abstract class Entity
{
    public float Health { get; protected set; }
    public float MaxHealth { get; protected set; }
    public ElementalState? Element { get; protected set; }

    protected Entity(float startingHealth, float maxHealth = 100, ElementType? startingElement = null)
    {
        Health = startingHealth;
        MaxHealth = maxHealth;
        Element = startingElement.HasValue ? new ElementalState(0, startingElement.Value) : null;
    }

}
