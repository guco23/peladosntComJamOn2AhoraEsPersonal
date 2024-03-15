using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTrinchera : MonoBehaviour
{

    [SerializeField] private float timeBuildSpend;

    [SerializeField] private GameObject trinchera;

    [SerializeField] private Vector3 offsetTrinchera;

    private SoldierMoveComponent moveComponent;

    private float elapsedTime;

    private Vector3 posWhereBuild;

    private bool building = false;



    public void setTrinPos(Vector3 pos)
    {
        Debug.Log(pos);

        posWhereBuild = pos;

    }

    void Start()
    {
        
        moveComponent = GetComponent<SoldierMoveComponent>();

    }

    // Update is called once per frame
    void Update()
    {

        if(transform.position.x >= posWhereBuild.x && !building)
        {

            building = true;

            moveComponent.stopMoving();

        }

        if (building)
        {

            elapsedTime += Time.deltaTime;

            if (elapsedTime > timeBuildSpend)
            {

                GameObject trin = Instantiate(trinchera, posWhereBuild + offsetTrinchera, Quaternion.identity);

                Destroy(this.gameObject);

            }

        }

    }
}
