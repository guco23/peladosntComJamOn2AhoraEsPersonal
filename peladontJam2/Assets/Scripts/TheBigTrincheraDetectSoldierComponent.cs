using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TheBigTrincheraDetectSoldierComponent : SoldierDetectSoldierComponent
{


    // Start is called before the first frame update
    void Start()
    {
        targetFocused = false;
        shootComponent = this.GetComponent<ShootComponent>();

    }

    // Update is called once per frame

    // Update is called once per frame
    void Update()
    {

        if (!targetFocused)
        {

            Physics.BoxCast(transform.position, transform.lossyScale / 2, transform.right,
                out deteccionEnemigos, transform.rotation, distancia, targetLayerMask);


            //Physics.Raycast(transform.position, transform.forward, out deteccionEnemigos, distancia, targetLayerMask);


            //Debug.DrawRay(transform.position, transform.forward, Color.green, 0f, false); //debug

            if (deteccionEnemigos.collider != null)
            {
                targetFocused = true;
                LifeComponent enemyLife = deteccionEnemigos.collider.gameObject.GetComponent<LifeComponent>();
                shootComponent.SetTarget(enemyLife);
            }
        }
    }
    public override void enemyDefeated()
    {
        targetFocused = false;
    }

    private void OnDrawGizmos()
    {
        if (targetFocused)
        {

        }
    }
}
