using System;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public CameraManager cameraManager;
    public GameObject[] areasHUD;
    public GameObject desejo;
    public GameObject pet;
    public GameObject clicker;
    public GameObject loja;
    public GameObject trem;

    void Start()
    {
        
    }

    void Update()
    {
        switch (cameraManager.currentPointIndex)
        {
            case 0:
                Desejos();
                break;

            case 1:
                Pets();
                break;

            case 2:
                Clicker();
                break;

            case 3:
                Loja();
                break;

            case 4:
                Trem();
                break;
        }
    }

    void Desejos()
    {
        desejo.SetActive(true);
        pet.SetActive(false);
        clicker.SetActive(false);
        loja.SetActive(false);
        trem.SetActive(false);
    }

    void Pets()
    {
        desejo.SetActive(false);
        pet.SetActive(true);
        clicker.SetActive(false);
        loja.SetActive(false);
        trem.SetActive(false);
    }

    void Clicker()
    {
        desejo.SetActive(false);
        pet.SetActive(false);
        clicker.SetActive(true);
        loja.SetActive(false);
        trem.SetActive(false);
    }

    void Loja()
    {
        desejo.SetActive(false);
        pet.SetActive(false);
        clicker.SetActive(false);
        loja.SetActive(true);
        trem.SetActive(false);
    }

    void Trem()
    {
        desejo.SetActive(false);
        pet.SetActive(false);
        clicker.SetActive(false);
        loja.SetActive(false);
        trem.SetActive(true);
    }
}
