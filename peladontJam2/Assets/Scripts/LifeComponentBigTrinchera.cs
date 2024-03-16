using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
                SceneManager.LoadScene("BloodTest", LoadSceneMode.Additive);
            }
            //Victoria
            else
            {
                //Pasar a la siguiente escena
            }

            return base.reciveDamage(damage);
        }

        return false;

    }

}
