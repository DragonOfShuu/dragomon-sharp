namespace DragoSharp.Models.Elements;

/// <summary>
/// Represents an elemental state on an Entity.
/// Pure data / logic — no I/O.
/// </summary>
public class Element
{
    public enum ElementType
    {
        Pyro, Hydro, Cryo,
        Dendro, Geo,
        Anemo, Electro,
        // Composite reactions
        Swirl, Crystallise, Overload, Superconduct,
        Electrocharge, Melt, Vaporise, Freeze, Burning
    }

    private int _frameTime;
    private readonly List<ElementType> _inflictedElements = new();

    public Element(int frameTime, ElementType type)
    {
        _frameTime = frameTime;
        _inflictedElements.Add(type);
    }

    public int FrameTime => _frameTime;

    public int AddFrameTime(int amount)
    {
        _frameTime += amount;
        return _frameTime;
    }

    /// <summary>Applies an element, potentially triggering a reaction.</summary>
    public bool InflictElement(ElementType element)
    {
        _inflictedElements.Add(element);
        // Reaction logic can be expanded here.
        return true;
    }

    public IReadOnlyList<ElementType> InflictedElements => _inflictedElements;
}
