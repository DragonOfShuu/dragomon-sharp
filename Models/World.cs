using DragoSharp.Models.Utils;
using DragoSharp.Models.Tiles;

namespace DragoSharp.Models;

/// <summary>
/// The game world: a 2-D grid of tiles plus the player.
/// All mutation methods return data; they never call Console.
/// </summary>
public class World
{
    // ── Public helpers ────────────────────────────────────────────────────────

    /// <summary>Total number of unique dragon species across all types.</summary>
    public static int DragonCount()
    {
        // Each dragon type has 5-7 unique species
        // Fire: 5, Electric: 5, Water: 5, Ice: 5, Earth: 5, Nature: 7, Wind: 6
        return 38;
    }

    // ── State ─────────────────────────────────────────────────────────────────

    /// <summary>Row-major tile storage: worldTiles[y][x].</summary>
    private List<List<Tile>> _tiles = new();
    private bool _generated;

    public int SizeX { get; private set; }
    public int SizeY { get; private set; }
    public Player.Player Player { get; private set; }
    public bool IsGenerated => _generated;

    // ── Construction ──────────────────────────────────────────────────────────

    public World(int sizeX, int sizeY, Player.Player player)
    {
        SizeX = sizeX;
        SizeY = sizeY;
        Player = player;
    }

    // ── Tile access ───────────────────────────────────────────────────────────

    public Tile GetTile(int x, int y)
    {
        if (y < 0 || y >= _tiles.Count || x < 0 || x >= _tiles[y].Count)
            throw new IndexOutOfRangeException($"Tile ({x},{y}) is out of bounds.");
        return _tiles[y][x];
    }

    public bool TileExists(int x, int y)
    {
        try { return GetTile(x, y) != null; }
        catch { return false; }
    }

    private void SetTile(int x, int y, Tile tile) => _tiles[y][x] = tile;

    // ── World generation ──────────────────────────────────────────────────────

    public void Generate()
    {
        if (_generated) return;
        _generated = true;

        // Base layer: all grassland
        _tiles = new List<List<Tile>>();
        for (int y = 0; y < SizeY; y++)
        {
            var row = new List<Tile>();
            for (int x = 0; x < SizeX; x++)
                row.Add(new Grassland());
            _tiles.Add(row);
        }

        GenerateMountain();
        GenerateSwamp();
        GenerateForest();
        GenerateMistlands();

        // Player's home tile
        SetTile(Player.XPos, Player.YPos, new Home());
    }

    // Sparse mountains biased toward the top (low y index = bottom of the map
    // in screen coords, but generation here works in array index order).
    private void GenerateMountain()
    {
        var rand = new Random();
        for (int y = 0; y < _tiles.Count; y++)
        {
            var row = _tiles[y];
            float chance = 100f * (SizeX / 10f) / ((float)y * 3f);
            for (int x = 0; x < row.Count; x++)
                if (rand.NextSingle() * 100f <= chance)
                    SetTile(x, y, new Mountain());
        }
    }

    // Swamp biased toward the bottom (high y index).
    private void GenerateSwamp()
    {
        var rand = new Random();
        for (int y = 0; y < _tiles.Count; y++)
        {
            var row = _tiles[y];
            float inv = Statics.InvertNum(y, 0, _tiles.Count - 1);
            float chance = 100f * (SizeX / 16f) / (inv * 4f);
            for (int x = 0; x < row.Count; x++)
                if (rand.NextSingle() * 100f <= chance)
                    SetTile(x, y, new Swamp());
        }
    }

    // ~33 % of remaining grassland becomes forest.
    private void GenerateForest()
    {
        var rand = new Random();
        for (int y = 0; y < _tiles.Count; y++)
            for (int x = 0; x < _tiles[y].Count; x++)
                if (_tiles[y][x] is Grassland && rand.Next(3) == 0)
                    SetTile(x, y, new Forest());
    }

    // Sinusoidal mistland columns on the left and right edges.
    private void GenerateMistlands()
    {
        for (int y = 0; y < _tiles.Count; y++)
        {
            int ratio = SizeX / ((10 * ((SizeX - 16) / 48)) + 16);
            int count = (int)(Math.Sin(y / 4.0) * (5.0 * ratio) - 3);

            for (int x = 0; x < count; x++)
            {
                SetTile(x, y, new Mistlands());
                SetTile((SizeX - 1) - x, y, new Mistlands());
            }
        }
    }

    // ── Player movement ───────────────────────────────────────────────────────

    /// <summary>
    /// Moves the player by (dx, dy) and optionally activates the landing tile.
    /// Returns the TileResult from activation (or an empty result if no activation).
    /// Throws IndexOutOfRangeException if the destination is out of bounds.
    /// </summary>
    public TileResult OffsetPlayer(int dx, int dy, bool activateTile = true)
    {
        if (!TileExists(Player.XPos + dx, Player.YPos + dy))
            throw new IndexOutOfRangeException("Player cannot fall off the map.");

        Player.MovePlayer(dx, dy);

        return activateTile
            ? GetTile(Player.XPos, Player.YPos).Activation(Player)
            : new TileResult(Array.Empty<string>());
    }

    // ── Serialisation support ─────────────────────────────────────────────────

    /// <summary>
    /// Directly writes the tile grid (used by the load system to restore saved worlds
    /// without re-generating).
    /// </summary>
    internal void RestoreTiles(List<List<Tile>> tiles)
    {
        _tiles = tiles;
        _generated = true;
    }

    /// <summary>Returns a read-only view of the raw tile grid for serialisation.</summary>
    internal IReadOnlyList<IReadOnlyList<Tile>> GetRawTiles() => _tiles;
}
