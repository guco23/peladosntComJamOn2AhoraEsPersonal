using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Indica quien controla la trinchera, los valores por defecto son los mismos que la layer del mismo
public enum ControlTrinchera
{
    ALIADOS = 6,
    ENEMIGOS = 7,
    VACIA = 10
}

public class TrincheraManager : MonoBehaviour
{
    [Tooltip("El numero maximo de soldados que admite la trinchera")]
    [SerializeField]
    int MAX_OCUP;
    //Los soldados que están resguardados en esta trinchera.
    GameObject[] contenidos;
    ControlTrinchera estado;
    //El numero de gente en la trinchera (y el indice en el que colocar el siguiente).
    int ocupacion;

    // Start is called before the first frame update.
    void Start()
    {
        contenidos = new GameObject[30];
        ocupacion = 0;
        estado = ControlTrinchera.VACIA;
    }


    //Cambia la posesion de la trinchera
    public void CambiarControl(ControlTrinchera estado)
    {
        //Hacer todo lo que sea necesario al cambiar el control de la trinchera
        this.estado = estado;
        this.gameObject.layer = (int) estado;

    }

    //A llamar cuando un soldado llega a la trinchera, para meterse dentro, si devuelve true ha entrado y hace lo que proceda, si devuelve false no ha entrado.
    public bool EntrarEnTrinchera(GameObject soldado)
    {
        if(estado == ControlTrinchera.VACIA) {
            CambiarControl((ControlTrinchera) soldado.layer);
        } else if(ocupacion >= MAX_OCUP) {
            return false;
        }
        //Añade al soldado al array de soldados
        MeterSoldado(soldado);
        return true;
    }

    //A llamar cuando la trinchera haya MUERTO, es decir, se haya quedado sin vida.
    public void DeadTrinchera()
    {
        estado = ControlTrinchera.VACIA;
    }

    //Saca a todas las unidades en la trinchera.
    public void SacarDeTrinchera() {
        
    }
    
    //Aqui el tema de detener al soldado, agregar la vida, ocultarlo, etc
    private void MeterSoldado(GameObject soldado) {
        contenidos[ocupacion] = soldado;
        ocupacion++;
        //Detiene al soldado y lo indica como estado de soldado atrincherado
        soldado.GetComponent<SoldierMoveComponent>().stopMoving();
        soldado.GetComponent<SoldierMoveComponent>().setEstadoSoldado(EstadoSoldado.SOLDADO_ATRINCHERADO);
        //Cambia su posicion para estar metido abajo, en la trinchera
        soldado.transform.position = this.transform.position - new Vector3(0,0.5f,0);
    }


}
