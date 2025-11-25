using UnityEngine;

public class InventoryUIManager : MonoBehaviour
{
    public GameObject inventory;
    [SerializeField] private CameraManager cameraManager;

    public void OpenInventory()
    {
        inventory.SetActive(!inventory.activeSelf);
        cameraManager.inventoryOpen = !cameraManager.inventoryOpen;
    }
}
