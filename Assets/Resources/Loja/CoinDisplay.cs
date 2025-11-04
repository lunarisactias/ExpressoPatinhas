using TMPro;
using UnityEngine;

public class CoinsDisplay : MonoBehaviour
{
    [SerializeField] private CoinsManager wallet;
    private TMP_Text textCoins;

    private void Awake()
    {
        textCoins = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        UpdateCoins();
        StoreManager.OnPurchaseSucceeded += _ => UpdateCoins();
    }

    private void OnDisable()
    {
        StoreManager.OnPurchaseSucceeded -= _ => UpdateCoins();
    }

    public void UpdateCoins()
    {
        textCoins.text = wallet.Coins.ToString();
    }

}
