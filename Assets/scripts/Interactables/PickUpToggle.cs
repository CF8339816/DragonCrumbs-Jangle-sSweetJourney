using UnityEngine;
/// <summary>
/// Kevin's scripts combined into one manager for  streamlining.---CF
/// I have had to rework the entire code block to convert the variable references and 
/// naming conventions to match the larger block of code as treying to bridge the two 
/// blocks did not work ..... A.K.A. "The Code of Theseus"
/// </summary>
/// 

public class PickUpToggle : MonoBehaviour, IInteractable
{
    //repercents current state
    public bool enableTriggers = false;
    //refernace to all triggers to turn on
    public Collider2D[] Triggers;

    void Awake()
    {
        BootStrappin bootStrappin = FindFirstObjectByType<BootStrappin>();
        if (bootStrappin != null)
        {
            bootStrappin.FindManagers();
        }
    }

    public virtual void Interact()
    {
        enableTriggers = !enableTriggers;
        if (enableTriggers)
        {
            Debug.Log("activating triggers");
            for (int i = 0; i < Triggers.Length; i++)
            {
                if (Triggers[i] != null)//----added to remove the  missing ref error happening from the destroyeed game objects----CF
                    Triggers[i].enabled = true;
            }

        }
        else
        {
            Debug.Log("deactivating triggers");
            for (int i = 0; i < Triggers.Length; i++)
            {
                if (Triggers[i] != null)//----added to remove the  missing ref error happening from the destroyeed game objects----CF
                    Triggers[i].enabled = false;
            }

        }
    }
}