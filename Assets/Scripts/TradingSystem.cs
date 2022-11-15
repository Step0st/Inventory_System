using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class TradingSystem
{
    private TradingSystem()
    {
    }

    public static TradingSystem Instance { get; } = new TradingSystem();
    
    public void SellItem(int itemPrice) 
    { 
        Player.money += itemPrice;
    }
    
    public bool TryToBuy(int itemPrice) 
    {
        if (Player.money >= itemPrice)
        {
            Player.money -= itemPrice;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AddMoney(int sum)
    {
        Player.money += sum;
    }
    
    public void RemoveMoney(int sum)
    {
        Player.money -= sum;
    }
}