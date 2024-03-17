using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BuildTrinchera : MonoBehaviour
{

    [SerializeField] private float timeBuildSpend;

    [SerializeField] private GameObject trinchera;

    [SerializeField] private Vector3 offsetTrinchera;

    [SerializeField] private int tipo = 0;

    private SoldierMoveComponent moveComponent;

    private float elapsedTime;

    private Vector3 posWhereBuild;

    private bool building = false;

    private EventInstance eventInstance;

    private InFrustrumChecker inFrustrum;

    [SerializeField]
    private FMODUnity.EventReference buildEvent;


    public void setType(int type)
    {
        tipo = type;
    }

    public void setTrinPos(Vector3 pos)
    {
        Debug.Log(pos);

        posWhereBuild = pos;

    }

    void Start()
    {
        
        moveComponent = GetComponent<SoldierMoveComponent>();
        eventInstance = RuntimeManager.CreateInstance(buildEvent.Path);
    }

    // Update is called once per frame
    void Update()
    {

        if(tipo == 0)
        {
            if(transform.position.x >= posWhereBuild.x && !building)
            {
                building = true;
                moveComponent.stopMoving();
                if(inFrustrum.IsVisible)
                    eventInstance.start();
            }
        }
        if (tipo == 1)
        {
            if (transform.position.x <= posWhereBuild.x && !building)
            {
                building = true;
                moveComponent.stopMoving();
                if (inFrustrum.IsVisible)
                    eventInstance.start();
            }
        }
        if (inFrustrum.IsVisible)
        {
            eventInstance.release();
        }
        if (building)
        {

            elapsedTime += Time.deltaTime;


            if (elapsedTime > timeBuildSpend)
            {
                eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                GameObject trin = Instantiate(trinchera, posWhereBuild + offsetTrinchera, Quaternion.identity);
                Destroy(gameObject);

            }

        }

    }
}
