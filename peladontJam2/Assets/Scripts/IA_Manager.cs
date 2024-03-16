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
    private GameObject basicSoldierPickAxePrefab;

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

    void spawnTrincher(int fila,int col) {

        Vector3Int cellPos = new Vector3Int(col, fil1PosY + fila - 1, 0);

        Vector3Int cellPosSpawn = new Vector3Int(spawnPosX, fil1PosY + fila - 1, 0);

        GameObject soldierPickaxe = Instantiate(basicSoldierPickAxePrefab, grid_.CellToWorld(cellPosSpawn) , Quaternion.identity);

        soldierPickaxe.transform.Rotate(new Vector3(0, -90, 0));

        BuildTrinchera trinBuild = soldierPickaxe.GetComponent<BuildTrinchera>();

        trinBuild.setTrinPos(grid_.CellToWorld(cellPos));

        trinBuild.setType(1);
    }


    private void Update()
    {
        elapsedTime += Time.deltaTime;  

        if(elapsedTime > stateTime) {
            elapsedTime = 0;

            state = (IA_STATE)Random.Range((int)0, (int)3);

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
            if (resourceManager.getResources() >= 300){

            }
        }
    }

    private void Start()
    {
        state = IA_STATE.ATACK_TACTIC;

        spawnTrincher(1, 2);
        //spawnEnemy(Random.Range((int)1, (int)2));
    }
}

