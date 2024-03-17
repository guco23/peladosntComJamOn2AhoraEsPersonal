using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ChangeScene : MonoBehaviour
{

    [SerializeField] int sceneID;


    public void LoadLevel(int sceneID)
    {
        if (sceneID == 2)
        {
            PlacementSystem.currentLevel = 0;
        }
        SceneManager.LoadScene(sceneID);
    }

   
    public void Exit()
    {
        Application.Quit();
    }
}
