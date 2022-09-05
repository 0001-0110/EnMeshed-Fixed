using UnityEngine;

public class Inventory
{
    public int Size { get; private set; }
    public ItemController[] Items { get; private set; }

    public Inventory(int size)
    {
        Size = size;
        Items = new ItemController[Size];
    }

    public bool AddItem(ItemController item)
    {
        throw new System.NotImplementedException();
    }

    public bool RemoveItem(ItemController item)
    {
        throw new System.NotImplementedException();
    }

    public bool RemoveItem(int slotIndex)
    {
        throw new System.NotImplementedException();
    }
}
