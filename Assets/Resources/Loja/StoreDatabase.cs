using System.Collections.Generic;
using UnityEngine;

public class StoreDatabase : MonoBehaviour
{
    [SerializeField] private string jsonResourcePath = "Loja/JSON_Item";
    private Dictionary<string, StoreItemDto> _map;

    [SerializeField] private bool clearOnStart = false;

    const string PurchasedKey = "STORE_PURCHASED_IDS";

    private void Awake()
    {
        if(clearOnStart)
        {
            PlayerPrefs.DeleteKey(PurchasedKey);
            PlayerPrefs.Save();
        }
    }

    public IReadOnlyList<StoreItemDto> LoadAll()
    {
        TextAsset ta = Resources.Load<TextAsset>(jsonResourcePath);
        if (ta == null)
        {
            Debug.Log("Objeto JSON não encontrado");
            return new List<StoreItemDto>();
        }

        var wrapper = JsonUtility.FromJson<StoreItemWrapper>(ta.text);
        if (wrapper?.item == null) wrapper = new StoreItemWrapper {item = new List<StoreItemDto>()};

        var purchasedCsv = PlayerPrefs.GetString(PurchasedKey, "");
        var purchasedSet = new HashSet<string>(purchasedCsv.Split(',', System.StringSplitOptions.RemoveEmptyEntries));

        foreach (var it in wrapper.item)
        {
            it.purchased = purchasedSet.Contains(it.id);
        }

        return wrapper.item;
    }

    public void SavePurchase(string id, string prefabName)
    {
        var purchasedCsv = PlayerPrefs.GetString(PurchasedKey,"");
        var set = new HashSet<string>(purchasedCsv.Split(',', System.StringSplitOptions.RemoveEmptyEntries));
        if (set.Add(id))
        {
            PlayerPrefs.SetString(PurchasedKey, string.Join(',', set));
            PlayerPrefs.Save();
        }
    }
}
