using System;
using UnityEngine;

public class GameWindow : MonoBehaviour
{
    public Action InventoryEvent;

    public Action ShopEvent;
    

    public void OnInventoryOpen()
    {
        InventoryEvent?.Invoke();
    }

    public void OnShopOpen()
    {
        ShopEvent?.Invoke();
    }
    
}