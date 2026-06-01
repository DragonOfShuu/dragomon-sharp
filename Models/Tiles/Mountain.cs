using DragoSharp.Models.Dragons;
using DragoSharp.Models.Utils;

namespace DragoSharp.Models.Tiles;

public class Mountain : Tile
{
    public override TileResult Activate(Player.Player player)
    {
        int roll = Statics.GenRNum(0, 3);
        if (roll == 0) return new TileResult(new[] { "You entered the mountain..." }, null, hasEncounterChance: true);

        int level = Statics.GenRNum(41, 65);
        int dragonType = Statics.GenRNum(0, 4);

        var dragon = dragonType switch
        {
            0 => Cryo.CreateDragon(level),
            1 => Geo.CreateDragon(level),
            2 => Anemo.CreateDragon(level),
            _ => Hydro.CreateDragon(level)
        };

        return new TileResult(
            new[] { "You encountered a dragon on the mountain!" },
            dragon,
            hasEncounterChance: true);
    }

    public override char Repr => 'M';
    public override string Name => "Mountain";
}
