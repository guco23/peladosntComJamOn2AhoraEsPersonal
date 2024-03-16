using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuComponent : MonoBehaviour
{

    [SerializeField] GameObject pauseButton;
    [SerializeField] GameObject pauseMenu;
    public void Pausa()
    {
        Debug.Log("pause Dededne");

        Time.timeScale = 0f;
        pauseButton.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
        pauseButton.SetActive(true);
        pauseMenu.SetActive(false);
    }
}
