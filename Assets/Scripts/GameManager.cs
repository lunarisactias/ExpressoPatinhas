using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("CLICKER")]
    public int coins { get; private set; }

    public int clickAmount { get; private set; } = 1;
    public int autoClickAmount { get; private set; } = 0;
    private bool autoClick = false;
    private float autoClickTimer = 0f;
    private float autoClickTimerDelay = 2f;
    public int indexUpgrade;

    [Header("texto :)")]
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI autoclickerText;
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        coinText.text = "Carinho: " + coins;
        autoclickerText.text = "autoclicker: " + autoClickAmount;

        autoClickTimer += Time.deltaTime;

        if (autoClick)
        {
            if(autoClickTimer >= autoClickTimerDelay)
            {
                coins += autoClickAmount;
                autoClickTimer = 0; 
            }
        }
    }

    public void AddCoin()
    {
        coins += clickAmount;
    }


    public void UpgradeClick(ShopButton button)
    {
        clickAmount++;
        coins -= button.price;
        button.price = Mathf.CeilToInt(button.price * button.priceMultiplier);
        indexUpgrade++;
    }

    public void FasterAutoClick(ShopButton button)
    {
        if (autoClick)
        {
            autoClickTimerDelay /= 1.05f;
            coins -= button.price;
            button.price = Mathf.CeilToInt(button.price * button.priceMultiplier);
            indexUpgrade++;
        }

    }

    public void AutoClick(ShopButton button)
    {
        autoClickAmount++;
        autoClick = true;
        coins -= button.price;
        button.price = Mathf.CeilToInt(button.price * button.priceMultiplier);
        indexUpgrade++;
    }
}
