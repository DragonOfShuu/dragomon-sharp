using DragoSharp.Common;

namespace DragoSharp.Models.Items;

public class ItemStack<TItem>(TItem item, int quantity = 1) : IEditableItem
    where TItem : Item
{
    public int Quantity { get; private set; } = quantity;

    public string DisplayName => item.DisplayName;

    public string? Description => item.Description;

    public bool IsFavorite => throw new NotImplementedException();

    public bool CanDelete => item.Deletable;

    public bool CanAdd => false;

    public bool IsCompleteObjectAddition => false;

    public bool CanRename => true;

    public bool CanUse => item.Consumable;

    public bool CanFavorite => true;

    public bool Add()
    {
        return CanAdd; // No "add" action for stacks, but this satisfies the contract.
    }

    public bool Delete()
    {
        return CanDelete; // Deletion is handled by the containing collection, but this satisfies the contract.
    }

    public bool Rename(string newName)
    {
        throw new NotImplementedException();
    }

    public bool ToggleFavorite()
    {
        throw new NotImplementedException();
    }

    public bool Use()
    {
        throw new NotImplementedException();
    }
}
