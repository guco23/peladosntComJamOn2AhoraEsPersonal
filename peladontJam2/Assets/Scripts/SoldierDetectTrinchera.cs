using UnityEngine;

public class SoldierDetectTrinchera : MonoBehaviour
{
    //Collider collider;
    // Start is called before the first frame update

    void OnTriggerEnter(Collider other) {
        //Al encontrar una trinchera, llama para meterse en ella, es esta la que hace todos los cambios necesarios en el soldado
        TrincheraManager tm = other.gameObject.GetComponent<TrincheraManager>();
        if(tm != null) {
            tm.EntrarEnTrinchera(this.gameObject);
        }
        
    }
}
