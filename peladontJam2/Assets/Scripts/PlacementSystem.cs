using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{

    [SerializeField]
    private Vector3 placeOffSet = new Vector3(1, 1, 1);

    [SerializeField]
    private Vector3 placeOffSetSpawn = new Vector3(1, 1, 1);
    #region references

    [SerializeField] private GameObject mouseIcon, cellIndicator,basicSoldierPrefab;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private Grid grid_;

    [SerializeField] int spawnCellX;

    #endregion



    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = inputManager.GetSelectedMapPoint();
        Vector3Int cellPos = grid_.WorldToCell(mousePos);
        print(cellPos);
        mouseIcon.transform.position = grid_.CellToWorld(cellPos) +placeOffSet;
        cellIndicator.transform.position = grid_.CellToWorld(cellPos);

        if (Input.GetMouseButtonDown(0))
        {
            SpawnBasicSoldier();
        }
    }

    public void SpawnBasicSoldier()
    {
        Vector3 mousePos = inputManager.GetSelectedMapPoint();
        Vector3Int cellPos = grid_.WorldToCell(mousePos);

        cellPos.x = spawnCellX;

        GameObject soldier = Instantiate(basicSoldierPrefab, grid_.CellToWorld(cellPos) + placeOffSetSpawn, Quaternion.identity);

        soldier.transform.Rotate(new Vector3(0, 90, 0));
    
    }
}
