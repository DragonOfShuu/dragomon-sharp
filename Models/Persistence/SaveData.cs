namespace DragoSharp.Models.Persistence;

// ── Data-transfer objects for JSON serialisation ──────────────────────────────
// These are plain classes with no game logic; they are the on-disk format.

public class SaveData
{
    public long     Id                { get; set; }
    public string?  SaveName          { get; set; }
    public DateTime DateMade          { get; set; }
    public bool     Favorite          { get; set; }

    public string   PlayerName        { get; set; } = "";
    public int      PlayerX           { get; set; }
    public int      PlayerY           { get; set; }
    public int      PlayerXp          { get; set; }
    public int      ActiveDragonIndex { get; set; }   // index in Dragons array

    public int      WorldSizeX        { get; set; }
    public int      WorldSizeY        { get; set; }

    public TileData[][]  Tiles   { get; set; } = Array.Empty<TileData[]>();
    public DragonData[]  Dragons { get; set; } = Array.Empty<DragonData>();
}

public class TileData
{
    public string TileType { get; set; } = "Grassland";
    public bool   Explored { get; set; }
}

public class DragonData
{
    /// <summary>Class name of the dragon species (e.g. "Earth", "Fire", "Generic").</summary>
    public string  DragonType { get; set; } = "Generic";
    public string  Name       { get; set; } = "";
    public string? Nickname   { get; set; }
    public int     Level      { get; set; }
    public float   Health     { get; set; }
    public bool    IsFavorite { get; set; }
}
