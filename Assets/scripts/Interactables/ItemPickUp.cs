using UnityEngine;

public class ItemPickUp : MonoBehaviour,IInteractable
{
    public string itemToEffect;


    public ItemManager itemManager;
    

    private void Start()
    {
        //set item manager here or replace item manger refeance to static instance
        itemManager = ItemManager.instace;
    }

    public void Interact()
    {
        itemManager.AddToItem(itemToEffect);
        Destroy(this.gameObject);
    }
}
