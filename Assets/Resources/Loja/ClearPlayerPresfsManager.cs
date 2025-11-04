using UnityEngine;

public class ClearPlayerPresfsManager : MonoBehaviour
{
    public bool clearOnStart = false;
    private void Awake()
    {
        if (clearOnStart)
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            Debug.Log("PlayerPrefs limpos com sucesso");
        }
    }
}
