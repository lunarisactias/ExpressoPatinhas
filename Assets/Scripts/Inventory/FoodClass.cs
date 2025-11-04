using UnityEngine;

[CreateAssetMenu(fileName = "New Food", menuName = "Inventory/Food")]
public class FoodClass : ItemClass
{
    [Header("Food Specific")]
    public float nutritionValue;

    public override ItemClass GetItem()
    {
        return this;
    }
    public override FoodClass GetFood()
    {
        return this;
    }

    public override ToyClass GetToy()
    {
        return null;
    }

}
