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

        if(elapsedTime > 0)
        {

            if(elapsedTime > enemySpawnRate) {
                elapsedTime = 0;
                spawnEnemy(Random.Range((int)1, (int)6));
            }
        }
    }


}

