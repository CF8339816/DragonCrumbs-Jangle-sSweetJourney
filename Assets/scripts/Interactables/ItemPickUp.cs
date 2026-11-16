using UnityEngine;

public class ItemPickUp : MonoBehaviour,IInteractable
{
    public string itemToAdd;


    

    public void Interact()
    {

        servicehub.instance.itemManager.AddToItem(itemToAdd);
        Destroy(this.gameObject);
    }
}
