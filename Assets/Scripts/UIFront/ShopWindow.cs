using System;
using UnityEngine;

public class ShopWindow : MonoBehaviour
{
    public GameObject consumablesWindow;
    public GameObject weaponsWindow;
    public GameObject armorWindow;
    
    public Action consumablesEvent;
    public Action weaponsEvent;
    public Action armorEvent;
    

    public void OnConsumablesOpen()
    {
        consumablesEvent?.Invoke();
    }

    public void OnWeaponOpen()
    {
        weaponsEvent?.Invoke();
    }
    
    public void OnArmorOpen()
    {
        armorEvent?.Invoke();
    }
}