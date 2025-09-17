using System.Drawing;
using UnityEngine;

public class PetNameRandomizer : MonoBehaviour
{
    private string nome;
    private string[] nomesRandom;
    public string desejo;
    public float timer;
    private bool hasItem;
    public GameObject acessorio;
    void Start()
    {
        nomesRandom = new string[4];
        nomesRandom[0] = "kiara";
        nomesRandom[1] = "chefia";
        nomesRandom[2] = "sury";
        nomesRandom[3] = "amendoa";

        nomeRandomizer();
    }

    void Update()
    {
        if(hasItem)
        {
            acessorio.SetActive(true);
        }

        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            Destroy(gameObject);
            Debug.Log(nome + " foi buscado mais cedo");
        }
    }

    void nomeRandomizer()
    {
        nome = nomesRandom[Random.Range(0, nomesRandom.Length)];
    }
}
