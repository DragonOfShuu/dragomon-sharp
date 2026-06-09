using DragoSharp.Common;

namespace DragoSharp.Models.Items;

public class Item(ItemProperties properties) : IEditableItem
{
    public string DisplayName => properties.Name;

    public string? Description => properties.Description;

    public bool IsFavorite => _isFavorite;

    public bool CanDelete => throw new NotImplementedException();

    public bool CanAdd => false;

    public bool IsCompleteObjectAddition => false;

    public bool CanRename => false;

    public bool CanUse => throw new NotImplementedException();

    public bool CanFavorite => true;

    private bool _isFavorite = false;

    public bool Add()
    {
        throw new NotImplementedException();
    }

    public bool Delete()
    {
        throw new NotImplementedException();
    }

    public bool Rename(string newName)
    {
        throw new NotImplementedException();
    }

    public bool ToggleFavorite()
    {
        return ToggleFavorite(!_isFavorite);
    }

    public bool ToggleFavorite(bool value)
    {
        _isFavorite = value;
        return _isFavorite;
    }

    public bool Use()
    {
        throw new NotImplementedException();
    }
}
