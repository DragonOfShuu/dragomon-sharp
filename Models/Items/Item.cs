namespace DragoSharp.Models.Items;

public class Item(ItemProperties properties)
{
    public string DisplayName => properties.Name;

    public string? Description => properties.Description;

    public bool Consumable => properties.Consumable;

    public void Consume()
    {
        if (!Consumable)
        {
            throw new InvalidOperationException($"{DisplayName} is not consumable.");
        }
    }

    /// <summary> An overridable method that is called when the item is consumed. This can be used to apply effects or trigger events. </summary>
    public void OnConsume() { }
}
