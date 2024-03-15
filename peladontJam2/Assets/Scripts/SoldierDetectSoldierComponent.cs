using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//Nueva idea: hacer un raycast hacia delante y lo que pille
public class SoldierDetectSoldierComponent : MonoBehaviour
{
    //El move component del soldado
    SoldierMoveComponent soldierMoveComponent;
    ShootComponent shootComponent;
    RaycastHit deteccionEnemigos;
    bool targetFocused;

    [SerializeField]
    [Tooltip("La distancia de detección")]
    int distancia;

    [SerializeField]
    [Tooltip("El layerMASK de las unidades enemigas")]
    LayerMask targetLayerMask;
    //LifeComponent enemyLife;

    // Start is called before the first frame update
    void Start()
    {
        targetFocused = false;
        deteccionEnemigos = new RaycastHit();
        soldierMoveComponent = this.GetComponent<SoldierMoveComponent>();
        shootComponent = this.GetComponent<ShootComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!targetFocused) {
            Physics.Raycast(transform.position, transform.forward, out deteccionEnemigos, distancia, targetLayerMask );
            Debug.DrawRay(transform.position, transform.forward, Color.green, 0f, false); //debug

            if(deteccionEnemigos.collider != null) {
                targetFocused = true;
                LifeComponent enemyLife = deteccionEnemigos.collider.gameObject.GetComponent<LifeComponent>();
                soldierMoveComponent.stopMoving();
                shootComponent.SetTarget(enemyLife);
            }
        }
        //También debería comprobarse si hay una trinchera, en ese caso, entrar en ella y lo que eso conlleve
    }
    /**
    A llamar una vez haya muerto el enemigo que tenía como objetuivo
    */
    void enemyDefeated() {
        targetFocused = false;
        soldierMoveComponent.continueMoving();
    }
}