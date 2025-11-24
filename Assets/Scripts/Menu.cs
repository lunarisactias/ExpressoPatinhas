using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject playButton;
    public GameObject configButton;
    public GameObject configPanel;
    [SerializeField] private float animTimer;
    [SerializeField] private bool isPressed;
    [SerializeField] private Animator menuAnim;

    private void Update()
    {
        if(!isPressed) { return; }
        else
        {
            animTimer += Time.deltaTime;
            if(animTimer > 1.2f) { UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene"); }
        }
    }

    public void PlayGame()
    {
        isPressed = true;
        menuAnim.SetBool("Pressed", true);
    }

    public void Config()
    {
        if (playButton.activeSelf)
        {
            playButton.SetActive(false);
        }
        else
        {
            playButton.SetActive(true);
        }   
        if (configPanel.activeSelf)
        {
            configPanel.SetActive(false);
        }
        else
        {
            configPanel.SetActive(true);
        }
    }
}
