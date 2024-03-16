using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class showresources : MonoBehaviour
{
    [SerializeField]
    ManagerResourcesTrincher playerResources;

    [SerializeField]
    TMP_Text textMeshPro;

    [SerializeField]
    Slider resourcesSlider;

    private void Update()
    {
        textMeshPro.text = "Resources: " + playerResources.getResources();
        resourcesSlider.value = playerResources.getResources();
    }


}

