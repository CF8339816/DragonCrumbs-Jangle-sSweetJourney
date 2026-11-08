using UnityEngine;

public class Item_PickUp : MonoBehaviour,IInteractable
{
    public string itemToEffect;


    public Item_Manager itemManager;

    private void Start()
    {
        //set item manager here or replace item manger refeance to static instance
    }

    public void Interact()
    {
        itemManager.AddToItem(itemToEffect);
        Destroy(this.gameObject);
    }
}
