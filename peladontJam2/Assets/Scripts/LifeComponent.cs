using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeComponent : MonoBehaviour
{
    [SerializeField]
    private float maxLife = 10;

    [SerializeField]
    private float life = 0;

    private void Start()
    {
        life = maxLife;
    }

    //damage must be positive
    //returns true, if dead
    public bool reciveDamage(float damage)
    {
        life -= damage;
        
        if(life <= 0) {
            Destroy(gameObject);
            return true;
        }
        return false;
    }

}

