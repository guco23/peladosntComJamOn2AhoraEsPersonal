using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletComponent : MonoBehaviour
{
    Rigidbody rb;
    Transform myTransform;

    [SerializeField]
    private float speed = 2;

    [SerializeField]
    private float damage = 0;

    private LifeComponent target;

    private ShootComponent shootComponent;
    private void Awake()
    {
        myTransform = transform;
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.right * speed;

    }

    private void Update()
    {
    }
    

    public void SetTarget(LifeComponent _target)
    {
        target = _target; 
        rb.velocity = (_target.gameObject.GetComponent<Transform>().position - myTransform.position).normalized * speed;
    }

    public void SetDamage(float _damage)
    {
        damage = _damage;
    }
    public void SetShootingComp(ShootComponent _shootComponent)
    {
        shootComponent = _shootComponent;   
    }

    private void OnTriggerEnter(Collider other)
    {
        //print(target);
        if (target == null) return;

        if(other.GetComponent<LifeComponent>() == target) {

            if (target.reciveDamage(damage))
            {
                shootComponent.StopShooting();
            }
            Destroy(gameObject);  
        }
    }
}

