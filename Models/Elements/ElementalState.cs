namespace DragoSharp.Models.Elements;

/// <summary>
/// Represents an elemental state on an Entity.
/// Pure data / logic — no I/O.
/// </summary>
public class ElementalState(int frameTime, ElementType type)
{
    private readonly List<ElementalInfliction> _inflictedElements = [];
    /// <Summary>
    ///  Frametime is the number of frames that the elemental effect will lst for.
    /// </summary>
    private readonly int frameTime = frameTime;
    private readonly ElementType _entityElement = type;

    public ElementType EntityElement => _entityElement;

    /// <summary>Applies an element, potentially triggering a reaction.</summary>
    public bool InflictElement(ElementType element)
    {
        _inflictedElements.Add(new ElementalInfliction(element, 1));
        // Reaction logic can be expanded here.
        return true;
    }

    public IReadOnlyList<ElementalInfliction> InflictedElements => _inflictedElements;
}
