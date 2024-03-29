using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuComponent : MonoBehaviour
{
    [SerializeField] GameObject sceneID;
    [SerializeField] GameObject pauseButton;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject controlsPanel1;
    [SerializeField] GameObject controlsPanel2;
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

    public void RestartLevel(int id)
    {
        Time.timeScale = 1.0f;
        sceneID.GetComponent<ChangeScene>().LoadLevel(id);
    }

    public void ControlesOn()
    {
        controlsPanel1.SetActive(true);
    }
    public void ControlesOff()
    {
        controlsPanel1.SetActive(false);
    }
    public void ChangePage1()
    {
        controlsPanel1.SetActive(false);
        controlsPanel2.SetActive(true);
    }
    public void ChangePage2()
    {
        controlsPanel1.SetActive(true);
        controlsPanel2.SetActive(false);
    }

}
