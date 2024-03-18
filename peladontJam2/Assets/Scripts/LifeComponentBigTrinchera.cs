using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.VFX;


public class LifeComponentBigTrinchera : LifeComponent 
{

    [SerializeField] private int type;

    [SerializeField] private Image healthBar_;

    public override bool reciveDamage(float damage) 
    {
        life -= damage;

        healthBar_.fillAmount = life / maxLife;

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

                if (PlacementSystem.currentLevel < 4)
                {
                    print(PlacementSystem.currentLevel);
                    SceneManager.LoadScene("Overworld");
                    PlacementSystem.currentLevel += 1;
                }
                else SceneManager.LoadScene("VictoryScene");
            }

            return base.reciveDamage(damage);
        }

        return false;

    }


}
