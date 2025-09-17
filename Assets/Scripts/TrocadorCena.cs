using UnityEngine;
using UnityEngine.SceneManagement;

public class TrocadorCena : MonoBehaviour
{
    public int cena;
    public void SceneChanger()
        {
            SceneManager.LoadScene(cena);
        }
}
