using DragoSharp.Models.Elements;
using DragoSharp.Models.Utils;

namespace DragoSharp.Models.Dragons;

public class Nature : Dragon
{
    public static readonly string[] Types =
        { "Pidgey", "Rattata", "Doduo", "Eevee", "Tauros", "Bidoof" };

    public Nature(int environmentLevel, float startingHealth)
        : base(Statics.PickRItem(Types)!, environmentLevel, startingHealth, 100, ElementType.Dendro) { }
}
