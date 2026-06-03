namespace DragoSharp.Models.Player;

/// <summary>
/// The player: position, XP, and dragon collection.
/// Raises events for level-up so the view layer can react without
/// the model ever calling Console directly.
/// Pure model — no I/O.
/// </summary>
public class Player
{
    public string Name { get; private set; }

    public int XPos { get; set; }
    public int YPos { get; set; }

    /// <summary>
    /// XP with internal set so the persistence layer can restore it on load.
    /// </summary>
    public int Xp { get; internal set; }

    private readonly Backpack _backpack = [];
    public Backpack Backpack => _backpack;

    private Dragons.Dragon? _activeDragon;
    public Dragons.Dragon? ActiveDragon => _activeDragon;

    /// <summary>Raised when the player gains a level. Argument is the new level.</summary>
    public event Action<int>? OnLevelUp;

    // ── Construction ─────────────────────────────────────────────────────────

    public Player(string name, int x, int y)
    {
        Name = name;
        XPos = x;
        YPos = y;

        var starter = new Dragons.Dragon("Shuckle", 15, 100);
        _backpack.Add(starter);
        _activeDragon = starter;
    }

    /// <summary>
    /// Restoration constructor used by the save/load system.
    /// Does NOT create a starter dragon — dragons are added via AddDragon.
    /// </summary>
    internal Player(string name, int x, int y, int xp)
    {
        Name = name;
        XPos = x;
        YPos = y;
        Xp = xp;
    }

    // ── XP / Level ───────────────────────────────────────────────────────────

    /// <summary>
    /// Maps raw XP to the player's display level.
    /// </summary>
    public int GetLevel()
    {
        if (Xp == 0) return 0;
        if (Xp is >= 1 and < 5) return 1;
        if (Xp is >= 5 and < 10) return 2;
        if (Xp is >= 10 and < 15) return 3;
        if (Xp is >= 15 and < 30) return 4;
        if (Xp is >= 30 and < 45) return 5;
        if (Xp is >= 45 and < 69) return 6;
        if (Xp == 69) return 69;
        if (Xp is >= 70 and < 90) return 6;
        if (Xp is >= 90 and < 135) return 7;
        if (Xp is >= 135 and < 270) return 8;
        if (Xp is >= 270 and < 300) return 9;
        return 10;
    }

    /// <summary>Adds XP and fires OnLevelUp if the player leveled up.</summary>
    public void AddXP(int amount)
    {
        int prev = GetLevel();
        Xp += amount;
        int next = GetLevel();
        if (next != prev)
            OnLevelUp?.Invoke(next);
    }

    // ── Dragon management ─────────────────────────────────────────────────────

    public void AddDragon(Dragons.Dragon dragon)
    {
        _backpack.Add(dragon);
        if (_activeDragon == null || dragon.Level > _activeDragon.Level)
            _activeDragon = dragon;
    }

    public void RemoveDragon(Dragons.Dragon dragon)
    {
        _backpack.Remove(dragon);
        if (_activeDragon == dragon)
            _activeDragon = _backpack.FirstOrDefault();
    }

    public void SetActiveDragon(Dragons.Dragon dragon)
    {
        _activeDragon = dragon;
    }

    // ── Movement ──────────────────────────────────────────────────────────────

    public void MovePlayer(int dx, int dy)
    {
        XPos += dx;
        YPos += dy;
    }
}
