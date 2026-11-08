using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Item_Manager : MonoBehaviour
{

    public List<items> recipe = new List<items>();
    public bool testing = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(testing == true)
        {
            LoadTestRecipe();
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadLevel1Recipe()
    {
        recipe.Clear();
        recipe.Add(new items("apple",5));
        recipe.Add(new items("flower", 1));
        recipe.Add(new items("honey", 1));
        recipe.Add(new items("weat", 5));
        
    }
    private void LoadTestRecipe()
    {
        recipe.Clear();
        recipe.Add(new items("apple", 1));
        recipe.Add(new items("SPM", 1));
    }
    public void AddToItem(string item)
    {
        item.ToLower();
        for(int i = 0; i < recipe.Count; i++)
        {
            if (recipe[i].itemName.ToLower() == item)
            {

                Debug.Log($"you got {item}");
                recipe[i].AddToItemCount();
                if (recipe[i].amountHeld == recipe[i].amountNeeded)
                {
                    //do stuff here
                }
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
                Debug.Log($"had enough of {item}");
                hasEnoughOfItem = recipe[i].HasEnoughOfItem();
            }
        }



        return hasEnoughOfItem;
        ;
    }
}
