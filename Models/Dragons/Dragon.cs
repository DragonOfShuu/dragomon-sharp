using DragoSharp.Common;
using DragoSharp.Models.Elements;
using DragoSharp.Views;

namespace DragoSharp.Models.Dragons;

/// <summary>
/// A Dragon — the core creature of the game.
/// Implements IEditableItem so it can appear in the backpack EditUI.
/// Pure model: all rename/use logic takes plain values; no Console calls.
/// </summary>
public class Dragon : Entity, IEditableItem
{
    public string Name { get; private set; }
    public string? Nickname { get; set; }
    public int Level { get; private set; }
    public bool IsFavorite { get; set; }
    public List<AttackPattern> Attacks { get; private set; }
    public DragonProperties? Properties { get; private set; }

    public Dragon(string name, int level, float startingHealth, float maxHealth = 100, ElementType? element = null, DragonProperties? properties = null, List<AttackPattern>? attacks = null)
        : base(startingHealth, maxHealth, element)
    {
        Name = name;
        Level = level;
        Properties = properties;
        Attacks = attacks ?? [];
    }

    /// <summary>Nickname if set, otherwise the species name.</summary>
    public string PreferredName => Nickname ?? Name;

    public override string ToString() => $"{PreferredName} lvl {Level}";

    // ── IEditableItem ────────────────────────────────────────────────────────

    string IEditableItem.DisplayName => ToString() + $"\n\t{Component.GetProgressBar((int)Health, (int)MaxHealth)}";

    string IEditableItem.Description => $"Element: {Element?.EntityElement.ToString() ?? "None"}\nHealth: {Health}/{MaxHealth}";

    bool IEditableItem.CanDelete => false;
    bool IEditableItem.Delete() => false;

    bool IEditableItem.CanAdd => false;
    bool IEditableItem.Add() => false;

    bool IEditableItem.IsCompleteObjectAddition => false;

    bool IEditableItem.CanRename => true;
    /// <param name="newName">Empty string clears the nickname.</param>
    bool IEditableItem.Rename(string newName)
    {
        Nickname = string.IsNullOrEmpty(newName) ? null : newName;
        return true;
    }

    bool IEditableItem.CanUse => false;
    bool IEditableItem.Use() => false;

    bool IEditableItem.CanFavorite => true;
    bool IEditableItem.ToggleFavorite()
    {
        IsFavorite = !IsFavorite;
        return IsFavorite;
    }
}
