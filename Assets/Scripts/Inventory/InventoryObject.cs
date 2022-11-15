using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;
using System.Runtime.Serialization;

// public enum InterfaceType
// {
//     Inventory,
//     Equipment,
// }

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public ItemDatabaseObject database;
    //public InterfaceType type;
    public Inventory container;

    //private Inventory newContainer;
    //public Inventory categoryContainer;
    //public ItemType itemCategory;

    /*private void CategorySwitch()
    {
        switch(itemCategory)
        {
            case InventoryCategory.All:
                Debug.Log("consumables");
                break;
            case InventoryCategory.Consumables:
                Debug.Log("weapon");
                break;
            case InventoryCategory.Weapon:
                Debug.Log("weapon");
                break;
            case InventoryCategory.Armor:
                Debug.Log("armor");
                break;
            default:
                Debug.Log("all");
                break;
        }
    }*/
    
    public InventorySlot[] GetSlots { get { return container.Slots; } }
    //public InventorySlot[] GetCategorySlots { get { return categoryContainer.Slots; } }    

    public bool AddItem(Item _item, int _amount /* ItemType _itemType */)
    {
        /*if (EmptySlotCount <= 0)
            return false;
        if (_itemType == database.ItemObjects[_item.Id].type)
        {
            InventorySlot slot = FindItemOnInventory(_item);
            if(!database.ItemObjects[_item.Id].stackable || slot == null)
            {
                SetEmptySlot(_item, _amount);
                return true;
            }
            slot.AddAmount(_amount);
            return true;
        }
        else
        {
            return false;
        }*/
        
        
        if (EmptySlotCount <= 0)
            return false;
        InventorySlot slot = FindItemOnInventory(_item);
        if(!database.ItemObjects[_item.Id].stackable || slot == null)
        {
            SetEmptySlot(_item, _amount);
            return true;
        }
        slot.AddAmount(_amount);
        return true;
    }

    /*public void SwitchCategory(ItemType _itemType)
    {
        int j = 0;
        for (int i = 0; i < container.Slots.Length; i++)
        {
            //Debug.Log(GetSlots[i].ItemObject.type);
            
            if (GetSlots[i].item.Id >= 0 && GetSlots[i].ItemObject.type == _itemType)
            {
                categoryContainer.Slots[j] = container.Slots[i];
                j++;
            }
        }
        //container = categoryContainer;
        Debug.Log("After copy" + categoryContainer.Slots.Length);
        
    }*/

    public void AddItemsAtStart()
    {
        AddItem(database.ItemObjects[0].data, 1);
        AddItem(database.ItemObjects[1].data, 1);
        AddItem(database.ItemObjects[5].data, 1);
        AddItem(database.ItemObjects[6].data, 1);
        AddItem(database.ItemObjects[9].data, 1);
        AddItem(database.ItemObjects[10].data, 1);
    }
    
    
    public int EmptySlotCount
    {
        get
        {
            int counter = 0;
            for (int i = 0; i < GetSlots.Length; i++)
            {
                if (GetSlots[i].item.Id <= -1)
                {
                    counter++;
                }
            }
            return counter;
            
        }
    }
    public InventorySlot FindItemOnInventory(Item _item)
    {
        for (int i = 0; i < GetSlots.Length; i++)
        {
            if(GetSlots[i].item.Id == _item.Id)
            {
                return GetSlots[i];
            }
        }
        return null;
    }
    public InventorySlot SetEmptySlot(Item _item, int _amount)
    {
        for (int i = 0; i < GetSlots.Length; i++)
        {
            if (GetSlots[i].item.Id <= -1)
            {
                GetSlots[i].UpdateSlot(_item, _amount);
                return GetSlots[i];
            }
        }
        //set up functionality for full inventory
        return null;
    }

    public void SwapItems(InventorySlot item1, InventorySlot item2)
    {
        // if(/*item2.CanPlaceInSlot(item1.ItemObject)  && */ item1.CanPlaceInSlot(item2.ItemObject))
        // {
            InventorySlot temp = new InventorySlot( item2.item, item2.amount);
            item2.UpdateSlot(item1.item, item1.amount);
            item1.UpdateSlot(temp.item, temp.amount);
            //}
    }
    
    [ContextMenu("Clear")]
    public void Clear()
    {
        container.Clear();
        //categoryContainer.Clear();
    }
}


public delegate void SlotUpdated(InventorySlot _slot);

