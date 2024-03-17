using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{

    [SerializeField] private GameObject resoursesSystem_;

    [SerializeField] private GameObject iaSystem_;

    [SerializeField] private GameObject canvas_;


    // Start is called before the first frame update
    void Start()
    {

        resoursesSystem_.SetActive(false);
        iaSystem_.SetActive(false);
        canvas_.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
