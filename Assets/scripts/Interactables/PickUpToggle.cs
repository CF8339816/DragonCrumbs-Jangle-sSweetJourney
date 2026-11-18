using UnityEngine;

public class PickUpToggle : MonoBehaviour,IInteractable
{
    //repercents current state
    public bool enableTriggers = false;
    //refernace to all triggers to turn on
    public Collider2D[] Triggers;
    public virtual void Interact()
    {
        enableTriggers = !enableTriggers;
        if (enableTriggers)
        {
            Debug.Log("activating triggers");
            for (int i = 0; i < Triggers.Length; i++)
            {
                Triggers[i].enabled = true;
            }
            
        }
        else
        {
            Debug.Log("deactivating triggers");
            for (int i = 0; i < Triggers.Length; i++)
            {
                Triggers[i].enabled = false;
            }
            
        }
    }

    
}
