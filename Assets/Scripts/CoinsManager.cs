using NUnit.Framework.Constraints;
using System;
using System.Threading;
using UnityEngine;

public class CoinsManager : MonoBehaviour
{
    [SerializeField] private int initialCoins;
    [SerializeField] private CoinsDisplay coinsDisplay;
    [SerializeField] private GameObject amountPrefab;
    [SerializeField] private Transform canvasParent;

    [SerializeField] private float autoClickTimer = 4f;
    [SerializeField] private float secondAutoClickTimer = 20f;
    public static CoinsManager Instance { get; private set; }
    const string CoinsKey = "WALLET_COINS";
    const string ClickKey = "CLICK_POWER";
    const string AutoClickKey = "AUTO_CLICK_ACTIVATED";
    const string AutoClickSpeedKey = "AUTO_CLICK_SPEED";
    const string AutoClickPowerKey = "AUTO_CLICK_POWER";
    const string SecondAutoClickKey = "2ND_AUTO_CLICK_KEY";
    const string SecondAutoClickSpeedKey = "2ND_AUTO_CLICK_SPEED_KEY";
    const string SecondAutoClickPowerKey = "2ND_AUTO_CLICK_POWER_KEY";

    private void Update()
    {
        if (AutoClickON == 1) { AutoClickerTimer(); }
        if (SecondAutoClickON == 1) { SecondAutoClick(); }
    }

    #region variaveis pt 2
    public int AutoClickON
    {
        get => PlayerPrefs.GetInt(AutoClickKey);
        set { PlayerPrefs.SetInt(AutoClickKey, value); PlayerPrefs.Save(); }
    }
    public float AutoClickTimerStart
    {
        get => PlayerPrefs.GetFloat(AutoClickSpeedKey);
        set { PlayerPrefs.SetFloat(AutoClickSpeedKey, value); PlayerPrefs.Save(); }
    }

    public int AutoClickPower
    {
        get => PlayerPrefs.GetInt(AutoClickPowerKey);
        set { PlayerPrefs.SetInt(AutoClickPowerKey, value); PlayerPrefs.Save(); }
    }

    public int SecondAutoClickON
    {
        get => PlayerPrefs.GetInt(SecondAutoClickKey);
        set { PlayerPrefs.SetInt(SecondAutoClickKey, value); PlayerPrefs.Save(); }
    }

    public float SecondAutoClickTimerStart
    {
        get => PlayerPrefs.GetFloat(SecondAutoClickSpeedKey);
        set { PlayerPrefs.SetFloat(SecondAutoClickSpeedKey, value); PlayerPrefs.Save(); }
    }

    public int SecondAutoClickPower
    {
        get => PlayerPrefs.GetInt(SecondAutoClickPowerKey);
        set { PlayerPrefs.SetInt(SecondAutoClickPowerKey, value); PlayerPrefs.Save(); }
    }


    public int ClickPower
    {
        get => PlayerPrefs.GetInt(ClickKey);
        set { PlayerPrefs.SetInt(ClickKey, value); PlayerPrefs.Save(); }
    }

    public int Coins
    {
        get => PlayerPrefs.GetInt(CoinsKey, initialCoins);
        private set { PlayerPrefs.SetInt(CoinsKey, value); PlayerPrefs.Save(); }
    }

    #endregion

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        coinsDisplay.UpdateCoins();
    }

    public bool canAfford(int cost) => Coins >= cost;

    public bool TryDebit(int cost)
    {
        if (!canAfford(cost)) return false;
        Coins -= cost;
        coinsDisplay.UpdateCoins();
        return true;
    }

    // opcional: método para adicionar moedas (recompensas do jogo)
    public void Add()
    {
        Coins = Coins + 1 + ClickPower;
        coinsDisplay.UpdateCoins();

        Vector2 clickPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        Instantiate(amountPrefab, clickPos, Quaternion.identity, canvasParent);
    }

    public void AutoClickerTimer()
    {
        autoClickTimer -= Time.deltaTime;
        if (autoClickTimer < 0) 
        { 
            AutoClick(); 
        }
    }

    public void AutoClick()
    {
        Coins = Coins + 1 + AutoClickPower;
        coinsDisplay.UpdateCoins();
        autoClickTimer = 4 - AutoClickTimerStart;
    }

    public void SecondAutoClick()
    {
        secondAutoClickTimer -= Time.deltaTime;

        if (secondAutoClickTimer < 0)
        {
            Coins = Coins + SecondAutoClickPower;
            coinsDisplay.UpdateCoins();
            secondAutoClickTimer = SecondAutoClickTimerStart;
        }
     
    }
}
