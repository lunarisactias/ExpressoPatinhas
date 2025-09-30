using System;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.ComponentModel;
using System.Collections;

public class Animal : MonoBehaviour
{
    [Header("Animal Info")]
    public string uniqueId;
    [SerializeField] private string animalName;
    [SerializeField] private int age;
    [SerializeField] private Species specie;
    [SerializeField] private Colors color;

    [Header("Animal Stats")]
    [Range(0f, 100f)]
    [SerializeField] private float happiness;
    [Range(0f, 100f)]
    [SerializeField] private float hunger;

    [Header("Animal UI")]
    [SerializeField] private Slider hungerLevel;
    [SerializeField] private Slider happinessLevel;
    [SerializeField] private TMPro.TextMeshProUGUI nameText;

    [Header("Animal Stats Settings")]
    [SerializeField] private float timeInSecondsToHungerFull = 86400f; // 24 hours
    [SerializeField] private float timeInSecondToHappinessEmpty = 14400f; // 4 hours

    [Header("Animal Movement")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private Collider2D movementArea;

    private float hungerRate;
    private float happinessRate;
    private Rigidbody2D rb;

    public enum Species
    {
        Gato,
        Cachorro,
        Cabra,
        Galinha,
        Ouriço,
        Guaxinim,
        Lontra,
        [Description("Panda Vermelho")]
        Panda_Vermelho,
        Carpa,
        Coelhinho,
        Veado
    }

    public enum Colors
    {
        Branco,
        Preto,
        Cinza,
        Marrom,
        Laranja,
        Rajado,
        Misturado
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        hungerRate = 100f / timeInSecondsToHungerFull;
        happinessRate = 100f / timeInSecondToHappinessEmpty;
        nameText.text = animalName;
        LoadData();
        StartCoroutine(MoveRandomly());
    }

    private void Update()
    {
        AnimalHUD();
        Hunger();
        ClampStats();
    }

    IEnumerator MoveRandomly()
    {
        while (true)
        {
            Debug.Log("Iniciando movimento aleatório");

            Vector2 newPos = new Vector2(
                UnityEngine.Random.Range(movementArea.bounds.min.x, movementArea.bounds.max.x),
                UnityEngine.Random.Range(movementArea.bounds.min.y, movementArea.bounds.max.y)
            );

            rb.MovePosition(Vector2.MoveTowards(rb.position, newPos, moveSpeed * Time.deltaTime));

            yield return new WaitForSeconds(UnityEngine.Random.Range(1f, 2f));
        }
    }

    public void FeedAnimal(float foodAmount)
    {
        hunger -= foodAmount;
        if (hunger < 0) hunger = 0;
        happiness += foodAmount * 0.1f;
        if (happiness > 100) happiness = 100;
    }

    public void PlayWithToy(float funAmount)
    {
        happiness += funAmount;
        hunger += funAmount * 0.1f;
    }

    public void SetName(string animalName)
    {
        this.animalName = animalName;
    }

    public void Hunger()
    {
        hunger += Time.deltaTime * hungerRate;
        if (hunger > 80) happiness -= Time.deltaTime * happinessRate;
    }

    private void ClampStats()
    {
        if (hunger > 100f) hunger = 100f;
        if (hunger < 0f) hunger = 0f;
        if (happiness > 100f) happiness = 100f;
        if (happiness < 0f) happiness = 0f;
    }

    private void AnimalHUD()
    {
        hungerLevel.value = hunger;
        happinessLevel.value = happiness;
    }

    public void SaveData()
    {
        string hungerKey = uniqueId + "_Hunger";
        string happinessKey = uniqueId + "_Happiness";
        string timeKey = uniqueId + "_LastSessionTime";

        PlayerPrefs.SetFloat(hungerKey, hunger);
        PlayerPrefs.SetFloat(happinessKey, happiness);

        string lastSessionTime = DateTime.UtcNow.ToBinary().ToString();
        PlayerPrefs.SetString(timeKey, lastSessionTime);

        PlayerPrefs.Save();
    }

    public void LoadData()
    {
        string hungerKey = uniqueId + "_Hunger";
        string happinessKey = uniqueId + "_Happiness";
        string timeKey = uniqueId + "_LastSessionTime";

        if (PlayerPrefs.HasKey(timeKey))
        {
            hunger = PlayerPrefs.GetFloat(hungerKey);
            happiness = PlayerPrefs.GetFloat(happinessKey);

            long tempTime = Convert.ToInt64(PlayerPrefs.GetString(timeKey));
            DateTime lastSession = DateTime.FromBinary(tempTime);
            TimeSpan timePassed = DateTime.UtcNow.Subtract(lastSession);
            float secondsPassed = (float)timePassed.TotalSeconds;

            Debug.Log($"Minutos desde a última sessão: {secondsPassed/60} minutos");

            float hungerGained = secondsPassed * hungerRate;
            hunger += hungerGained;

            if (hunger - hungerGained > 80f)
            {
                happiness -= secondsPassed * happinessRate;
            }
            else if (hunger > 80f)
            {
                float hungerNeededToReach80 = 80f - (hunger - hungerGained);
                float timeToReach80 = hungerNeededToReach80 / hungerRate;
                float timeAbove80 = secondsPassed - timeToReach80;
                if (timeAbove80 > 0)
                {
                    happiness -= timeAbove80 * happinessRate;
                }
            }

            ClampStats();
        }
    }

    void OnApplicationQuit()
    {
        if (!string.IsNullOrEmpty(uniqueId))
        {
            SaveData();
        }
    }
}
