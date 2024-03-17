using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InFrustrumChecker : MonoBehaviour
{
    Camera cam;
    Plane[] frustrumPlanes;
    Collider coll;
    bool isVisible;

    public bool IsVisible {  get { return isVisible; } }
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        coll = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        frustrumPlanes = GeometryUtility.CalculateFrustumPlanes(cam);
        isVisible = GeometryUtility.TestPlanesAABB(frustrumPlanes, coll.bounds);

    }
}
