using UnityEngine;

public class TremController : MonoBehaviour
{
    public bool hasPets;
    public GameObject[] prefabs;
    public int minAnimais;
    public int maxAnimais;
    void Start()
    {
        
    }

    void Update()
    {
        GameManager gameManager = new GameManager();
        if(gameManager.animais == 0)
        {
            hasPets = false;
        }

        if(hasPets == false)
        {
            NewPets();
        }
    }

    private void NewPets()
    {
        GameManager.instance.animais += Random.Range(minAnimais, maxAnimais);
        Debug.Log(GameManager.instance.animais + " animais sairam do trem");
        hasPets = true;
    }
}
