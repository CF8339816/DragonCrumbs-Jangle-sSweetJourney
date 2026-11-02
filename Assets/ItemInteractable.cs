using UnityEngine;

public class ItemInteractable : MonoBehaviour,IInteractable
{
    public string itemToEffect;
    public enum ItemEffect
    {
        addToItem,
        checkItem
    }
    public ItemEffect howEffect;

    public ItemManager itemManager;

    private void Start()
    {
        //set item manager here or replace item manger refeance to static instance
    }

    public void Interact()
    {
        switch (howEffect)
        {

            case ItemEffect.addToItem:

                itemManager.AddToItem(itemToEffect);

                break;




            case ItemEffect.checkItem:


                itemManager.CheckItem(itemToEffect);



                break;


        }
    }

}
