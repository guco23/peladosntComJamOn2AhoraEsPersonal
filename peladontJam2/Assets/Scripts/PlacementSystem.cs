using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{

    #region references

    [SerializeField] private GameObject mouseIcon, cellIndicator;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private Grid grid_;

    #endregion



    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = inputManager.GetSelectedMapPoint();
        Vector3Int cellPos = grid_.WorldToCell(mousePos);
        mouseIcon.transform.position = mousePos;
        cellIndicator.transform.position = grid_.CellToWorld(cellPos);
    }
}
