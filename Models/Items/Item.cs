using DragoSharp.Common; 

namespace DragoSharp.Models.Items;

public class Item : IEditableItem
{
    public Item()
    {
      
    }

    public string DisplayName => throw new NotImplementedException();

    public string? Description => throw new NotImplementedException();

    public bool IsFavorite => throw new NotImplementedException();

    public bool CanDelete => throw new NotImplementedException();

    public bool CanAdd => throw new NotImplementedException();

    public bool IsCompleteObjectAddition => throw new NotImplementedException();

    public bool CanRename => throw new NotImplementedException();

    public bool CanUse => throw new NotImplementedException();

    public bool CanFavorite => throw new NotImplementedException();

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
        throw new NotImplementedException();
    }

    public bool Use()
    {
        throw new NotImplementedException();
    }
}
