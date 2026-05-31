using DragoSharp.Models.Elements;
using DragoSharp.Models.Utils;

namespace DragoSharp.Models.Dragons;

public class Earth : Dragon
{
    public static readonly string[] Types =
        { "Sandshrew", "Diglett", "Cubone", "Phanpy", "Gligar" };

    public Earth(int environmentLevel, float startingHealth, float maxHealth = 100)
        : base(Statics.PickRItem(Types)!, environmentLevel, startingHealth, maxHealth, ElementType.Geo) { }
}
