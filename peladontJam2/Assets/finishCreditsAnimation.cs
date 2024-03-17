using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class finishCreditsAnimation : MonoBehaviour
{
    [SerializeField] private Animator m_Animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void FinishFade()
    {
        SceneManager.LoadScene(0);
    }
}
