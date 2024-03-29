using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Animator _animator;


    // Start is called before the first frame update
    void Start()
    {
        fadeIn();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void fadeIn()
    {
        _animator.SetTrigger("FadeIn");
    }
    public void fadeToGame()
    {
        _animator.SetTrigger("FadeOut");
    }

    public void fadeOutCompleted()
    {
        SceneManager.LoadScene(4);
    }
    public void changeToTutorial()
    {
        SceneManager.LoadScene(5);
    }

    public void credits()
    {
        SceneManager.LoadScene(6);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
