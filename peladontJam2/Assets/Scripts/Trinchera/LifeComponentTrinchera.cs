using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeComponentTrinchera : LifeComponent
{
    protected new void Start() {
        base.Start();
    
    }
    public override bool reciveDamage(float damage)
    {
        life -= damage;
        
        //si tenemos 0 o menos vida, destroy
        if(life <= 0) {
            this.GetComponent<TrincheraManager>().DeadTrinchera(); //"Muerte" de la trinchera
            return true;
        }
        return false;

        //LUIS HAZ TU COSA (SONIDO AQUI)

    }
}
