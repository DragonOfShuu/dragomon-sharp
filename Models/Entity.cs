namespace DragoSharp.Models;

/// <summary>
/// Base class for anything that exists in the world and has health.
/// Pure model — no I/O.
/// </summary>
public abstract class Entity
{
    public float Health { get; protected set; }
    public Elements.Element? Element { get; protected set; }

    protected Entity(float startingHealth)
    {
        Health = startingHealth;
    }
}
