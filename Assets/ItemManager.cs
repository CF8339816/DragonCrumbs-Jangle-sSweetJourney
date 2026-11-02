using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{

    public List<items> recipe = new List<items>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddToItem(string item)
    {
        item.ToLower();
        for(int i = 0; i < recipe.Count; i++)
        {
            if (recipe[i].itemName.ToLower() == item)
            {
                recipe[i].AddToItemCount();
            }
        }
    }
    public bool CheckItem(string item)
    {
        bool hasEnoughOfItem = false;
        item.ToLower();
        for (int i = 0; i < recipe.Count; i++)
        {
            if (recipe[i].itemName.ToLower() == item)
            {
                hasEnoughOfItem = recipe[i].HasEnoughOfItem();
            }
        }



        return hasEnoughOfItem;
        ;
    }
}
