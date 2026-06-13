namespace DragoSharp.Common;

/// <summary>
/// Contract for objects that can appear in an EditUI list.
/// Intentionally contains only pure-data operations (no Scanner / Console
/// parameters) so model types can implement it without depending on the
/// view layer.  All user-facing prompts live in the EditUI view class.
/// </summary>
public interface IEditableItem
{
    /// <summary>Text shown in the list.</summary>
    string DisplayName { get; }

    string? Description { get; } // Optional multiline text shown when the item is selected; null to show nothing.

    bool IsFavorite { get; }

    bool CanDelete { get; }

    /// <returns>True if the item was successfully deleted functionally (if necessary). If this function returns true, it will be removed from the collection.</returns>
    bool Delete();

    bool CanAdd { get; }

    /// <returns>True if the item was successfully added to a collection.</returns>
    bool Add();

    /// <summary>
    /// When true the EditUI shows a "Create new…" entry at the bottom of the list,
    /// allowing the user to add a brand-new item rather than operating on an existing one.
    /// </summary>
    bool IsCompleteObjectAddition { get; }

    bool CanRename { get; }

    /// <param name="newName">The new name supplied by the view after prompting the user.</param>
    bool Rename(string newName);

    bool CanUse { get; }

    /// <returns>True if the caller should treat this as a "select and exit" event.</returns>
    bool Use();

    bool CanFavorite { get; }
    bool ToggleFavorite();
}
