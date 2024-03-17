using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;

public class LifeComponentBigTrinchera : LifeComponent
{

    [SerializeField] private int type;

    public override bool reciveDamage(float damage) 
    {
        life -= damage;

        if(life <= 0)
        {
            //Derrota
            if (type == 0)
            {
                SceneManager.LoadScene("DefeatScene");
                
            }
            //Victoria
            else
            {
                //si vences la trinchera enemiga

                //SceneManager.LoadScene("VictoryScene");

                if (PlacementSystem.currentLevel < 2)
                {
                    print(PlacementSystem.currentLevel);
                    SceneManager.LoadScene("SamuScene");
                    PlacementSystem.currentLevel += 1;
                }
                else SceneManager.LoadScene("VictoryScene");
            }

            return base.reciveDamage(damage);
        }

        return false;

    }

}
