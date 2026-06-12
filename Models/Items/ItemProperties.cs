namespace DragoSharp.Models.Items;

public class ItemProperties
{
    public string Name { get; private set; } = "Unnamed Item";
    public string Description { get; private set; } = "No description available.";
    public bool Deletable { get; private set; } = true;
    public bool Consumable { get; private set; } = false;

    /// <summary> Indicates whether the item is not consumed by a dragon when used. This is useful when an item affects the world, or things like luck, where a dragon isn't actually drinking it.</summary>
    public bool NotConsumedByDragon { get; private set; } = false;
    public Effect? Effect { get; private set; }

    public ItemProperties WithName(string name)
    {
        Name = name;
        return this;
    }

    public ItemProperties WithDescription(string description)
    {
        Description = description;
        return this;
    }

    public ItemProperties IsDeletable(bool deletable = true)
    {
        Deletable = deletable;
        return this;
    }

    public ItemProperties IsConsumable(bool consumable = true)
    {
        Consumable = consumable;
        return this;
    }

    public ItemProperties IsNotConsumedByDragon(bool notConsumedByDragon = true)
    {
        NotConsumedByDragon = notConsumedByDragon;
        return this;
    }

    public ItemProperties WithEffect(Effect effect)
    {
        Effect = effect;
        return this;
    }
}
