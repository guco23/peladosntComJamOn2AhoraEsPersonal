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
    [SerializeField]
    [Tooltip("Campo para probar la caracteríatica de sacar a los bichones de la trinchera")]
    bool debugSacar;
    [SerializeField]
    [Tooltip("El tamaño de la trinchera, del 1 al 5")]
    int tamañoDeTrinchera;
    Grid grid; //La y es el carril en el que está

    // Start is called before the first frame update.
    void Start()
    {
        //El la hora de hacer STRING TYPING
        grid = GameObject.Find("Grid").GetComponent<Grid>();
        contenidos = new GameObject[30];
        ocupacion = 0;
        estado = ControlTrinchera.VACIA;
        ComprobarFusion();
    }

    private void Update()
    {
        //ESTO ES PARA PODER PROBAR LA FUNCIONALIDAD HABRA QUE QUITARLO MAS TARDE
        if(debugSacar) {
            SacarDeTrinchera();
            debugSacar = false;
        }
    }

    //Cambia la posesion de la trinchera
    public void CambiarControl(ControlTrinchera estado)
    {
        //Hacer todo lo que sea necesario al cambiar el control de la trinchera
        this.estado = estado;
        this.gameObject.layer = (int) estado;
        ComprobarFusion();
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
        estado = ControlTrinchera.VACIA;
    }

    //Saca a todas las unidades en la trinchera.
    public void SacarDeTrinchera() {
        for (int i = 0; i < ocupacion; i++)
        {
            SacarSoldado(contenidos[i]);
        }
        estado = ControlTrinchera.VACIA;
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
        soldado.transform.position = this.transform.position - new Vector3(0,0.5f,0);
    }

    private void SacarSoldado(GameObject soldado)
    {
        soldado.transform.position = this.transform.position + new Vector3(0, 0.5f, 0);
        soldado.GetComponent<SoldierMoveComponent>().continueMoving();
        soldado.GetComponent<SoldierMoveComponent>().setEstadoSoldado(EstadoSoldado.SOLDADO_EN_CAMPO);
    }

    //Lanzar al crear la instancia de trinchera y al cambiar de bando
    private void ComprobarFusion()
    {
        //Debe comprobar si hay trincheras adyacentes, con el mismo bando
        //Crea una instancia del tamaño apropiado, suma sus características y destruye las anteriores.
        //Primero recogemos los objetos de las casillas adyacentes
        Vector3 izq = grid.CellToWorld(grid.WorldToCell(this.transform.position) + new Vector3Int(0, 0, 0));
        Vector3 der = grid.CellToWorld(grid.WorldToCell(this.transform.position) + new Vector3Int(0, 0, 0));
        RaycastHit hitIzq;
        RaycastHit hitDer;


        //Comprobar para el objeto en la izquierda
        if (Physics.Raycast(izq, Vector3.down, out hitIzq, 100, LayerMask.GetMask("Aliado", "Enemigo", "TrincheraVacia"))) {
            GameObject objIzq = hitIzq.collider.gameObject;
            Debug.Log("si, esta encontrando algo algo");

            if (objIzq.layer == this.gameObject.layer)
                Fusionar(objIzq);
        }
        Debug.DrawLine(izq, izq + new Vector3(0,-10,0), Color.green, 10);
        //Comprobar para el objeto en la derecha
        if (Physics.Raycast(der, Vector3.down, out hitDer, 100, LayerMask.GetMask("Aliado", "Enemigo", "TrincheraVacia")))
        {
            GameObject objDer = hitDer.collider.gameObject;
            Debug.Log("si, esta encontrando algo");

            if (objDer.layer == this.gameObject.layer)
                Fusionar(objDer);
        }
        Debug.DrawLine(der, der + new Vector3(0, 10, 0), Color.green, 10);

        //RECORDATORIO: los soldados de la trinchera pueden atacar a cualquiera de las filas accesibles.
    }

    public int GetCarril()
    {
        return grid.WorldToCell(this.transform.position).y;
    }

    public void Fusionar(GameObject trinchera)
    {
        Debug.Log("fusionar con trinchera en " + trinchera.transform.ToString());
    }
}
