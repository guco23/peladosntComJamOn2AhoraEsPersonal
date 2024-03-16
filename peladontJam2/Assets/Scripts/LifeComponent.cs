using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class LifeComponent : MonoBehaviour
{
    [SerializeField]
    protected float maxLife = 10;

    [SerializeField]
    protected float life = 0;

    [SerializeField]
    protected FMODUnity.EventReference inputsound;

    [SerializeField] 
    private VisualEffect bloodEffect;

    protected void Start()
    {
        life = maxLife;
    }

    //damage must be positive
    //returns true, if dead
    public virtual bool reciveDamage(float damage)
    {
        life -= damage;
        EventInstance soundInstance = RuntimeManager.CreateInstance(inputsound.Path);

        if (bloodEffect) bloodEffect.SendEvent("Bleed");

        //si tenemos 0 o menos vida, destroy
        if (life <= 0) {
            soundInstance.setParameterByName("Alive",1f);
            soundInstance.start();
            soundInstance.release();
            Destroy(gameObject);
            return true;
        }
        soundInstance.start();
        soundInstance.release();
        return false;

        //LUIS HAZ TU COSA (SONIDO AQUI)

    }

    public float getLife() { return life; }

}

