namespace DragoSharp.Models.Tiles;

public class Home : Tile
{
    public override TileResult Activate(Player.Player player)
    {
        return new TileResult(new[]
        {
            "You walk up to your house.",
            $"\"Hello, {player.Name}!\", your mother says.",
            "That's all. You can't do anything else here."
        });
    }

    public override char   Repr => 'H';
    public override string Name => "Home";
}
