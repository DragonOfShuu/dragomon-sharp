namespace DragoSharp.Models.Elements;

public class ElementalInfliction
{
    public ElementType Type { get; }
    public int Duration { get; }
    public int Magnitude { get; }

    public ElementalInfliction(ElementType type, int frameTime, int magnitude = 1)
    {
        Type = type;
        Duration = frameTime;
        Magnitude = magnitude;
    }
}