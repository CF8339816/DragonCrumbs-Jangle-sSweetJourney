using UnityEngine;

public class ItemPickUp : MonoBehaviour,IInteractable
{
    public string itemToEffect;


    public ItemManager itemManager;

    private void Start()
    {
        //set item manager here or replace item manger refeance to static instance
    }

    public void Interact()
    {
        itemManager.AddToItem(itemToEffect);
    }
}
