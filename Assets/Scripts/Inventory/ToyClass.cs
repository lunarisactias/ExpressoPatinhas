using UnityEngine;

[CreateAssetMenu(fileName = "New Toy", menuName = "Inventory/Toy")]
public class ToyClass : ItemClass
{
    [Header("Toy Specific")]
    public int funValue;

    public override ItemClass GetItem()
    {
        return this;
    }
    public override ToyClass GetToy()
    {
        return this;
    }

    public override FoodClass GetFood()
    {
        return null;
    }

}
