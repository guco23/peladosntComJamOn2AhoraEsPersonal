using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillSoldiers : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.layer == LayerMask.NameToLayer("Aliado"))
        {

            List<FischlWorks_FogWar.csFogWar.FogRevealer> fogList = PlacementSystem.fog_._FogRevealers;

            bool encontrado = false;

            int i = 0;

            while (i < fogList.Count && !encontrado)
            {
                if (fogList[i]._RevealerTransform == other.gameObject.transform)
                {
                    encontrado = true;
                }
                else i++;
            }

            if (encontrado)
            {
                fogList.RemoveAt(i);
                PlacementSystem.fog_.ReplaceFogRevealerList(fogList);
            }

            Destroy(other.gameObject);

        }

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
