using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Queremos que el soldadito
[RequireComponent(typeof(Rigidbody))]
public class SoldierMoveComponent : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField]
    [Tooltip("La velocidad del personaje")]
    float speed;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        continueMoving();
    }

    public void stopMoving() {
        rb.velocity = Vector3.zero;
    }

    public void continueMoving() {
        rb.velocity = transform.forward * speed;
    }
}
