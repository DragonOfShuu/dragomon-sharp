using DragoSharp.Models.Utils;

namespace DragoSharp.Models.Dragons;

public class Fire : Dragon
{
    public static readonly string[] Types =
        { "Charmander", "Vulpix", "Growlithe", "Ponyta", "Flareon" };

    public Fire(int environmentLevel, float startingHealth)
        : base(Statics.PickRItem(Types)!, environmentLevel, startingHealth) { }
}
