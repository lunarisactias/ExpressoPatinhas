using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tutorial : MonoBehaviour
{
    public bool playedTutorial = false;
    public int index = 0;
    private float textSpeed;
    [SerializeField] private GameObject balaoFala;
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
    [SerializeField] private string[] tutorialTxt;
    void Start()
    {
        textMeshProUGUI.GetComponent<TextMeshProUGUI>();
        textMeshProUGUI.text = string.Empty;
        textSpeed = 0.05f;
        StartCoroutine(TextWrite());
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == UnityEngine.TouchPhase.Began)
        {
            if(textMeshProUGUI.text == tutorialTxt[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textMeshProUGUI.text = tutorialTxt[index];
            }
        }

        if (playedTutorial)
        {
            Destroy(gameObject);
        }

        SpeechBubble();
    }

    void SpeechBubble()
    {
        if(index == 5)
        {
            balaoFala.SetActive(false);
        }
        else
        {
            balaoFala.SetActive(true);
        }
    }

    IEnumerator TextWrite()
    {
        foreach (char c in tutorialTxt[index].ToCharArray())
        {
            textMeshProUGUI.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < tutorialTxt.Length - 1)
        {
            index++;
            textMeshProUGUI.text = string.Empty;
            StartCoroutine(TextWrite());
        }
        else
        {
            playedTutorial = true;
        }
    }

    void BotãoTutorial()
    {

    }
}
