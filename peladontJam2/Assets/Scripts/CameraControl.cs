using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class CameraControl : MonoBehaviour
{

	#region parameters

	[SerializeField] private float speed_;

    #endregion

    private Vector3 move;
    private Vector2 myDir;
    private CharacterController controller;
    private Camera mainCamera;

    #region methods


    public void DirMovement(InputAction.CallbackContext context)
    {

        myDir = context.ReadValue<Vector2>();

    }
    #endregion

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        move = (myDir.y * mainCamera.transform.forward + myDir.x * mainCamera.transform.right) * speed_;

        controller.Move(move * Time.deltaTime);

    }

}
