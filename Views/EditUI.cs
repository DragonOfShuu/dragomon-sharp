using DragoSharp.Common;

namespace DragoSharp.Views;

/// <summary>
/// Interactive list editor displayed in the terminal.
/// Lets the user browse, rename, delete, favourite, use, or add items.
/// Pure view / controller glue: asks the view (Question/Screen) for input,
/// delegates mutations to the IEditableItem model objects.
/// </summary>
public class EditUI
{
    // ── Inner types ───────────────────────────────────────────────────────────

    private enum Action
    {
        Delete,
        Add,
        Rename,
        Use,
        Favourite,
        Back,
    }

    /// <summary>How the EditUI session ended.</summary>
    public enum ExitReason
    {
        UserExited,
        ItemSelected,
        CreatedNew,
    }

    public class Result<T>
        where T : IEditableItem
    {
        public ExitReason Reason { get; init; }
        public T? SelectedItem { get; init; }
        public IEnumerable<T> NewItems { get; init; } = [];
        public bool CreatedNew => Reason == ExitReason.CreatedNew;
    }

    public class ItemManipulationResult<T>
        where T : IEditableItem
    {
        public T? SelectedItem { get; init; } = default;
        public IEnumerable<T>? NewItems { get; init; } = null;
    }

    // ── Entry point ───────────────────────────────────────────────────────────

    /// <summary>
    /// Runs the interactive list editor and returns a Result describing
    /// how the user exited and what (if anything) they selected.
    /// </summary>
    public static Result<T> Run<T>(string prompt, IEnumerable<T> source)
        where T : IEditableItem
    {
        var items = source.ToList();
        bool supportsCreate = items.Count > 0 && items[0].IsCompleteObjectAddition;

        while (true)
        {
            Screen.Clear();

            // Favorites bubble to the top
            items = [.. items.OrderByDescending(i => i.IsFavorite).ThenBy(i => i.DisplayName)];

            // Build the menu
            var menuLabels = items.Select(i => (i.IsFavorite ? "★ " : "") + i.DisplayName).ToList();

            if (supportsCreate)
                menuLabels.Add("Create new...");
            menuLabels.Add("Exit");

            string[] options = [.. menuLabels];
            int chosen = Question.ChooseItem(prompt, options);

            // "Exit" — last entry
            if (chosen == options.Length - 1)
                return new Result<T> { Reason = ExitReason.UserExited, NewItems = items };

            // "Create new…" — second-to-last when available
            if (supportsCreate && chosen == options.Length - 2)
                return new Result<T> { Reason = ExitReason.CreatedNew, NewItems = items };

            T item = items[chosen];

            var actionResult = ManipulateItem(item, items, chosen, supportsCreate);

            if (actionResult.NewItems is not null)
                items = [.. actionResult.NewItems]; // update list if item was deleted

            if (actionResult.SelectedItem is not null)
                return new Result<T>
                {
                    Reason = ExitReason.ItemSelected,
                    SelectedItem = actionResult.SelectedItem,
                    NewItems = items,
                };
        }
    }

    // ── Item interaction ──────────────────────────────────────────────────────

    private static ItemManipulationResult<T> ManipulateItem<T>(
        T item,
        IEnumerable<T> oldItems,
        int idx,
        bool supportsCreate
    )
        where T : IEditableItem
    {
        List<T> items = [.. oldItems]; // make a mutable copy
        while (true)
        {
            Console.WriteLine($"{item.DisplayName} selected.");

            var actions = AvailableActions(item, supportsCreate);
            var actionLabels = actions.Select(a => a.ToString()).ToArray();
            int choice = Question.ChooseItem("What would you like to do?", actionLabels);
            Action action = actions[choice];

            switch (action)
            {
                case Action.Use:
                    if (item.Use())
                        return new ItemManipulationResult<T> { SelectedItem = item };
                    break; // Use returned false → stay on the item

                case Action.Favourite:
                    item.ToggleFavorite();
                    break;

                case Action.Rename:
                    string newName = Question.RequestString("Enter a new name (blank to clear):");
                    item.Rename(newName);
                    break;

                case Action.Add:
                    item.Add();
                    break;

                case Action.Delete:
                    if (item.Delete())
                    {
                        items.RemoveAt(idx);
                        return new ItemManipulationResult<T> { NewItems = items }; // back to list
                    }
                    break;

                case Action.Back:
                    return new ItemManipulationResult<T> { NewItems = null }; // back to list
            }
        }
    }

    // ── Helpers ───────────────────────────────────────────────────────────────

    private static Action[] AvailableActions(IEditableItem item, bool includeAdd)
    {
        var list = new List<Action>();
        if (item.CanUse)
            list.Add(Action.Use);
        if (item.CanFavorite)
            list.Add(Action.Favourite);
        if (item.CanRename)
            list.Add(Action.Rename);
        if (item.CanAdd && includeAdd)
            list.Add(Action.Add);
        if (item.CanDelete)
            list.Add(Action.Delete);
        list.Add(Action.Back);
        return [.. list];
    }
}
