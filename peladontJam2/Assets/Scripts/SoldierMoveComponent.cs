using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EstadoSoldado {
    SOLDADO_EN_CAMPO,
    SOLDADO_ATRINCHERADO
}
//Queremos que el soldadito
[RequireComponent(typeof(Rigidbody))]
public class SoldierMoveComponent : MonoBehaviour
{
    Rigidbody rb;
    [Tooltip("el estado del soldado")]
    [SerializeField]
    EstadoSoldado estado;
    [SerializeField]
    [Tooltip("La velocidad del personaje")]
    float speed;
    public FMODUnity.EventReference inputsound;
    public float timeBetweenSteps = 0.5f;

    private float time;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();

        continueMoving();

    }
    private void Update()
    {
        if(time < 0 && rb.velocity.magnitude >0)
        {
            time = timeBetweenSteps;
            EventInstance soundInstance = RuntimeManager.CreateInstance(inputsound.Path);
            soundInstance.start();
            soundInstance.release();

        }

        time -=Time.deltaTime;
    }

    public void stopMoving() {

        Debug.Log("Stop");

        rb.velocity = Vector3.zero;
    }

    public void continueMoving() {
        rb.velocity = transform.forward * speed;
        
    }
    public EstadoSoldado GetEstadoSoldado() {
        return estado;
    }
    public void setEstadoSoldado(EstadoSoldado estado) {
        this.estado = estado;
    }
}
