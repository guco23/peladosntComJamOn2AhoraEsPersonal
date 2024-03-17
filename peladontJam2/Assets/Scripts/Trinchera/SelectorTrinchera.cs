using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SelectorTrinchera : MonoBehaviour
{
    [SerializeField] private GameObject UIElement;
    [SerializeField] private LayerMask layerMask;
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

    public void SacarTodos()
    {
        trincheraManager.SacarDeTrinchera();
    }

    private void UpdateNumber()
    {       
        textNumero.text = trincheraManager.getAmmountToSalir().ToString();
    }
}
