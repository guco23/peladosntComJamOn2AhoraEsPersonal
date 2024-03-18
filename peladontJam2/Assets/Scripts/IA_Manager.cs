using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class IA_Manager : MonoBehaviour
{
    [SerializeField]
    private int spawnPosX = 10;

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


    public 
    List<Vector3Int> posicionesConTrincheras;



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

        GameObject soldierPickaxe = Instantiate(basicSoldierPickAxePrefab, grid_.CellToWorld(cellPosSpawn)+spawnPosOffSet , Quaternion.identity);

        soldierPickaxe.transform.Rotate(new Vector3(0, -90, 0));

        BuildTrinchera trinBuild = soldierPickaxe.GetComponent<BuildTrinchera>();

        trinBuild.setTrinPos(grid_.CellToWorld(cellPos));

        trinBuild.setType(1);

        posicionesConTrincheras.Add(cellPos);
    }


    private void Update()
    {
        elapsedTime += Time.deltaTime;  

        if(elapsedTime > stateTime) {
            elapsedTime = 0;

            //state = (IA_STATE)Random.Range((int)0, (int)3);
            NextState();
            //spawnEnemy(Random.Range((int)1, (int)6));
            //spawnEnemy(Random.Range((int)1, (int)2));
        }
        

        if(state == IA_STATE.ATACK_FRENETIC)
        {
            if(resourceManager.getResources() >= 150)
            {
                resourceManager.SpendResourses(100);
                spawnEnemy(Random.Range((int)1, (int)7));
            }
        }
        else if (state == IA_STATE.ATACK_TACTIC)
        {
            if (resourceManager.getResources() >= 600)
            {
                resourceManager.SpendResourses(600);
                spawnEnemy(1);
                spawnEnemy(2);
                spawnEnemy(3);
                spawnEnemy(4);
                spawnEnemy(5);
                spawnEnemy(6);
            }
        }
        else if(state == IA_STATE.BUILD_TRINCHER)
        {
            if (resourceManager.getResources() >= 400){

                resourceManager.SpendResourses(300);


                int fila = Random.Range((int)1,(int)6);
                
                int col = Random.Range((int)-2, (int)9);
           
                Vector3Int cellPos = new Vector3Int(col, fil1PosY + fila - 1, 0);

                while (posicionesConTrincheras.Contains(cellPos)){
                    fila = Random.Range((int)0, (int)6);
                    col = Random.Range((int)-2, (int)9);
                    cellPos = new Vector3Int(col, fil1PosY + fila - 1, 0);
                }

                spawnTrincher(fila, col);
            }
        }
    }

    private void Start()
    {
        //state = (IA_STATE)Random.Range((int)0, (int)3);
        NextState();
        //state = IA_STATE.ATACK_TACTIC;
        //spawnTrincher(1, 2);
        //spawnEnemy(Random.Range((int)1, (int)2));
    }


    void NextState()
    {
        int x = Random.Range((int)0,(int)100);

        if (x <= 40)
        {
            state = IA_STATE.ATACK_TACTIC;
        }
        else if (x <= 80)
        {
            state = IA_STATE.ATACK_FRENETIC;
        }
        else
        {
            state = IA_STATE.BUILD_TRINCHER;
        }
    }
}

