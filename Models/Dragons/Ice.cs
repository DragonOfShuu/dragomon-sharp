using DragoSharp.Models.Utils;

namespace DragoSharp.Models.Dragons;

public class Ice : Dragon
{
    public static readonly string[] Types =
        { "Abra", "Drowzee", "Mr. Mime", "Natu", "Espeon" };

    public Ice(int environmentLevel, float startingHealth)
        : base(Statics.PickRItem(Types)!, environmentLevel, startingHealth) { }
}
