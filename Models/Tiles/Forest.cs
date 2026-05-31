using DragoSharp.Models.Dragons;
using DragoSharp.Models.Utils;

namespace DragoSharp.Models.Tiles;

public class Forest : Tile
{
    public override TileResult Activate(Player.Player player)
    {
        var dragon = GenDragon(10,
            new Water(Statics.GenRNum(21, 45), 100),
            new Wind (Statics.GenRNum(21, 45), 100),
            new Earth(Statics.GenRNum(21, 45), 100),
            new Nature(Statics.GenRNum(21, 45), 100));

        return new TileResult(
            new[] { "You entered the forest..." },
            dragon,
            hasEncounterChance: true);
    }

    public override char   Repr => 'F';
    public override string Name => "Forest";
}
