using UnityEngine;

public class ShopUpgrades : MonoBehaviour
{
    [SerializeField] private GameObject[] decoracoes;
    void Start()
    {
        decoracoes = GameObject.FindGameObjectsWithTag("Decoração");
    }

    void Update()
    {
        
    }
}
