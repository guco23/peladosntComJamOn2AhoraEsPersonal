using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerResourcesTrincher : MonoBehaviour
{


    [SerializeField] private float cooldown;

    [SerializeField] private float resourseWinAmount;

    private float elapsedTime = 0;

    [SerializeField]
    private float resourseAmount = 0;


    #region methods

    public bool SpendResourses(float cost)
    {
        if (cost <= resourseAmount)
        {
            resourseAmount -= cost;
            return true;
        }
        else
        {
            return false;
        }
    }

    public float getResources()
    {
        return resourseAmount;
    }

    public float getElapsedTime()
    {
        return elapsedTime;
    }
    #endregion

    void Start()
    {

    }

    public void setResourcesAmount(int amount)
    {
        resourseWinAmount = amount;
    }

    // Update is called once per frame
    void Update()
    {

        if (elapsedTime >= cooldown)
        {

            resourseAmount += resourseWinAmount;
            elapsedTime = 0;

        }
        else
        {
            elapsedTime += Time.deltaTime;
        }



    }
}

