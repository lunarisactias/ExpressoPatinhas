using UnityEngine;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;

public class InventoryManager : MonoBehaviour
{
    public ItemClass[] itemsToAdd;
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private GameObject slotsHolder;
    [SerializeField] private int minimumSlots;
    [SerializeField] private List<SlotClass> items = new List<SlotClass>();
    private GameObject[] slots;
    private Vector2 touchPosition;

    private void Start()
    {
        slots = new GameObject[slotsHolder.transform.childCount];
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = slotsHolder.transform.GetChild(i).gameObject;
        }

        RefreshUI();

        this.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            ItemClass itemToRemove = itemsToAdd[Random.Range(0, itemsToAdd.Length)];
            if (itemToRemove != null) 
            {
                RemoveItem(itemToRemove);
            }
        }

        SpawnItem();
    }

    public void RefreshUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            try
            {
                slots[i].transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().enabled = true;
                slots[i].transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = items[i].GetItem().itemIcon;
                slots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = items[i].GetQuantity().ToString();
            }
            catch
            {
                if (slots[i].transform.GetChild(0).GetComponent<UnityEngine.UI.Image>())
                {
                    slots[i].transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().enabled = false;
                    slots[i].transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = null;
                    slots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
                }
            }
        }
    }

    public void AddItem(ItemClass item)
    {
        var price = item.price;

        if (!CoinsManager.Instance.canAfford(price))
        {
            Debug.Log("Not enough coins to buy this item.");
            return;
        }

        CoinsManager.Instance.TryDebit(price);

        SlotClass slot = ContainsItem(item);

        if (slot != null)
        {
            slot.AddQuantity(1);
        }
        else
        {
            items.Add(new SlotClass(item, 1));

            if (items.Count > slotsHolder.transform.childCount)
            {
                Instantiate(slotPrefab, slotsHolder.transform);

                slots = new GameObject[slotsHolder.transform.childCount];
                for (int i = 0; i < slots.Length; i++)
                {
                    slots[i] = slotsHolder.transform.GetChild(i).gameObject;
                }
            }
        }

        RefreshUI();

        Debug.Log($"Added item: {item.itemName} | Cost: {price}");
    }

    public bool RemoveItem(ItemClass item)
    {
        SlotClass temp = ContainsItem(item);
        if (temp != null)
        {
            if (temp.GetQuantity() > 1)
            {
                temp.SubQuantity(1);
            }
            else
            {
                SlotClass slotToRemove = new SlotClass();
                foreach (SlotClass slot in items)
                {
                    if (slot.GetItem() == item)
                    {
                        slotToRemove = slot;
                        break;
                    }
                }

                if (items.Count >= minimumSlots && slotsHolder.transform.childCount > minimumSlots)
                {
                    Destroy(slotsHolder.transform.GetChild(slotsHolder.transform.childCount - 1).gameObject);
                }

                items.Remove(slotToRemove);
            }
        }
        else
        {
            return false;
        }

        RefreshUI();
        return true;
    }

    public void SpawnItem()
    {
        GameObject itemToSpawn;

        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            SlotClass slotWithItem = GetClosestSlot();
            Vector2 slotPosition;

            int i = items.IndexOf(slotWithItem);
            if (i >= 0)
            {
                slotPosition = slots[i].transform.position;
            }
            else
            {
                slotPosition = Vector2.zero;
            }

            if (touch.phase == TouchPhase.Began && Vector2.Distance(touchPosition, slotPosition) <= 1.5f)
            {
                itemToSpawn = slotWithItem.GetItem().itemPrefab;

                if (itemToSpawn != null)
                {
                    Vector3 spawnLocation = new Vector3(0f, 3.5f, 0f);
                    GameObject spawnedItem = Instantiate(itemToSpawn, spawnLocation, Quaternion.identity);
                    RemoveItem(slotWithItem.GetItem());
                }
            }
        }
    }
    
    public SlotClass GetClosestSlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (Vector2.Distance(slots[i].transform.position, touchPosition) <= 0.3f)
            {
                Debug.Log(items[i].GetItem().itemName);
                return items[i];
            }
        }
        return null;
    }

    public SlotClass ContainsItem(ItemClass item)
    {
        foreach (SlotClass slot in items)
        {
            if (slot.GetItem() == item)
            {
                return slot;
            }
        }

        return null;
    }
}
