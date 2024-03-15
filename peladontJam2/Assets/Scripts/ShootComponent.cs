using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootComponent : MonoBehaviour
{
    [SerializeField]
    private float fireRate = 1.5f;

    [SerializeField]    
    private float minFireRate = 1.3f;

    [SerializeField]
    private float maxFireRate = 1.7f;



    private float elapsedTime = 0;

    [SerializeField]
    private bool shooting = true;


    [SerializeField]
    private float damage = 2f;

    [SerializeField]
    private LifeComponent target;

    private void Start()
    {
        RandFireRate();
    }

    private void Update()
    {
        if (shooting)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime > fireRate)
            {
                elapsedTime = 0;

                shoot();
            }
        }


    }

    void shoot()
    {
        if (target.reciveDamage(damage))
        {
            target = null;
            shooting = false;
        }

        RandFireRate();

        Debug.Log("disparo");

        //llamar al sonido de disparo
    }

    public void StartShooting()
    {
        shooting = true;
    }

    private void RandFireRate()
    {
        fireRate = Random.Range(minFireRate, maxFireRate); 
    }
}

