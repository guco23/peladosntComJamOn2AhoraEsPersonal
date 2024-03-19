using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DotDestroyMusic : MonoBehaviour
{
    private void Awake()
    {
        //SceneManager.sceneLoaded += TurnDownSound;
        SceneManager.sceneUnloaded += TurnDownSound;
        
    }
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TurnDownSound(Scene sc)
    {
        if (sc.buildIndex == 1 || sc.buildIndex == 2 || sc.buildIndex == 5 || sc.buildIndex == 3|| sc.buildIndex == 6)
        {
            GetComponent<StudioEventEmitter>().Stop();
        }
    }
}
