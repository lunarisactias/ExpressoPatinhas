using UnityEngine;

public class CoinsManager : MonoBehaviour
{
    [SerializeField] private int initialCoins;
    [SerializeField] private CoinsDisplay coinsDisplay;
    public int upgradedClick;
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
    public void Add()
    {
        //varivel que armazena o multiplicador. esse é permanete; salvo no playerPrefs.
        //valor default das moedas = 1;
        // valor de multiplicar. Ex, multiplador x2 = valor defailt x 2 = 2;
        //mult 3x: 1 x 3 = 3
        //os valores dos multiplicadores devem vir do json;


        Coins = Mathf.Max(0, Coins + );
        coinsDisplay.UpdateCoins();
    }
}
