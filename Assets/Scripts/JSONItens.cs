using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class JSONItens : MonoBehaviour
{
    public TextAsset textJSON;
    private int index;

    [Header("Texto")]
    private TextMeshProUGUI nameText;
    private TextMeshProUGUI descricaoText;
    private TextMeshProUGUI priceText;


    [System.Serializable]
    public class Item
    {
        public string name;
        public string descricao;
        public string prefabName;
        public int price;
        public bool activated;
    }

    [System.Serializable]
    public class ItemList
    {
        public Item[] item;
    }

    public ItemList myItemList = new ItemList();

    void Start()
    {
        myItemList = JsonUtility.FromJson<ItemList>(textJSON.text);

        GameObject nameTextObj = GameObject.Find("Nome");
        nameText = nameTextObj.GetComponent<TextMeshProUGUI>();

        GameObject descTextObj = GameObject.Find("Descrição");
        descricaoText = descTextObj.GetComponent<TextMeshProUGUI>();

        GameObject priceTextObj = GameObject.Find("Preço");
        priceText = priceTextObj.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        
    }
}
