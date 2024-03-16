using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlacementSystem : MonoBehaviour
{

    [SerializeField]
    private float minSpawnRate = 0.3f;

    private float elapsedTime = 0;

    private bool spawnSoldier = false;

    [SerializeField]
    private Vector3 placeOffSet = new Vector3(1, 1, 1);

    [SerializeField]
    private Vector3 placeOffSetSpawn = new Vector3(1, 1, 1);

    #region references

    [SerializeField] private GameObject mouseIcon, cellIndicator, basicSoldierPrefab, basicSoldierPickAxePrefab;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private Grid grid_;

    [SerializeField] int spawnCellX;

    [SerializeField] int enemyType;

    [SerializeField] ManagerResourcesTrincher teamResourses;

    #endregion
    bool posIzq = true;
    bool menosRango = false;
    int contadorRango = 0;


    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = inputManager.GetSelectedMapPoint();
        Vector3Int cellPos = grid_.WorldToCell(mousePos);
        //print(cellPos);
        mouseIcon.transform.position = grid_.CellToWorld(cellPos) +placeOffSet;
        cellIndicator.transform.position = grid_.CellToWorld(cellPos);

        elapsedTime += Time.deltaTime;


        if (spawnSoldier)
        {
            if(elapsedTime > minSpawnRate)
            {

                elapsedTime = 0;

                Vector3Int cellPosSpawn = cellPos;

                cellPosSpawn.x = spawnCellX;


                if (enemyType == 0 && teamResourses.SpendResourses(100))
                {

                   
                    GameObject soldier = Instantiate(basicSoldierPrefab, grid_.CellToWorld(cellPosSpawn) + placeOffSetSpawn, Quaternion.identity);

                    soldier.transform.Rotate(new Vector3(0, 90, 0));

                    if(posIzq)
                    {
                        soldier.transform.position += new Vector3(0, 0, 0.45f);
                    }
                    else
                    {
                        soldier.transform.position += new Vector3(0, 0, -0.45f);
                    }

                    if(menosRango)
                    {
                        soldier.GetComponent<SoldierDetectSoldierComponent>().ReduceRange(0.5f);
                    }

                    posIzq = !posIzq;
                    contadorRango++;
                    if(contadorRango >= 2)
                    {
                        contadorRango = 0;
                        menosRango = !menosRango;
                    }

                    /*

                    if(b)
                    {
                        soldier.transform.position += new Vector3(0, 0, 1);
                    }
                    b = !b;
                     */

                }
                else if(enemyType == 1 && teamResourses.SpendResourses(300))
                {

                    GameObject soldierPickaxe = Instantiate(basicSoldierPickAxePrefab, grid_.CellToWorld(cellPosSpawn) + placeOffSetSpawn, Quaternion.identity);

                    soldierPickaxe.transform.Rotate(new Vector3(0, 90, 0));

                    BuildTrinchera trinBuild = soldierPickaxe.GetComponent<BuildTrinchera>();

                    trinBuild.setTrinPos(grid_.CellToWorld(cellPos));

                }
                

            }
        }
    }

    public void SpawnBasicSoldier(InputAction.CallbackContext callback)
    {
        if(callback.started )
        {
            spawnSoldier = true;
        }
        if (callback.canceled)
        {
            spawnSoldier = false;
        }
    
    }
}
