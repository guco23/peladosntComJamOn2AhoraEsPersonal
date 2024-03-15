using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Queremos que el soldadito
[RequireComponent(typeof(Rigidbody))]
public class SoldierMoveComponent : MonoBehaviour
{
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * 10;

    }

    public void stopMoving() {
        rb.velocity = Vector3.zero;
    }

    public void continueMoving() {

    }
}
