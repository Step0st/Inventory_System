using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "New Item", menuName = "Inventory System/Items/New Item")]
public class ItemObject : ScriptableObject
{

    public Sprite itemSprite;
    public bool stackable;
    public ItemType type;
    [TextArea(5, 10)]
    public string description;
    public Item data = new Item();

    // public Item CreateItem()
    // {
    //     Item newItem = new Item(this);
    //     return newItem;
    // }
    
}


