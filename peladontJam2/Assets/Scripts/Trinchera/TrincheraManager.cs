using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    [Tooltip("El soldado que spawnea al start")]
    [SerializeField]
    GameObject soldadoInicial;
    int ammountToSalir;

    // Start is called before the first frame update.
    void Start()
    {
        //El la hora de hacer STRING TYPING
        contenidos = new GameObject[30];
        ocupacion = 0;
        ammountToSalir = 0;
        estado = ControlTrinchera.VACIA;
        GameObject soldado = Instantiate(soldadoInicial, gameObject.transform.position, Quaternion.identity);
        //EntrarEnTrinchera(soldado);
    }

    //Cambia la posesion de la trinchera
    public void CambiarControl(ControlTrinchera estado)
    {
        //Hacer todo lo que sea necesario al cambiar el control de la trinchera
        this.estado = estado;
        this.gameObject.layer = (int) estado;
        this.GetComponent<LifeComponentTrinchera>().SetLife(0);
        
    }

    //A llamar cuando un soldado llega a la trinchera, para meterse dentro, si devuelve true ha entrado y hace lo que proceda, si devuelve false no ha entrado.
    public bool EntrarEnTrinchera(GameObject soldado)
    {
        if(estado == ControlTrinchera.VACIA) {
            CambiarControl((ControlTrinchera) soldado.layer);
        } else if(ocupacion >= MAX_OCUP) {
            return false;
        }
        //rotar soldado

        
         
        //soldado.transform.right = gameObject.transform.right;

        if(soldado.layer == LayerMask.NameToLayer("Aliado"))
        {
            soldado.transform.right = gameObject.transform.right;
        }
        else
        {
            soldado.transform.right = -gameObject.transform.right;

        }


        //Añade al soldado al array de soldados
        MeterSoldado(soldado);
        //Añade a la trinchera la vida del soldado
        this.GetComponent<LifeComponentTrinchera>().AddLife(soldado.GetComponent<LifeComponent>().getLife());
        return true;
    }

    //A llamar cuando la trinchera haya MUERTO, es decir, se haya quedado sin vida.
    public void DeadTrinchera()
    {
        //Mata a todos los pibes en la trinchera
        for(int i = 0; i < ocupacion; i++)
        {
            Destroy(contenidos[i]);
        }
        CambiarControl(ControlTrinchera.VACIA);
    }

    //Saca a todas las unidades en la trinchera.
    //La cosa sería utilizar otra forma de sacar para que el jugador seleccione una cantidcad específica.
    public void SacarDeTrinchera() {
        for (int i = 0; i < ocupacion; i++)
        {
            SacarSoldado(contenidos[i]);
        }
        CambiarControl(ControlTrinchera.VACIA);
        ocupacion = 0;
        contenidos = new GameObject[30];
    }

    //Aqui el tema de detener al soldado, agregar la vida, ocultarlo, etc
    private void MeterSoldado(GameObject soldado) {
        contenidos[ocupacion] = soldado;
        ocupacion++;
        //Detiene al soldado y lo indica como estado de soldado atrincherado
        soldado.GetComponent<SoldierMoveComponent>().stopMoving();
        soldado.GetComponent<SoldierMoveComponent>().setEstadoSoldado(EstadoSoldado.SOLDADO_ATRINCHERADO);
        //Cambia su posicion para estar metido abajo, en la trinchera
        soldado.transform.position -=  new Vector3(0,0.5f,0);
        this.GetComponent<LifeComponentTrinchera>().AddLife(soldado.GetComponent<LifeComponent>().getLife());


        if (gameObject.layer == LayerMask.NameToLayer("Aliado")){

            //print("aaaaaaaaaaaaaaaaaaaa");
            soldado.transform.Rotate(new Vector3(0,90,0));
        }
        else
        {
            //print("bbbbbbbbbbbbbbbbbbbbbbb");

            soldado.transform.Rotate(new Vector3(0, -90, 0));
        }
    }

    private void SacarSoldado(GameObject soldado)
    {
        if(soldado == null)
        {
            return;
        }
        //soldado.transform.position = this.transform.position + new Vector3(0, 0.5f, 0);
        soldado.transform.position += new Vector3(0, 0.5f, 0);
        soldado.GetComponent<SoldierMoveComponent>().continueMoving();
        soldado.GetComponent<SoldierMoveComponent>().setEstadoSoldado(EstadoSoldado.SOLDADO_EN_CAMPO);
    }

    public void Aumentar()
    {
        if (ammountToSalir < ocupacion)
            ammountToSalir++;
    }

    public void Reducir()
    {
        if(ammountToSalir > 0)
            ammountToSalir--;
    }

    public int getAmmountToSalir()
    {
        return ammountToSalir;
    }
}