using DragoSharp.Models;
using DragoSharp.Models.Player;

namespace DragoSharp.Views;

/// <summary>
/// Responsible for all terminal output related to the game world:
/// the minimap, player statistics, and map rendering.
/// Receives plain data from the controller — never touches game logic.
/// </summary>
public static class GameView
{
    // ── Statistics header ─────────────────────────────────────────────────────

    /// <summary>
    /// Prints the HUD shown at the top of each main-loop iteration:
    /// player coordinates, current tile name, and the 3×3 minimap.
    /// </summary>
    public static void PrintStatistics(World world)
    {
        Player player = world.Player;
        Console.WriteLine($"Player Coords - ({player.XPos}, {player.YPos})");
        Console.WriteLine($"Current Tile : {world.GetTile(player.XPos, player.YPos).Name}");
        RenderMiniMap(world, player, radius: 1, hideUnexplored: false);
    }

    // ── Map rendering ─────────────────────────────────────────────────────────

    /// <summary>
    /// Renders a framed minimap centred on <paramref name="player"/>.
    /// </summary>
    /// <param name="radius">
    /// Number of tiles to show in each direction from the player.
    /// The rendered square is (2*radius+1) × (2*radius+1).
    /// </param>
    /// <param name="hideUnexplored">
    /// When true, unexplored tiles are shown as █ instead of their symbol.
    /// </param>
    public static void RenderMiniMap(World world, Player player, int radius, bool hideUnexplored)
    {
        int size = radius * 2 + 1;
        var map = new string[size, size];

        int topY = player.YPos + radius;
        int bottomY = player.YPos - radius;
        int leftX = player.XPos - radius;
        int rightX = player.XPos + radius;

        int row = 0;
        for (int y = topY; y >= bottomY; y--)
        {
            int col = 0;
            for (int x = leftX; x <= rightX; x++)
            {
                try
                {
                    var tile = world.GetTile(x, y);
                    map[row, col] = hideUnexplored && !tile.Explored
                        ? "█"
                        : tile.Repr.ToString();
                }
                catch (IndexOutOfRangeException)
                {
                    map[row, col] = " ";
                }
                col++;
            }
            row++;
        }

        // The player's position is always shown as 'X'
        map[radius, radius] = "X";

        // ── Border ────────────────────────────────────────────────────────────
        int innerWidth = size * 2 + 1;

        Console.Write("╔");
        Console.Write(new string('═', innerWidth));
        Console.WriteLine("╗");

        for (int r = 0; r < size; r++)
        {
            Console.Write("║ ");
            for (int c = 0; c < size; c++)
                Console.Write(map[r, c] + " ");
            Console.WriteLine("║");
        }

        Console.Write("╚");
        Console.Write(new string('═', innerWidth));
        Console.WriteLine("╝");
    }
}
