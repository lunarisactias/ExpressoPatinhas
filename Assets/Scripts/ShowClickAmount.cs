using System;
using TMPro;
using UnityEngine;

public class ShowClickAmount : MonoBehaviour
{
    private CoinsManager coinManager;
    private float disappearTimer = 1.3f;
    private float speed = 1.4f;
    private TextMeshProUGUI tmp;
    private float alphaValue = 1;

    void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        coinManager = GameObject.Find("GameManager").GetComponent<CoinsManager>();
    }

    void Update()
    {
        tmp.color = new Color(0, 0, 0, alphaValue);
        float fadeOutSpeed = 0.008f;
        alphaValue -= fadeOutSpeed;

        transform.Translate(Vector2.up * Time.deltaTime * speed);
        tmp.text = "+" + (coinManager.ClickPower + 1);

        disappearTimer -= Time.deltaTime;
        if (disappearTimer < 0) { Destroy(gameObject); }
    }
}
