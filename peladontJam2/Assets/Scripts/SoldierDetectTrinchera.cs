using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoldierDetectTrinchera : MonoBehaviour
{
    //Collider collider;
    // Start is called before the first frame update
    void Start()
    {
      //  collider = this.GetComponent<Collider>();
    }

    void OnTriggerEnter(Collider other) {
        TrincheraManager tm = other.gameObject.GetComponent<TrincheraManager>();
        if(tm != null) {
            tm.EntrarEnTrinchera(this.gameObject);
        }
    }
}
