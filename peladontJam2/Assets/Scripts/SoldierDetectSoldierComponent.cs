using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/**
* Tenemos un problema: el soldado tiene dos colliders, el del propio cuertpo, el rango de visión y el rango de disparo.
* Al no conocer el tipo de de Collider que quiero para cada cosa, no tengo forma de saber cual es cual, porque todos están siendo accedidos como Collider
* Así que tengo dos opciones, poner tipos fijos (sphere collider, mesh, etc) y así desambiguar. O hacer subobjetos con los distintos colliders (puede ser tedioso).
*/
[RequireComponent(typeof(SphereCollider))]
public class SoldierDetectSoldierComponent : MonoBehaviour
{
    Collider deteccionEnemigos;
    // Start is called before the first frame update
    void Start()
    {
        deteccionEnemigos = this.GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Esto hay que hacerlo de otra forma, no con layers
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.layer == 6) {
            Debug.Log("soldado detectado");
        }   
    }
}
