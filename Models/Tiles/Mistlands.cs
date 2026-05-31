using DragoSharp.Models.Dragons;
using DragoSharp.Models.Utils;

namespace DragoSharp.Models.Tiles;

public class Mistlands : Tile
{
    public override TileResult Activate(Player.Player player)
    {
        var dragon = GenDragon(4,
            new Water(Statics.GenRNum(76, 99), 100),
            new Fire (Statics.GenRNum(76, 99), 100));

        return new TileResult(
            new[] { "You entered the mistlands..." },
            dragon,
            hasEncounterChance: true);
    }

    public override char   Repr => 'L';
    public override string Name => "Mistlands";
}
