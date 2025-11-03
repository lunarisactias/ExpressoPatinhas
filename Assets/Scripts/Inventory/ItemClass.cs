using UnityEngine;

public abstract class ItemClass : ScriptableObject
{
    [Header("Item Properties")]
    public string itemName;
    public Sprite itemIcon;
    public GameObject itemPrefab;
    public int maxStack = 999;

    public abstract ItemClass GetItem();
    public abstract ToyClass GetToy();
    public abstract FoodClass GetFood();
}
