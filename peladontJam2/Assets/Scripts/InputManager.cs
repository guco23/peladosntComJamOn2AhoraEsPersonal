using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    #region references

    private Camera sceneCamera;

    [SerializeField] private LayerMask placementLayer;

    #endregion

    #region parameters

    private Vector3 lastPos_;

    #endregion

    #region methods

    public Vector3 GetSelectedMapPoint()
    {

        Vector3 mousePos = Input.mousePosition;

        mousePos.z = sceneCamera.nearClipPlane;

        Ray ray = sceneCamera.ScreenPointToRay(mousePos);

        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 100, placementLayer))
        {

            lastPos_= hit.point;

        }

        return lastPos_;

    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        sceneCamera= Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
