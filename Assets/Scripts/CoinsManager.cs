using UnityEngine;

public class CoinsManager : MonoBehaviour
{
    [SerializeField] private int initialCoins;
    [SerializeField] private CoinsDisplay coinsDisplay;
    public int clickPower;
    public bool autoClickerON = false;
    public float initialAutoClickTimer = 2f;
    private float autoClickTimer = 2f;
    public int autoClickPower = 1;
    const string CoinsKey = "WALLET_COINS";

    private void Update()
    {
        if(!autoClickerON)
        {
            return;
        }
        else
        {
            AutoClickerTimer();
        }
    }

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
    public void Add()
    {
        Coins = Coins + clickPower;
        coinsDisplay.UpdateCoins();
    }

    public void AutoClickerTimer()
    {
        autoClickTimer -= Time.deltaTime;
        if (autoClickTimer < 0) { AutoClick(); }
    }

    public void AutoClick()
    {
        Coins = Coins + autoClickPower;
        coinsDisplay.UpdateCoins();
        autoClickTimer = initialAutoClickTimer;
    }
}
