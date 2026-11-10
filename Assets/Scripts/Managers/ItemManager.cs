using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    // move to survice hub/game manager latter
    public static ItemManager instace;
    public List<items> recipe = new List<items>();
    public enum Recpie
    {
        none,
        test,
        one,
        two
    }
    public Recpie currentRecpie;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //remove when instace is moved to survace hub
        if(instace == null)
        {
            instace = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    

    public void LoadRecipe(Recpie rec)
    {
        currentRecpie = rec;
        switch (currentRecpie)
        {
            case Recpie.none:
                unloadRecipe();

                break;

            case Recpie.test:
                LoadTestRecipe();

                break;


            case Recpie.one:

                LoadLevel1Recipe();
                break;


            case Recpie.two:
                LoadLevel2Recipe();

                break;
        }
    }
    public void unloadRecipe()
    {
        recipe.Clear();
    }
    private void LoadLevel1Recipe()
    {
        recipe.Clear();
        recipe.Add(new items("apple",5));
        recipe.Add(new items("flower", 1));
        recipe.Add(new items("honey", 1));
        recipe.Add(new items("weat", 5));
        
    }
    private void LoadLevel2Recipe()
    {
        recipe.Clear();
        recipe.Add(new items("placeholder", 1));
        recipe.Add(new items("placeholder", 1));
        recipe.Add(new items("placeholder", 1));
        recipe.Add(new items("placeholder", 1));

    }
    private void LoadTestRecipe()
    {
        recipe.Clear();
        
    }
    public void AddToItem(string item)
    {
        item.ToLower();
        for(int i = 0; i < recipe.Count; i++)
        {
            if (recipe[i].itemName.ToLower() == item.ToLower())
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
            if (recipe[i].itemName.ToLower() == item.ToLower())
            {
                Debug.Log($"had enough of {item}");
                hasEnoughOfItem = recipe[i].HasEnoughOfItem();
            }
        }



        return hasEnoughOfItem;
        
    }
}
