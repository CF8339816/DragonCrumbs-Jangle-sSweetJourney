using UnityEngine;

public class ItemPickUp : MonoBehaviour,IInteractable
{
    public string itemToAdd;


    

    public void Interact()
    {

        ItemManager.instace.AddToItem(itemToAdd);
        Destroy(this.gameObject);
    }
}
