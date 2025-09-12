using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject playButton;
    public GameObject configButton;
    public GameObject configPanel;

    public void PlayGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
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
