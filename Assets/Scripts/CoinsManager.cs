using UnityEngine;

public class CoinsManager : MonoBehaviour
{
    [SerializeField] private int initialCoins;
    const string CoinsKey = "WALLET_COINS";

    public int Coins
    {
        get => PlayerPrefs.GetInt(CoinsKey, initialCoins);
        private set { PlayerPrefs.SetInt(CoinsKey, value); PlayerPrefs.Save(); }
    }

    public bool canAfford(int cost) => Coins >= cost;

    public void Add(int amount) => Coins = Mathf.Max(0, Coins + amount);
}
