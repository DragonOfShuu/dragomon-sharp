using DragoSharp.Models.Dragons;
using DragoSharp.Models.Utils;

namespace DragoSharp.Models.Tiles;

public class Forest : Tile
{
    public override TileResult Activate(Player.Player player)
    {
        int roll = Statics.GenRNum(0, 2);
        if (roll == 0) return new TileResult(new[] { "You entered the forest..." }, null, hasEncounterChance: true);

        int level = Statics.GenRNum(21, 45);
        int dragonType = Statics.GenRNum(0, 4);

        var dragon = dragonType switch
        {
            0 => Hydro.CreateDragon(level),
            1 => Anemo.CreateDragon(level),
            2 => Geo.CreateDragon(level),
            _ => Dendro.CreateDragon(level)
        };

        return new TileResult(
            new[] { "You encountered a dragon in the forest!" },
            dragon,
            hasEncounterChance: true);
    }

    public override char Repr => 'F';
    public override string Name => "Forest";
}
