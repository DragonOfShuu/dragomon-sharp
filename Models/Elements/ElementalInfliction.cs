namespace DragoSharp.Models.Elements;

public class ElementalInfliction(ElementType type, int frameTime, int magnitude = 1)
{
    public ElementType Type { get; } = type;
    public int Duration { get; } = frameTime;
    public int Magnitude { get; } = magnitude;
}
