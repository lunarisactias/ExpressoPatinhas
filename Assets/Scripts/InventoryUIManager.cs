using UnityEngine;

public class InventoryUIManager : MonoBehaviour
{
    public GameObject inventory;

    public void OpenInventory()
    {
        inventory.SetActive(!inventory.activeSelf);
    }
}
