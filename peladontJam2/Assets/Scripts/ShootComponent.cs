using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootComponent : MonoBehaviour
{
    [SerializeField]
    ParticleSystem _shootParticles;

    [SerializeField]
    private float firstShootDelay = 0.5f;

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

    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private Transform spawnPoint;

    [SerializeField]
    private SoldierDetectSoldierComponent soldierDectect;

    //public FMODUnity.EventReference inputsound;
    public StudioEventEmitter emitter;

    private InFrustrumChecker checker;

    private Animator anim;

    private void Start()
    {
        RandFireRate();
        elapsedTime = fireRate - firstShootDelay;

        soldierDectect = GetComponent<SoldierDetectSoldierComponent>();    
        checker = GetComponent<InFrustrumChecker>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (shooting)
        {
            //control de tiempo
            elapsedTime += Time.deltaTime;

            if (elapsedTime > fireRate)
            {
                elapsedTime = 0;

                //si toca disparar, shoot
                shoot();
            }
        }

        if(target == null || target.GetComponent<LifeComponent>().getLife() <= 0) {
            StopShooting(); 
        }

    }

    private void shoot()
    {

        SpawnBullet();
        //si matamos al enemigo
        /*
        if (target.reciveDamage(damage))
        {
            target = null;
            shooting = false;
        }
         */

        //recalcular el fire rate(aleatorio entre min y max)
        RandFireRate();

        //Debug.Log("disparo");

        _shootParticles.Play();

        //llamar al sonido de disparo(LUIS HAZ TU COSA)
        if (checker.IsVisible)
        { 
            //EventInstance soundInstance = RuntimeManager.CreateInstance(inputsound.Path);
            emitter.Play();
            //soundInstance.start();
            //soundInstance.release();
        }
    }

    private void StartShooting()
    {
        shooting = true;


        anim.SetBool("isShooting",shooting);
        RandFireRate();

        elapsedTime = fireRate - firstShootDelay;
    }

    private void RandFireRate()
    {
        fireRate = Random.Range(minFireRate, maxFireRate); 
    }

    public void SetTarget(LifeComponent _target)
    {
        target = _target;

        StartShooting();
    }

    public void StopShooting()
    {
        target = null;
        shooting = false;
        anim.SetBool("isShooting", shooting);
        soldierDectect.enemyDefeated();
    }

    private void SpawnBullet()
    {
        BulletComponent bullet = Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity).GetComponent<BulletComponent>();

        bullet.SetTarget(target);
        bullet.SetDamage(damage);

        bullet.SetShootingComp(this);
    }


    
}

