using NUnit.Framework.Constraints;
using System;
using System.
using UnityEngine;

public class CoinsManager : MonoBehaviour
{
    [SerializeField] private int initialCoins;
    [SerializeField] private CoinsDisplay coinsDisplay;
    //public float initialAutoClickTimer /*{ get => PlayerPrefs.GetFloat(AutoClickSpeedKey); set { PlayerPrefs.SetFloat(AutoClickSpeedKey, value)} } */= 4f;
    private float autoClickTimer = 4f;
    //public int autoClickPower = 1;
    const string CoinsKey = "WALLET_COINS";
    const string ClickKey = "CLICK_POWER";
    const string AutoClickKey = "AUTO_CLICK_ACTIVATED";
    const string AutoClickSpeedKey = "AUTO_CLICK_SPEED";
    const string AutoClickPowerKey = "AUTO_CLICK_POWER";

    private void Update()
    {
        if (!autoClickON)
        {
            return;
        }
        else
        {
            AutoClickerTimer();
        }
    }

    #region variaveis pt 2

    public bool AutoClick 
    {
        get => PlayerPrefs.GetString();
        set;
    }

    public float AutoClickTimerStart
    {
        get => 4 - PlayerPrefs.GetFloat(AutoClickSpeedKey);
        set { PlayerPrefs.SetFloat(AutoClickSpeedKey, value); PlayerPrefs.Save(); }
    }

    public int AutoClickPower
    {
        get => PlayerPrefs.GetInt(AutoClickPowerKey);
        set { PlayerPrefs.SetInt(AutoClickPowerKey, value); PlayerPrefs.Save(); }
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
        Coins = Coins + 1 + ClickPower;
        coinsDisplay.UpdateCoins();
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
        autoClickTimer = AutoClickTimerStart;
    }
}
