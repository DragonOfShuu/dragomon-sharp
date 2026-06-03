using DragoSharp.Models.Dragons;
using DragoSharp.Models.Utils;

namespace DragoSharp.Models.Tiles;

public class Swamp : Tile
{
    public override TileResult Activate(Player.Player player)
    {
        int roll = Statics.GenRNum(0, 2);
        if (roll == 0) return new TileResult(["You entered the swamp..."], null, hasEncounterChance: true);

        int level = Statics.GenRNum(61, 85);
        int dragonType = Statics.GenRNum(0, 3);

        var dragon = dragonType switch
        {
            0 => Hydro.CreateDragon(level),
            1 => Dendro.CreateDragon(level),
            _ => Electro.CreateDragon(level)
        };

        return new TileResult(
            ["You encountered a dragon in the swamp!"],
            dragon,
            hasEncounterChance: true);
    }

    public override char Repr => 'S';
    public override string Name => "Swamp";
}
