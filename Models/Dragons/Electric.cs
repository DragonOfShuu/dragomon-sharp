using DragoSharp.Models.Utils;

namespace DragoSharp.Models.Dragons;

public class Electric : Dragon
{
    public static readonly string[] Types =
        { "Pikachu", "Magnemite", "Voltorb", "Electabuzz", "Jolteon" };

    public Electric(int environmentLevel, float startingHealth)
        : base(Statics.PickRItem(Types)!, environmentLevel, startingHealth) { }
}
