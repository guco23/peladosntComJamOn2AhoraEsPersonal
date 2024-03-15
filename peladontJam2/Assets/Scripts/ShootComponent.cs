using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootComponent : MonoBehaviour
{
    [SerializeField]
    float fireRate = 1.5f;

    float elapsedTime = 0;

    [SerializeField]
    bool shooting = true;


    [SerializeField]
    float damage = 2f;


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
        

    }
}

