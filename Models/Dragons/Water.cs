using DragoSharp.Models.Elements;
using DragoSharp.Models.Utils;

namespace DragoSharp.Models.Dragons;

public class Water : Dragon
{
    public static readonly string[] Types =
        { "Squirtle", "Psyduck", "Slowpoke", "Horsea", "Vaporeon" };

    public Water(int environmentLevel, float startingHealth)
        : base(Statics.PickRItem(Types)!, environmentLevel, startingHealth, 100, ElementType.Hydro) { }
}
