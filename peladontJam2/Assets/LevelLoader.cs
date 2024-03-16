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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void fadeToGame()
    {
        _animator.SetTrigger(1);
    }

    public void fadeCompleted()
    {
        SceneManager.LoadScene(1);
    }
}
