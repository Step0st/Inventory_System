using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory System/Items/New Item")]
public class ItemObject : ScriptableObject
{
    public Sprite itemSprite;
    public ItemType type;
    [TextArea(3, 8)]
    public string description;
    public Item data = new Item();
}


