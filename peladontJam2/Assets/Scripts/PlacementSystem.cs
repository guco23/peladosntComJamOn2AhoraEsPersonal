using FischlWorks_FogWar;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField] private ManagerResourcesTrincher resources_player;
    [SerializeField] private GameObject resourcesText;

    [SerializeField] private GameObject levelText;
    [SerializeField] private ManagerResourcesTrincher resources_iA;


    [SerializeField] private int rangoVisionSoldados = 3;
    [SerializeField] private int rangoVisionMineros = 2;

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

    [SerializeField] private GameObject mouseIconZafarero;
    [SerializeField] private GameObject mouseIcon, cellIndicator, basicSoldierPrefab, basicSoldierPickAxePrefab;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private Grid grid_;
    [SerializeField] static public csFogWar fog_;
    [SerializeField] private csFogWar myfog_;

    public static int currentLevel = 0;

    [SerializeField] int spawnCellX;

    [SerializeField] int enemyType;

    [SerializeField] ManagerResourcesTrincher teamResourses;

    #endregion

    public void setSoldierType(int i)
    { 
        print("tu viejjaaaaaa"); 
        enemyType = i;

        mouseIcon.SetActive(enemyType == 0);
        mouseIconZafarero.SetActive(enemyType == 1);

    }


    List<spawnInfo> spawns = new List<spawnInfo>();
    // Update is called once per frame
    void Update()
    {
        //texto UI
        resourcesText.GetComponent<TMP_Text>().text = "+" +resources_player.getResourcesPerSecond();



        Vector3 mousePos = inputManager.GetSelectedMapPoint();
        Vector3Int cellPos = grid_.WorldToCell(mousePos);
        print(cellPos);
        if(enemyType == 0)
        {
            mouseIcon.transform.position = grid_.CellToWorld(cellPos) + placeOffSet;
        }
        else
        {
            mouseIconZafarero.transform.position = grid_.CellToWorld(cellPos) + placeOffSet;
        }
        
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
                    
                    //niebla
                    fog_.AddFogRevealer(new csFogWar.FogRevealer(soldier.transform, rangoVisionSoldados, true));

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
                else if(enemyType == 1 && teamResourses.SpendResourses(200))
                {

                    GameObject soldierPickaxe = Instantiate(basicSoldierPickAxePrefab, grid_.CellToWorld(cellPosSpawn) + placeOffSetSpawn, Quaternion.identity);

                    soldierPickaxe.transform.Rotate(new Vector3(0, 90, 0));

                    fog_.AddFogRevealer(new csFogWar.FogRevealer(soldierPickaxe.transform, rangoVisionMineros, true));

                    
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
        if(levelText != null)
        {
            levelText.SetActive(true);

            levelText.GetComponent<TMP_Text>().text = "LEVEL " + (currentLevel + 1);
        }

        
        if (currentLevel == 0)
        {
            resources_iA.setResourcesAmount(53);
        }
        else if (currentLevel == 1)
        {
            resources_iA.setResourcesAmount(60);
        }
        else if (currentLevel == 2)
        {
            resources_iA.setResourcesAmount(68);
        }
        else if (currentLevel == 3)
        {
            resources_iA.setResourcesAmount(75);
        }
        else if (currentLevel == 4)
        {
            resources_iA.setResourcesAmount(83);
        }

        setSoldierType(0);
        for (int i = 0;i <6;i++)
        {
            spawns.Add(new spawnInfo(false, false, 0));
        }

        fog_ = myfog_;


        if (levelText != null)
        {
            StartCoroutine("Fade");
        }
    }

    IEnumerator Fade()
    {

        yield return new WaitForSeconds(1.5f);
        levelText.gameObject.SetActive(false);
    }
}
