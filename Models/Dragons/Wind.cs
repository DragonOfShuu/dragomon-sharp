using DragoSharp.Models.Utils;

namespace DragoSharp.Models.Dragons;

public class Wind : Dragon
{
    public static readonly string[] Types =
        { "Gastly", "Misdreavus", "Duskull", "Drifloon", "Spiritomb" };

    public Wind(int environmentLevel, float startingHealth)
        : base(Statics.PickRItem(Types)!, environmentLevel, startingHealth) { }
}
