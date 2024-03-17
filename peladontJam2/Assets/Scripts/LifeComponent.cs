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
    
    [SerializeField] 
    private VisualEffect miniBloodEffect;

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

        if (miniBloodEffect)
        {
            miniBloodEffect.SendEvent("Bleed");
        }

        //si tenemos 0 o menos vida, destroy
        if (life <= 0) {
            soundInstance.setParameterByName("Alive",1f);
            soundInstance.start();
            soundInstance.release();
            if (bloodEffect)
            {
                bloodEffect.SendEvent("Bleed");
                bloodEffect.gameObject.transform.parent = null;
            }
            Destroy(gameObject);
            
            List<FischlWorks_FogWar.csFogWar.FogRevealer> fogList =   PlacementSystem.fog_._FogRevealers;

            bool encontrado = false;

            int i = 0;

            while (i < fogList.Count && !encontrado) 
            {
                if (fogList[i]._RevealerTransform == transform)
                {
                    encontrado = true;
                }
                else i++;
            }

            if(encontrado)
            {
                fogList.RemoveAt(i);
                PlacementSystem.fog_.ReplaceFogRevealerList(fogList);   
            }

            return true;
        }
        soundInstance.start();
        soundInstance.release();
        return false;

        //LUIS HAZ TU COSA (SONIDO AQUI)

    }

    public float getLife() { return life; }

}

