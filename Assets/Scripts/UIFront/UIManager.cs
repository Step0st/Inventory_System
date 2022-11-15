using System;
using Game.Mechanics;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    //[SerializeField] private CategoriesWindow _categories;
    [SerializeField] private InventoryWindow _inventoryWindow;
    [SerializeField] private ShopWindow _shopWindow;
    [SerializeField] private GameWindow _gameWindow;
    [SerializeField] private TextMeshProUGUI _moneyText;
    
    bool inventoryActive = false;
    bool shopActive = false;

    private void Start()
    {
        _gameWindow.InventoryEvent += () =>
        {
            if (!inventoryActive)
            {
                _inventoryWindow.gameObject.SetActive(true);
                inventoryActive = true;
            }
            else
            {
                _inventoryWindow.gameObject.SetActive(false);
                inventoryActive = false;
            }
        };
        
        _gameWindow.ShopEvent += () =>
        {
            if (!shopActive)
            {
                _shopWindow.gameObject.SetActive(true);
                shopActive = true;
            }
            else
            {
                _shopWindow.gameObject.SetActive(false);
                shopActive = false;
            }
        };

        _inventoryWindow.consumablesEvent += () =>
        {
            SwitchInventoryWindow(_inventoryWindow.consumablesWindow);
            SwitchShopWindow(_shopWindow.consumablesWindow);
        };
        
        _inventoryWindow.weaponsEvent += () =>
        {
            SwitchInventoryWindow(_inventoryWindow.weaponsWindow);
            SwitchShopWindow(_shopWindow.weaponsWindow);
        };
        _inventoryWindow.armorEvent += () =>
        {
            SwitchInventoryWindow(_inventoryWindow.armorWindow);
            SwitchShopWindow(_shopWindow.armorWindow);
        };
        _shopWindow.consumablesEvent += () =>
        {
            SwitchShopWindow(_shopWindow.consumablesWindow);
            SwitchInventoryWindow(_inventoryWindow.consumablesWindow);
        };
        _shopWindow.weaponsEvent += () =>
        {
            SwitchShopWindow(_shopWindow.weaponsWindow);
            SwitchInventoryWindow(_inventoryWindow.weaponsWindow);
        };
        _shopWindow.armorEvent += () =>
        {
            SwitchShopWindow(_shopWindow.armorWindow);
            SwitchInventoryWindow(_inventoryWindow.armorWindow);
        };

    }
    
    public void SwitchInventoryWindow(GameObject windowToOpen)
    {
        _inventoryWindow.consumablesWindow.gameObject.SetActive(false);
        _inventoryWindow.weaponsWindow.gameObject.SetActive(false);
        _inventoryWindow.armorWindow.gameObject.SetActive(false);
        windowToOpen.gameObject.SetActive(true);
    }
    
    public void SwitchShopWindow(GameObject windowToOpen)
    {
        _shopWindow.consumablesWindow.gameObject.SetActive(false);
        _shopWindow.weaponsWindow.gameObject.SetActive(false);
        _shopWindow.armorWindow.gameObject.SetActive(false);
        windowToOpen.gameObject.SetActive(true);
    }

    private void Update()
    {
        _moneyText.text = Player.money.ToString();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitHelper.Exit();
        }
    }
} 
