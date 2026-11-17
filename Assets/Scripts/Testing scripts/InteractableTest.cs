using UnityEngine;
using UnityEngine.InputSystem;

public class InteractableTest : MonoBehaviour
{
    private IInteractable currentInteractable;
    public void Interact(InputAction.CallbackContext context)
    {
        if (currentInteractable != null && context.performed)
        {
            
            currentInteractable.Interact();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        if (other.TryGetComponent(out IInteractable foundInteractable))
        {
            currentInteractable = foundInteractable;
            Debug.Log(currentInteractable);



        }
        
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IInteractable foundInteractable))
        {
            currentInteractable = null;



        }
        
    }
}
