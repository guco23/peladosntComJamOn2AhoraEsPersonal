using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ResourseManager : MonoBehaviour
{


    [SerializeField] private float cooldown;

    [SerializeField] private float resourseWinAmount;

    private float elapsedTime = 0;

    [SerializeField]
    private float resourseAmount = 0;


    #region methods

    public bool SpendResourses(float cost)
    {
        if(cost < resourseAmount)
        {
            resourseAmount -= cost;
            return true;
        }
        else
        {
            return false;
        }


    }


    #endregion

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(elapsedTime >= cooldown)
        {
            Debug.Log(elapsedTime);

            resourseAmount += resourseWinAmount;
            elapsedTime = 0;

            Debug.Log(elapsedTime);

        }
        else
        {
            elapsedTime += Time.deltaTime;
        }

        

    }
}
