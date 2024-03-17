using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creditsAnimation : MonoBehaviour
{
    [SerializeField] Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startCredits()
    {
        _animator.SetTrigger("Start");
    }
}
