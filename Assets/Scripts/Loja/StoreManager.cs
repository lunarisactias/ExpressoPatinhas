using System;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    public static event Action<StoreItemDto> OnPurchaseSucceeded;
    public static event Action<StoreItemDto, string> OnPurchaseFailed;

    [SerializeField] private CoinsManager coinsManager;
    [SerializeField] private StoreDatabase storeDB;

    public void TryPurchase(StoreItemDto item)
    {
        if (item == null) { OnPurchaseFailed?.Invoke(item, "item invalido"); return;}
        if (item.purchased) { OnPurchaseFailed?.Invoke(item, "item já comprado"); return;}

        if (!coinsManager.canAfford(item.price))
        {
            OnPurchaseFailed?.Invoke(item, "Moedas insuficientes");
            return;
        }

        storeDB.SavePurchase(item.id);
        item.purchased = true;
    }
}
