using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResorsesUpgrade : MonoBehaviour
{

    [SerializeField] private ManagerResourcesTrincher upgrade;


    [SerializeField] private int resoursesNumWin;


    public void UpgradeResoursesManager()
    {

        upgrade.UpgradeResourses(resoursesNumWin);

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
