using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
    protected StudioEventEmitter emitter;

    [SerializeField] 
    private VisualEffect bloodEffect;
    
    [SerializeField] 
    private VisualEffect miniBloodEffect;

    private Animator anim;

    protected void Start()
    {
        life = maxLife;
        anim = GetComponentInChildren<Animator>();
    }

    //damage must be positive
    //returns true, if dead
    public virtual bool reciveDamage(float damage)
    {
        life -= damage;

        if (miniBloodEffect)
        {
            miniBloodEffect.SendEvent("Bleed");
        }

        //si tenemos 0 o menos vida, destroy
        if (life <= 0) {

            SoldierMoveComponent solMove_;

            

            if (gameObject.TryGetComponent<SoldierMoveComponent>(out solMove_))
            {

                solMove_.enabled= false;

            }

            emitter.SetParameter("Alive", 1f);
            emitter.Play();
            if (bloodEffect)
            {
                bloodEffect.SendEvent("Bleed");
                bloodEffect.gameObject.transform.parent = null;
            }

            List<FischlWorks_FogWar.csFogWar.FogRevealer> fogList = PlacementSystem.fog_._FogRevealers;

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

            if (encontrado)
            {
                fogList.RemoveAt(i);
                PlacementSystem.fog_.ReplaceFogRevealerList(fogList);
            }
            anim.SetTrigger("die");
            Destroy(gameObject,3f);
            
            return true;
        }
        emitter.Play();
        return false;

        //LUIS HAZ TU COSA (SONIDO AQUI)

    }

    public float getLife() { return life; }

}

