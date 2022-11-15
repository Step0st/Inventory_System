 using System;
 using UnityEngine;

 public class MoneyHelper  : MonoBehaviour
 {
     public void OnAddMoney()
     {
         TradingSystem.Instance.AddMoney(25);
     }

     public void OnRemoveMoney()
     {
         TradingSystem.Instance.RemoveMoney(25);
     } 
 }
