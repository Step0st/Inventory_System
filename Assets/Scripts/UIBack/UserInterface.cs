using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class UserInterface : MonoBehaviour
{
    public InventoryObject inventory;
    
    [SerializeField] private GameObject _slotPrefab;
    [SerializeField] private UIType _uiType;
    private Dictionary<GameObject, InventorySlot> _slotsOnInterface = new Dictionary<GameObject, InventorySlot>();

    void Start()
    {
        for (int i = 0; i < inventory.GetSlots.Length; i++)
        {
            inventory.GetSlots[i].parent = this;
            inventory.GetSlots[i].OnAfterUpdate += OnSlotUpdate;
        }
        CreateSlots();
        foreach (var slot in inventory.GetSlots)
        {
            OnSlotUpdate(slot);
        }
        
        AddEvent(gameObject, EventTriggerType.PointerEnter, delegate { OnEnterInterface(gameObject); });
        AddEvent(gameObject, EventTriggerType.PointerExit, delegate { OnExitInterface(gameObject); });
        
    }

    public void OnSlotUpdate(InventorySlot slot)
    {
        if (slot.item.Id >= 0)
        { 
            slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().sprite = slot.ItemObject.itemSprite;
            slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
            slot.slotDisplay.GetComponentInChildren<TextMeshProUGUI>().text = slot.amount == 1 ? "" : slot.amount.ToString("n0");
        }
        else
        {
            slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
            slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
            slot.slotDisplay.GetComponentInChildren<TextMeshProUGUI>().text = "";
        }
    }

    public void CreateSlots()
    {
        _slotsOnInterface = new Dictionary<GameObject, InventorySlot>();
        for (int i = 0; i < inventory.GetSlots.Length; i++)
        {
            var obj = Instantiate(_slotPrefab, Vector3.zero, Quaternion.identity, transform);

            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
            AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });

            inventory.GetSlots[i].slotDisplay = obj;
            _slotsOnInterface.Add(obj, inventory.GetSlots[i]);
        }
    }

    protected void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }

    public void OnEnter(GameObject obj)
    {
        MouseData.slotHoveredOver = obj;
        if (_slotsOnInterface[obj].item.Id >= 0)
        {
            TooltipManager.instance.SetAndShowTooltip("Price: " + _slotsOnInterface[obj].item.price.ToString());
        }
    }
    public void OnExit(GameObject obj)
    {
        MouseData.slotHoveredOver = null;
        TooltipManager.instance.HideTooltip();
    }

    public void OnEnterInterface(GameObject obj)
    {
        MouseData.interfaceMouseIsOver = obj.GetComponent<UserInterface>();
    }

    public void OnExitInterface(GameObject obj)
    {
        MouseData.interfaceMouseIsOver = null;
    }
    public void OnDragStart(GameObject obj)
    {
        MouseData.tempItemBeingDragged = CreateTempItem(obj);
    }
    public GameObject CreateTempItem(GameObject obj)
    {
        GameObject tempItem = null;
        if(_slotsOnInterface[obj].item.Id >= 0)
        {
            tempItem = new GameObject();
            var rt = tempItem.AddComponent<RectTransform>();
            rt.sizeDelta = new Vector2(50, 50);
            tempItem.transform.SetParent(transform.parent.parent.parent);
            var img = tempItem.AddComponent<Image>();
            img.sprite = _slotsOnInterface[obj].ItemObject.itemSprite;
            img.raycastTarget = false;
        }
        return tempItem;
    }
    public void OnDragEnd(GameObject obj)
    {
        Destroy(MouseData.tempItemBeingDragged);

        if (MouseData.slotHoveredOver)
        {
            if (MouseData.interfaceMouseIsOver._uiType == UIType.Inventory &&
                _uiType == UIType.Inventory)
            {
                InventorySlot mouseHoverSlotData = MouseData.interfaceMouseIsOver._slotsOnInterface[MouseData.slotHoveredOver];
                inventory.SwapItems(_slotsOnInterface[obj], mouseHoverSlotData);
            }
            
            int itemPrice = _slotsOnInterface[obj].item.price;
            if (MouseData.interfaceMouseIsOver._uiType == UIType.Inventory && _uiType == UIType.Shop 
                && MouseData.interfaceMouseIsOver._slotsOnInterface[MouseData.slotHoveredOver].item.Id < 0)
            {
                if (TradingSystem.Instance.TryToBuy(itemPrice))
                {
                    InventorySlot mouseHoverSlotData = MouseData.interfaceMouseIsOver._slotsOnInterface[MouseData.slotHoveredOver];
                    inventory.SwapItems(_slotsOnInterface[obj], mouseHoverSlotData);
                }
            }
            
            if (MouseData.interfaceMouseIsOver._uiType == UIType.Shop && _uiType == UIType.Inventory 
                && MouseData.interfaceMouseIsOver._slotsOnInterface[MouseData.slotHoveredOver].item.Id < 0)
            {
                TradingSystem.Instance.SellItem(itemPrice);
                InventorySlot mouseHoverSlotData = MouseData.interfaceMouseIsOver._slotsOnInterface[MouseData.slotHoveredOver];
                inventory.SwapItems(_slotsOnInterface[obj], mouseHoverSlotData);
            }
        }
    }
    public void OnDrag(GameObject obj)
    {
        if (MouseData.tempItemBeingDragged != null)
            MouseData.tempItemBeingDragged.GetComponent<RectTransform>().position = Input.mousePosition;
    }
}


