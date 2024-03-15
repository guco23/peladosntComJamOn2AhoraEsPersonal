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
    //La cantidad máxima de soldados que pueden ocupar la trinchera.
    const int MAX_OCUP = 30;
    //Los soldados que están resguardados en esta trinchera.
    GameObject[] contenidos;
    ControlTrinchera estado;
    //El numero de gente en la trinchera (y el indice en el que colocar el siguiente).
    int ocupacion;

    // Start is called before the first frame update.
    void Start()
    {
        contenidos = new GameObject[MAX_OCUP];
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

    //A llamar cuando un soldado llega a la trinchera, para meterse dentro
    public void EntrarEnTrinchera(GameObject soldado)
    {
        //Esto tiene que ocultar o hacer lo que sea para la unidad que va a entrar.
        //Y aplicar todos los cambios necesarios en la trinchera.
        if(estado == ControlTrinchera.VACIA) {
            CambiarControl((ControlTrinchera) soldado.layer);
        }
        contenidos[ocupacion] = soldado;
        ocupacion++;
        //Aqui el tema de detener al soldado, agregar la vida, ocultarlo, etc
    }

    //A llamar cuando la trinchera haya MUERTO, es decir, se haya quedado sin vida.
    public void DeadTrinchera()
    {
        estado = ControlTrinchera.VACIA;
    }

    //Saca a todas las unidades en la trinchera.
    public void SacarDeTrinchera() {

    }
}
