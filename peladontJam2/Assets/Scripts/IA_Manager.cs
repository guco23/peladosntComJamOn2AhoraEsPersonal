using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class IA_Manager : MonoBehaviour
{
    [SerializeField]
    private int spawnPosX = 19;

    [SerializeField]
    private int fil1PosY = -3;

    [SerializeField]
    private GameObject enemySoldierPrefab;

    [SerializeField]
    private Vector3 spawnPosOffSet;

    [SerializeField] private Grid grid_;


    [SerializeField]
    private float enemySpawnRate = 1.0f;

    private float elapsedTime = 0;

    [SerializeField]
    private float stateTime = 15;
    public enum IA_STATE
    {
        ATACK_FRENETIC,//spawnea todos los soldados posibles
        ATACK_TACTIC,//espera un poco y spawnea soldados
        BUILD_TRINCHER//intenta construir trincheras
    }

    private IA_STATE state;

    [SerializeField]
    private ManagerResourcesTrincher resourceManager;


    /* FILAS:
     * 5
     * 4
     * 3
     * 2
     * 1
     */
    void spawnEnemy(int fila)
    {
        Vector3Int cellPos = new Vector3Int(spawnPosX, fil1PosY + fila - 1, 0);

        GameObject soldier = Instantiate(enemySoldierPrefab, grid_.CellToWorld(cellPos) + spawnPosOffSet, Quaternion.identity);    

        soldier.transform.Rotate(new Vector3(0, -90, 0));
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;  

        if(elapsedTime > stateTime) {
            elapsedTime = 0;

            state = (IA_STATE)Random.Range((int)0, (int)4);

            //spawnEnemy(Random.Range((int)1, (int)6));
            //spawnEnemy(Random.Range((int)1, (int)2));
        }
        

        if(state == IA_STATE.ATACK_FRENETIC)
        {
            if(resourceManager.getResources() >= 150)
            {
                resourceManager.SpendResourses(100);
                spawnEnemy(Random.Range((int)1, (int)6));
            }
        }
        else if (state == IA_STATE.ATACK_TACTIC)
        {
            if (resourceManager.getResources() >= 500)
            {
                resourceManager.SpendResourses(500);
                spawnEnemy(1);
                spawnEnemy(2);
                spawnEnemy(3);
                spawnEnemy(4);
                spawnEnemy(5);
            }
        }
        else if(state == IA_STATE.BUILD_TRINCHER)
        {

        }
    }

    private void Start()
    {
        state = IA_STATE.ATACK_TACTIC;
        //spawnEnemy(Random.Range((int)1, (int)2));
    }
}

