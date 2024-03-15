using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//Nueva idea: hacer un raycast hacia delante y lo que pille
public class SoldierDetectSoldierComponent : MonoBehaviour
{
    RaycastHit deteccionEnemigos;
    bool targetFocused;

    [SerializeField]
    [Tooltip("La distancia de detección")]
    int distancia;

    [SerializeField]
    [Tooltip("El layerMASK de las unidades enemigas (porfa mirar lo que es un layermask)")]
    LayerMask targetLayerMask;
    // Start is called before the first frame update
    void Start()
    {
        targetFocused = false;
        deteccionEnemigos = new RaycastHit();
    }

    // Update is called once per frame
    void Update()
    {
        if(!targetFocused) {
            Physics.Raycast(transform.position, transform.forward, out deteccionEnemigos, distancia, targetLayerMask );
            Debug.DrawRay(transform.position, transform.forward, Color.green, 0f, false); //debug

            if(deteccionEnemigos.collider == null) {
                Debug.Log("nadie ha sido detectado");
            }
        }
    }
}
