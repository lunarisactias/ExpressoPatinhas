using UnityEngine;

public class CoinsManager : MonoBehaviour
{
    [SerializeField] private int initialCoins;
    [SerializeField] private CoinsDisplay coinsDisplay;
    const string CoinsKey = "WALLET_COINS";

    public int Coins
    {
        get => PlayerPrefs.GetInt(CoinsKey, initialCoins);
        private set { PlayerPrefs.SetInt(CoinsKey, value); PlayerPrefs.Save(); }
    }

    public bool canAfford(int cost) => Coins >= cost;

    public bool TryDebit(int cost)
    {
        if (!canAfford(cost)) return false;
        Coins -= cost;
        return true;
    }

    // opcional: método para adicionar moedas (recompensas do jogo)
    public void Add(int amount)
    {
        Coins = Mathf.Max(0, Coins + amount);
        coinsDisplay.UpdateCoins();
    }
}
