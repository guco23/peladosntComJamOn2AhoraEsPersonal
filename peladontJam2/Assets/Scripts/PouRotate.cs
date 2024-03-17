using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PouRotate : MonoBehaviour
{
    private Transform tr_;

    [SerializeField] private float rot_;

    // Start is called before the first frame update
    void Start()
    {
        tr_ = transform;
    }

    // Update is called once per frame
    void Update()
    {
        //tr_.rotation += Vector3(0, rot_, 0);
    }
}
