using DragoSharp.Models.Dragons;
using DragoSharp.Models.Utils;

namespace DragoSharp.Models.Tiles;

public class Grassland : Tile
{
    public override TileResult Activate(Player.Player player)
    {
        var dragon = GenDragon(10,
            new Water(Statics.GenRNum(1, 25), 100),
            new Wind (Statics.GenRNum(1, 25), 100),
            new Fire (Statics.GenRNum(1, 25), 100),
            new Nature(Statics.GenRNum(1, 25), 100));

        return new TileResult(
            new[] { "You entered the grassland..." },
            dragon,
            hasEncounterChance: true);
    }

    public override char   Repr => 'G';
    public override string Name => "Grassland";
}
