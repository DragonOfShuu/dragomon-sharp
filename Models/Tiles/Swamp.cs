using DragoSharp.Models.Dragons;
using DragoSharp.Models.Utils;

namespace DragoSharp.Models.Tiles;

public class Swamp : Tile
{
    public override TileResult Activate(Player.Player player)
    {
        var dragon = GenDragon(4,
            new Water(Statics.GenRNum(61, 85), 100),
            new Nature(Statics.GenRNum(61, 85), 100),
            new Electric(Statics.GenRNum(41, 65), 100));

        return new TileResult(
            new[] { "You entered the swamp..." },
            dragon,
            hasEncounterChance: true);
    }

    public override char Repr => 'S';
    public override string Name => "Swamp";
}
