using DragoSharp.Models.Dragons;
using DragoSharp.Models.Utils;

namespace DragoSharp.Models.Tiles;

public class Mistlands : Tile
{
    public override TileResult Activate(Player.Player player)
    {
        int roll = Statics.GenRNum(0, 2);
        if (roll == 0) return new TileResult(new[] { "You entered the mistlands..." }, null, hasEncounterChance: true);

        int level = Statics.GenRNum(76, 99);
        int dragonType = Statics.GenRNum(0, 2);

        var dragon = dragonType switch
        {
            0 => Hydro.CreateDragon(level),
            _ => Pyro.CreateDragon(level)
        };

        return new TileResult(
            new[] { "You encountered a powerful dragon in the mistlands!" },
            dragon,
            hasEncounterChance: true);
    }

    public override char Repr => 'L';
    public override string Name => "Mistlands";
}
