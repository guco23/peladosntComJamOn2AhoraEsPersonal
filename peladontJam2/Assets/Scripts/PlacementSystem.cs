using FischlWorks_FogWar;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlacementSystem : MonoBehaviour
{

    [SerializeField] private LayerMask placementLayer;
    [SerializeField]
    private int fil1PosY = -3;
    public struct spawnInfo
    {
        public spawnInfo(bool pos,bool rango, int contador)
        {
            posIzq = pos;
            menosRango = rango;
            contadorRango = contador;
        }

        public bool posIzq;
        public bool menosRango;
        public int contadorRango;
    }

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
    [SerializeField] private csFogWar fog_;

    [SerializeField] int spawnCellX;

    [SerializeField] int enemyType;

    [SerializeField] ManagerResourcesTrincher teamResourses;

    #endregion

    public void setSoldierType(int i)
    { print("tu viejjaaaaaa"); enemyType = i; }


    List<spawnInfo> spawns = new List<spawnInfo>();
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
                    fog_.AddFogRevealer(new csFogWar.FogRevealer(soldier.transform, 1, true));

                    soldier.transform.Rotate(new Vector3(0, 90, 0));

                    print(cellPosSpawn.y - fil1PosY);
                    spawnInfo sp = spawns[cellPosSpawn.y - fil1PosY];


                    if(sp.posIzq)
                    {
                        soldier.transform.position += new Vector3(0, 0, 0.45f);
                    }
                    else
                    {
                        soldier.transform.position += new Vector3(0, 0, -0.45f);
                    }

                    if(sp.menosRango)
                    {
                        soldier.GetComponent<SoldierDetectSoldierComponent>().ReduceRange(0.5f);
                    }

                    sp.posIzq = !sp.posIzq;
                    sp.contadorRango++;
                    if(sp.contadorRango >= 2)
                    {
                        sp.contadorRango = 0;
                        sp.menosRango = !sp.menosRango;
                    }


                    spawns[cellPosSpawn.y - fil1PosY ] = sp;
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
        if (callback.canceled)
        {
            spawnSoldier = false;
        }
        if (callback.started )
        {

            Vector3 mousePos = Input.mousePosition;

            mousePos.z = Camera.main.nearClipPlane;

            Ray ray = Camera.main.ScreenPointToRay(mousePos);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, placementLayer))
            {
                //return;
                //solo spawn si tca el terreno
                spawnSoldier = true;
            }
        }
       
    
    }

    private void Start()
    {
        for (int i = 0;i <5;i++)
        {
            spawns.Add(new spawnInfo(false, false, 0));
        }

    }
}
