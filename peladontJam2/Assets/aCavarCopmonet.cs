using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aCavarCopmonet : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        animator.SetTrigger("Digging");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
