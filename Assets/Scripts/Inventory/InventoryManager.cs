using UnityEngine;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public ItemClass itemToAdd;
    public ItemClass itemToAdd2;
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private GameObject slotsHolder;
    [SerializeField] private int minimumSlots;
    [SerializeField] private List<ItemClass> items = new List<ItemClass>();
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
            AddItem(itemToAdd);
            RefreshUI();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            AddItem(itemToAdd2);
            RefreshUI();
        }
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            if (items.Count > 0)
            {
                RemoveItem(items[items.Count - 1]);
                RefreshUI();
            }
        }
    }

    public void RefreshUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            var image = slots[i].transform.GetChild(0).GetComponent<UnityEngine.UI.Image>();
            if (i < items.Count)
            {
                image.enabled = true;
                image.sprite = items[i].itemIcon;
            }
            else
            {
                image.sprite = null;
                image.enabled = false;
            }
        }
    }

    public void AddItem(ItemClass item)
    {
        if (items.Count >= slotsHolder.transform.childCount)
        {
            Instantiate(slotPrefab, slotsHolder.transform);
            slots = new GameObject[slotsHolder.transform.childCount];
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i] = slotsHolder.transform.GetChild(i).gameObject;
            }
        }

        items.Add(item);

        Debug.Log($"Added item: {item.itemName}");
    }

    public void RemoveItem(ItemClass item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            Debug.Log($"Removed item: {item.itemName}");
        }
        if (items.Count >= minimumSlots && slotsHolder.transform.childCount > minimumSlots)
        {
            Destroy(slotsHolder.transform.GetChild(slotsHolder.transform.childCount - 1).gameObject);
        }
    }
}
