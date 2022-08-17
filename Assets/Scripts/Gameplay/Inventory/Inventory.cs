public class Inventory
{
    public int Size;
    private ItemController[] items;

    public Inventory(int size)
    {
        Size = size;
        items = new ItemController[Size];
    }
}
