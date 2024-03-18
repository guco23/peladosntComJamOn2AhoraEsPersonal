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

    [SerializeField] private float maxWinAmount;

    [SerializeField] private float upgradeAmount;


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

    public bool UpgradeResourses(float cost)
    {

        if (cost <= resourseAmount && resourseWinAmount < maxWinAmount)
        {
            resourseAmount -= cost;

            resourseWinAmount += upgradeAmount;

            if(resourseWinAmount > maxWinAmount)
            {

                resourseWinAmount= maxWinAmount;

            }


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

    public float getResourcesPerSecond()
    {
        return resourseWinAmount;
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

