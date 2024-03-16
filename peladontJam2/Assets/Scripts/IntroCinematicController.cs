using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroCinematicController : MonoBehaviour
{

    [SerializeField] private GameObject _menuUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void activateMenuUI()
    {
        _menuUI.SetActive(true);
        Debug.Log("activar menu");
    }
}
