using System;
using UnityEngine;
using static UnityEditor.Progress;

public class StoreManager : MonoBehaviour
{
    public static event Action<StoreItemDto> OnPurchaseSucceeded;
    public static event Action<StoreItemDto> OnPurchaseFailed;

    private AudioSource audioSource;
    [SerializeField] private AudioClip purchaseSound;
    [SerializeField] private Material[] backgroundImages;

    [SerializeField] private CoinsManager coinsManager;
    [SerializeField] private StoreDatabase storeDB;
    [SerializeField] private Transform decoParent;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void TryPurchase(StoreItemDto item)
    {

        if (item == null) { OnPurchaseFailed?.Invoke(item); return;}
        if (item.purchased) { OnPurchaseFailed?.Invoke(item); return;}

        if (!coinsManager.canAfford(item.price))
        {
            OnPurchaseFailed?.Invoke(item);
            return;
        }

        if (!coinsManager.TryDebit(item.price))
        {
            OnPurchaseFailed?.Invoke(item);
            return;
        }

        storeDB.SavePurchase(item.id);
        item.purchased = true;
        audioSource.PlayOneShot(purchaseSound);


        if (!string.IsNullOrEmpty(item.prefabpatch))
        {
            var prefab = Resources.Load<GameObject>(item.prefabpatch);
            if (prefab != null) Instantiate(prefab, decoParent);
        }

        switch (item.key)
        {
            case "ChangeBackgroundImage":
                int clickValueInt2 = (int)item.value;
                coinsManager.ClickPower += clickValueInt2;


                GameObject clickerScene = GameObject.Find("Background Clicker");

                MeshRenderer clickerMeshRen = clickerScene.GetComponent<MeshRenderer>();

                int backgroundInt = (int)item.value2;
                clickerMeshRen.material = backgroundImages[backgroundInt];
                break;

            case "BetterClick"://case UpgradeKey.BetterClick:
                int clickValueInt = (int)item.value;
                coinsManager.ClickPower += clickValueInt;
                break;

            case "ActivateAutoclick":
                int autoClickOnValueInt = (int)item.value;
                coinsManager.AutoClickON = autoClickOnValueInt;
                break;

            case "FasterAutoclick":
                coinsManager.AutoClickTimerStart -= item.value;
                break;

            case "BetterAutoclick":
                int autoClickvalueInt = (int)item.value;
                coinsManager.AutoClickPower += autoClickvalueInt;
                break;

            case "SecondAutoClick":
                int SecAutoClickValueInt = (int)item.value;
                coinsManager.SecondAutoClickON = SecAutoClickValueInt;

                coinsManager.SecondAutoClickTimerStart = item.value2;

                int SecAutoClickPower = (int)item.value3;
                coinsManager.SecondAutoClickPower += SecAutoClickPower;
                break;
        }

        OnPurchaseSucceeded?.Invoke(item);
    }
}
