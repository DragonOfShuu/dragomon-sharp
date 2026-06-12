namespace DragoSharp.Models.Items;

public class ItemStack<TItem>(TItem item, int quantity = 1)
    where TItem : Item
{
    public TItem Item { get; private set; } = item;
    public int Quantity { get; private set; } = quantity;

    public void Add(int amount = 1)
    {
        Quantity += amount;
    }

    public bool Remove(int amount = 1)
    {
        if (amount > Quantity)
            return false;

        Quantity -= amount;
        return true;
    }
}
