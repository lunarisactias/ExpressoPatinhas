using System;
using UnityEngine;
using static UnityEditor.Progress;

public class StoreManager : MonoBehaviour
{
    public static event Action<StoreItemDto> OnPurchaseSucceeded;
    public static event Action<StoreItemDto, string> OnPurchaseFailed;

    [SerializeField] private CoinsManager coinsManager;
    [SerializeField] private StoreDatabase storeDB;
    [SerializeField] private Transform decoParent;

    public void TryPurchase(StoreItemDto item)
    {

        if (item == null) { OnPurchaseFailed?.Invoke(item, "item invalido"); return;}
        if (item.purchased) { OnPurchaseFailed?.Invoke(item, "item já comprado"); return;}

        if (!coinsManager.canAfford(item.price))
        {
            OnPurchaseFailed?.Invoke(item, "Moedas insuficientes");
            return;
        }

        if (!coinsManager.TryDebit(item.price))
        {
            OnPurchaseFailed?.Invoke(item, "Falha ao debitar moedas.");
            return;
        }

        storeDB.SavePurchase(item.id);
        item.purchased = true;


        if (!string.IsNullOrEmpty(item.prefabpatch))
        {
            var prefab = Resources.Load<GameObject>(item.prefabpatch);
            if (prefab != null) Instantiate(prefab, decoParent);
        }

        //switch (item.upgrade.key)
        //{
        //    case UpgradeKey.BetterClick:
        //        int valueInt = (int)item.upgrade.value;
        //        coinsManager.clickPower += valueInt;
        //        break;
        //}

        OnPurchaseSucceeded?.Invoke(item);
    }
}
