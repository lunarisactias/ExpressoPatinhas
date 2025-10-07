using UnityEngine;

public class ShopUpgrades : MonoBehaviour
{
    [SerializeField] private GameObject[] decoracoes;
    private GameManager gameManager;
    void Start()
    {
        GameObject gameManagerObj = new GameObject();
        gameManagerObj = GameObject.Find("GameManager");
        gameManager = gameManagerObj.GetComponent<GameManager>();

    }

    void Update()
    {
        Decorations();
    }

    void Decorations()
    {
        
    }
}
