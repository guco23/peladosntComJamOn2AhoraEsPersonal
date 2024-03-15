using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

	#region parameters

	[SerializeField] private float speed_;

    #endregion

    #region methods

    public void MoveCamera(bool dir)
    {

        int moveDir = 0;

        if (dir)
        {
            moveDir = 1;
        }
        else
        {
            moveDir = -1;
        }

        

    }

    #endregion

}
