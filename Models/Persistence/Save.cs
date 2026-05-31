using System.Text.Json;
using DragoSharp.Common;
using DragoSharp.Models.Dragons;
using DragoSharp.Models.Tiles;

namespace DragoSharp.Models.Persistence;

/// <summary>
/// A save slot: wraps a Game plus metadata.
/// Implements IEditableItem so it can appear in the save-picker EditUI.
/// Pure model — no Console I/O.
/// </summary>
public class Save : IEditableItem
{
    // ── Constants ─────────────────────────────────────────────────────────────

    private static readonly string FolderPath = "data";
    private static int _counter;

    // ── State ─────────────────────────────────────────────────────────────────

    private readonly long _id;
    private readonly string _filePath;
    private Game _game;

    public string? SaveName { get; set; }
    public DateTime DateMade { get; private set; }
    public bool Favorite { get; private set; }

    // ── Construction ──────────────────────────────────────────────────────────

    public Save(Game game)
    {
        _counter++;
        _game = game;
        DateMade = DateTime.Now;
        _id = (DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() / 2L) + _counter;
        _filePath = Path.Combine(FolderPath, $"{_id}.json");
    }

    /// <summary>Used by the load system to reconstruct a Save from disk.</summary>
    private Save(Game game, long id, string filePath, string? name, DateTime date, bool fav)
    {
        _game = game;
        _id = id;
        _filePath = filePath;
        SaveName = name;
        DateMade = date;
        Favorite = fav;
    }

    public Game Game => _game;

    public Save UpdateDate()
    {
        DateMade = DateTime.Now;
        return this;
    }

    // ── Persistence ───────────────────────────────────────────────────────────

    public bool SaveGame()
    {
        Directory.CreateDirectory(FolderPath);

        try
        {
            var data = ToSaveData();
            string json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"The game could not be saved: {ex.Message}");
            return false;
        }
    }

    public static Save[] LoadAll()
    {
        if (!Directory.Exists(FolderPath))
            return Array.Empty<Save>();

        var files = Directory.GetFiles(FolderPath, "*.json");
        if (files.Length == 0)
            return Array.Empty<Save>();

        var results = new List<Save>();
        foreach (var file in files)
        {
            try
            {
                string json = File.ReadAllText(file);
                var data = JsonSerializer.Deserialize<SaveData>(json);
                if (data == null) continue;
                results.Add(FromSaveData(data, file));
            }
            catch
            {
                Console.WriteLine($"Warning: could not load save file '{file}'.");
            }
        }
        return results.ToArray();
    }

    // ── SaveData conversion ───────────────────────────────────────────────────

    private SaveData ToSaveData()
    {
        var world = _game.World;
        var player = world.Player;

        // Serialize dragons
        var dragonData = player.Backpack.Select((d, i) => new DragonData
        {
            DragonType = d.GetType().Name,
            Name = d.Name,
            Nickname = d.Nickname,
            Level = d.Level,
            Health = d.Health,
            IsFavorite = d.IsFavorite
        }).ToArray();

        int activeIdx = player.ActiveDragon != null
            ? player.Backpack.IndexOf(player.ActiveDragon)
            : 0;

        // Serialize tiles
        var rawTiles = world.GetRawTiles();
        var tileData = rawTiles.Select(row =>
            row.Select(t => new TileData
            {
                TileType = t.GetType().Name,
                Explored = t.Explored
            }).ToArray()
        ).ToArray();

        return new SaveData
        {
            Id = _id,
            SaveName = SaveName,
            DateMade = DateMade,
            Favorite = Favorite,
            PlayerName = player.Name,
            PlayerX = player.XPos,
            PlayerY = player.YPos,
            PlayerXp = player.Xp,
            ActiveDragonIndex = activeIdx,
            WorldSizeX = world.SizeX,
            WorldSizeY = world.SizeY,
            Tiles = tileData,
            Dragons = dragonData
        };
    }

    private static Save FromSaveData(SaveData data, string filePath)
    {
        // Reconstruct dragons
        var dragons = data.Dragons.Select(d => BuildDragon(d)).ToList();

        // Reconstruct player (uses internal restore constructor)
        var player = new Player.Player(data.PlayerName, data.PlayerX, data.PlayerY, data.PlayerXp, restore: true);
        foreach (var d in dragons)
            player.Backpack.Add(d);

        if (dragons.Count > 0)
        {
            int ai = Math.Clamp(data.ActiveDragonIndex, 0, dragons.Count - 1);
            player.SetActiveDragon(dragons[ai]);
        }

        // Reconstruct world
        var world = new World(data.WorldSizeX, data.WorldSizeY, player);

        var tileGrid = data.Tiles.Select(row =>
            row.Select(t => BuildTile(t)).ToList()
        ).ToList();

        world.RestoreTiles(tileGrid);

        var game = new Game(world);
        return new Save(game, data.Id, filePath, data.SaveName, data.DateMade, data.Favorite);
    }

    private static Dragon BuildDragon(DragonData d)
    {
        Dragon dragon = d.DragonType switch
        {
            "Earth" => new Earth(d.Level, d.Health),
            "Fire" => new Fire(d.Level, d.Health),
            "Water" => new Water(d.Level, d.Health),
            "Electric" => new Electric(d.Level, d.Health),
            "Ice" => new Ice(d.Level, d.Health),
            "Nature" => new Nature(d.Level, d.Health),
            "Wind" => new Wind(d.Level, d.Health),
            _ => new Dragon(d.Name, d.Level, d.Health)
        };
        // Named dragons (like the starter "Shuckle") need their Name restored
        // via the nickname if the species name differs:
        if (d.DragonType == "Generic")
        {
            // Dragon.Name is private; nickname stands in for named starters
            dragon.Nickname = d.Name;
        }
        if (!string.IsNullOrEmpty(d.Nickname))
            dragon.Nickname = d.Nickname;
        dragon.IsFavorite = d.IsFavorite;
        return dragon;
    }

    private static Tile BuildTile(TileData t)
    {
        Tile tile = t.TileType switch
        {
            "Forest" => new Forest(),
            "Home" => new Home(),
            "Mountain" => new Mountain(),
            "Swamp" => new Swamp(),
            "Mistlands" => new Mistlands(),
            _ => new Grassland()
        };
        tile.Explored = t.Explored;
        return tile;
    }

    // ── IEditableItem ─────────────────────────────────────────────────────────

    string IEditableItem.DisplayName =>
        SaveName ?? DateMade.ToString("g");

    bool IEditableItem.IsFavorite => Favorite;

    bool IEditableItem.CanDelete => true;
    bool IEditableItem.Delete()
    {
        try { File.Delete(_filePath); return true; }
        catch { return false; }
    }

    bool IEditableItem.CanAdd => true;
    bool IEditableItem.Add() => false;   // "add" means create new — handled by controller
    bool IEditableItem.IsCompleteObjectAddition => true;   // signals EditUI to show a "Create new…" option

    bool IEditableItem.CanRename => true;
    bool IEditableItem.Rename(string newName)
    {
        SaveName = string.IsNullOrWhiteSpace(newName) ? null : newName;
        SaveGame();
        return true;
    }

    bool IEditableItem.CanUse => true;
    bool IEditableItem.Use() => true;   // "use" = select this save

    bool IEditableItem.CanFavorite => true;
    bool IEditableItem.ToggleFavorite()
    {
        Favorite = !Favorite;
        SaveGame();
        return Favorite;
    }

    public override string ToString() =>
        SaveName ?? DateMade.ToString("g");
}
