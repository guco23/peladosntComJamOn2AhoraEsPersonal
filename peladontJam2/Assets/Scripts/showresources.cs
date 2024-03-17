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
        textMeshPro.text = playerResources.getResources().ToString();
        resourcesSlider.value = playerResources.getElapsedTime()*1000;
    }


}

