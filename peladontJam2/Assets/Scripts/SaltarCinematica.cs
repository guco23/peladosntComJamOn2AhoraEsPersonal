using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SaltarCinematica : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera camaraAnimacion;
    [SerializeField]
    private GameObject menuUi;

    public void SaltarAnimacion(InputAction.CallbackContext callback)
    {
        if (callback.started)
        {
            print("tu vejaaaa");
            camaraAnimacion.enabled = false;


            StartCoroutine(activeMenu());
        }
    }

    IEnumerator activeMenu()
    {

        yield return new WaitForSecondsRealtime(2.0f);
        menuUi.SetActive(true);

    }

}

