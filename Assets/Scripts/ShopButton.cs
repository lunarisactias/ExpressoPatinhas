using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ShopButton : MonoBehaviour
{
    public int price;
    public float priceMultiplier = 1.10f;
    public string upgradeName;
    public TextMeshProUGUI buttonText;

    private void Update()
    {
        buttonText.text = upgradeName + "\n Price: " + price;

        if (GameManager.instance.coins < price)
        {
             gameObject.GetComponent<Button>().interactable = false;
        }
        else
        {
            gameObject.GetComponent<Button>().interactable = true;
        }
    }
}

