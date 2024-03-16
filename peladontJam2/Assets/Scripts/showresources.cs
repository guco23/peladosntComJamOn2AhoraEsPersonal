using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class showresources : MonoBehaviour
{
    [SerializeField]
    ManagerResourcesTrincher playerResources;

    [SerializeField]
    TMP_Text textMeshPro;

    private void Update()
    {
        textMeshPro.text = "Resources: " + playerResources.getResources();
    }


}

