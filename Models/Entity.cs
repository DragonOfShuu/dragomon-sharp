using DragoSharp.Models.Elements;

namespace DragoSharp.Models;

/// <summary>
/// Base class for anything that exists in the world and has health.
/// Pure model — no I/O.
/// </summary>
public abstract class Entity(float startingHealth, float maxHealth = 100, ElementType? startingElement = null)
{
    public float Health { get; protected set; } = startingHealth;
    public float MaxHealth { get; protected set; } = maxHealth;
    public ElementalState? Element { get; protected set; } = startingElement.HasValue ? new ElementalState(0, startingElement.Value) : null;
}
