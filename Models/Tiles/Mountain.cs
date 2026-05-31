using DragoSharp.Models.Dragons;
using DragoSharp.Models.Utils;

namespace DragoSharp.Models.Tiles;

public class Mountain : Tile
{
    public override TileResult Activate(Player.Player player)
    {
        var dragon = GenDragon(3,
            new Ice(Statics.GenRNum(41, 65), 100),
            new Earth(Statics.GenRNum(41, 65), 100),
            new Wind(Statics.GenRNum(41, 65), 100),
            new Water(Statics.GenRNum(41, 65), 100));

        return new TileResult(
            new[] { "You entered the mountain..." },
            dragon,
            hasEncounterChance: true);
    }

    public override char Repr => 'M';
    public override string Name => "Mountain";
}
