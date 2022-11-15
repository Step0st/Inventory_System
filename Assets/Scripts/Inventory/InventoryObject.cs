using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public ItemDatabaseObject database;
    public Inventory container;
    public InventorySlot[] GetSlots { get { return container.Slots; } }

    public void SwapItems(InventorySlot item1, InventorySlot item2)
    {
        InventorySlot temp = new InventorySlot( item2.item, item2.amount);
        item2.UpdateSlot(item1.item, item1.amount);
        item1.UpdateSlot(temp.item, temp.amount);
    }
    
    [ContextMenu("Clear")]
    public void Clear()
    {
        container.Clear();
    }
}

public delegate void SlotUpdated(InventorySlot _slot);

