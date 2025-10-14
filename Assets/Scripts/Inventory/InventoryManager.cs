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

    private void Start()
    {
        slots = new GameObject[slotsHolder.transform.childCount];
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = slotsHolder.transform.GetChild(i).gameObject;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            AddItem(itemsToAdd[Random.Range(0, itemsToAdd.Length)]);
            RefreshUI();
        }
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            RemoveItem(itemsToAdd[Random.Range(0, itemsToAdd.Length)]);
        }
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
                }
                slots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
            }
        }
    }

    public void AddItem(ItemClass item)
    {
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
                RefreshUI(); 
            }

        }

        Debug.Log($"Added item: {item.itemName}");
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

                items.Remove(slotToRemove);
            }
        }
        else
        {
            return false;
        }
        if (items.Count >= minimumSlots && slotsHolder.transform.childCount > minimumSlots)
        {
            Destroy(slotsHolder.transform.GetChild(slotsHolder.transform.childCount - 1).gameObject);
        }

        RefreshUI();
        return true;
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
