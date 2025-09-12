using UnityEngine;

public class Animal : MonoBehaviour
{
    [SerializeField] private string animalName;
    [SerializeField] private float happiness;
    [SerializeField] private float hunger;
    [SerializeField] private int age;
    [SerializeField] private string species;
    [SerializeField] private string color;

    private void Update()
    {
        Hunger();
    }

    private void Start()
    {
        
    }

    public void Feed(float foodAmount)
    {
        hunger -= foodAmount;
        if (hunger < 0) hunger = 0;
        happiness += foodAmount * 0.1f;
        if (happiness > 100) happiness = 100;
    }

    public void SetName(string animalName)
    {
        this.animalName = animalName;
    }

    public void Hunger()
    {
        hunger += Time.deltaTime * 0.01f;
        if (hunger > 100) hunger = 100;
        if (hunger > 80) happiness -= Time.deltaTime * 0.5f;
        if (happiness < 0) happiness = 0;
    }
}
