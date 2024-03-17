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

    private Animator anim;


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
        inFrustrum = GetComponent<InFrustrumChecker>();
        anim = GetComponentInChildren<Animator>();
        float num = Random.Range(0f, 1f);
        if ( num > 0.5f)
        {
            anim.SetTrigger("NarutoRun");
        }
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
                if (inFrustrum.IsVisible)
                {
                    Debug.Log("Sonido");
                    eventInstance.start();
                }
                anim.SetTrigger("Digging");
            }
        }
        if (tipo == 1)
        {
            if (transform.position.x <= posWhereBuild.x && !building)
            {
                building = true;
                moveComponent.stopMoving();
                if (inFrustrum.IsVisible)
                {
                    Debug.Log("Sonido");
                    eventInstance.start();
                }
                anim.SetTrigger("Digging");
            }
        }
        //if (!inFrustrum.IsVisible)
        //{
        //    eventInstance.release();
        //}
        if (building)
        {

            elapsedTime += Time.deltaTime;


            if (elapsedTime > timeBuildSpend)
            {
                eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                GameObject trin = Instantiate(trinchera, posWhereBuild + offsetTrinchera, Quaternion.identity);

                List<FischlWorks_FogWar.csFogWar.FogRevealer> fogList = PlacementSystem.fog_._FogRevealers;

                bool encontrado = false;

                int i = 0;

                while (i < fogList.Count && !encontrado)
                {
                    if (fogList[i]._RevealerTransform == transform)
                    {
                        encontrado = true;
                    }
                    else i++;
                }

                if (encontrado)
                {
                    fogList.RemoveAt(i);
                    PlacementSystem.fog_.ReplaceFogRevealerList(fogList);
                }

                Destroy(gameObject);

            }

        }

    }
}
