using System;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Animal : MonoBehaviour
{
    [Header("Animal Info")]
    public string uniqueId;
    [SerializeField] private string animalName;
    [SerializeField] private int age;
    [SerializeField] private string species;
    [SerializeField] private string color;

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
    private float hungerRate;
    private float happinessRate;

    private void Awake()
    {
        hungerRate = 100f / timeInSecondsToHungerFull;
        happinessRate = 100f / timeInSecondToHappinessEmpty;
        nameText.text = animalName;
        LoadData();
    }

    private void Update()
    {
        hungerLevel.value = hunger;
        happinessLevel.value = happiness;
        Hunger();
        ClampStats();
    }

    public void Feed(float foodAmount)
    {
        hunger -= foodAmount;
        if (hunger < 0) hunger = 0;
        happiness += foodAmount * 0.1f;
        if (happiness > 100) happiness = 100;
    }

    public void PlayWith(float funAmount)
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

    void OnApplicationQuit()
    {
        if (!string.IsNullOrEmpty(uniqueId))
        {
            SaveData();
        }
    }

    public void SaveData()
    {
        string hungerKey = uniqueId + "_Hunger";
        string happinessKey = uniqueId + "_Happiness";
        string timeKey = uniqueId + "_LastSessionTime";

        Debug.Log($"Salvando dados para o ID: {uniqueId}");
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
            Debug.Log($"Carregando dados para o ID: {uniqueId}");
            hunger = PlayerPrefs.GetFloat(hungerKey);
            happiness = PlayerPrefs.GetFloat(happinessKey);

            long tempTime = Convert.ToInt64(PlayerPrefs.GetString(timeKey));
            DateTime lastSession = DateTime.FromBinary(tempTime);
            TimeSpan timePassed = DateTime.UtcNow.Subtract(lastSession);
            float secondsPassed = (float)timePassed.TotalSeconds;

            Debug.Log($"Tempo desde a última sessão: {secondsPassed} segundos");

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
        else
        {
            Debug.Log($"Nenhum dado salvo encontrado para o ID: {uniqueId}.");
        }
    }
}
