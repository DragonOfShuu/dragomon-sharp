namespace DragoSharp.Models.Items;

public class ItemProperties
{
    public string Name { get; private set; } = "Unnamed Item";
    public string Description { get; private set; } = "No description available.";
    public bool Consumable { get; private set; } = false;
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

    public ItemProperties IsConsumable(bool consumable = true)
    {
        Consumable = consumable;
        return this;
    }

    public ItemProperties WithEffect(Effect effect)
    {
        Effect = effect;
        return this;
    }
}
