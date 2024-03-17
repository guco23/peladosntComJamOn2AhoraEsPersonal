using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

//Al agregar este script como componente, hacedlo en uno que NO SEA LA PROPIA UI DE ESTO, porque se va a desactivar uwu
public class SelectorTrinchera : MonoBehaviour
{
    //Metes aquí el elemento de U
    [SerializeField] private GameObject UIElement;
    //El layer mask de las trincheras que puedes acceder (aliados)
    [SerializeField] private LayerMask layerMask;
    //El texto del numerito de la cantidad que sacas
    [SerializeField] private TMP_Text textNumero;
    TrincheraManager trincheraManager;
    public void SelectTrinchera(InputAction.CallbackContext callback)
    {
        if(callback.started)
        {
            Vector3 mousePos = Input.mousePosition;
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(mousePos);

            if (Physics.Raycast(ray, out hit, 10000, layerMask))
            {
                GameObject obj = hit.collider.gameObject;
                if (obj.GetComponent<TrincheraManager>() != null)
                {
                    UIElement.SetActive(true);
                    trincheraManager = obj.GetComponent<TrincheraManager>();
                    UpdateNumber();
                }
            }
            else
            {
                UIElement.SetActive(false);
            }
        }
    }

    public void Aumentar()
    {
        trincheraManager.Aumentar();
        UpdateNumber();
    }

    public void Reducir()
    {
        trincheraManager.Reducir();
        UpdateNumber();
    }

    //Se llama sacar todos, pero deberia sacar la cantidad que el jugador ha seleccionado lol
    public void SacarTodos()
    {
        trincheraManager.SacarDeTrinchera();
    }

    private void UpdateNumber()
    {       
        textNumero.text = trincheraManager.getAmmountToSalir().ToString();
    }
}
