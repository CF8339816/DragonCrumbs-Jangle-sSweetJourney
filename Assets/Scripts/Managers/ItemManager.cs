using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    // move to survice hub/game manager latter
    public static ItemManager instace;
    public List<items> recipe = new List<items>();
    public string cleanItem;
    public int newCount;
    public string item;

    public enum Recpie
    {
        none,
        test,
        one,
        two
    }
    public Recpie currentRecpie;
 //private BootStrappin bootStrappin;
    private void Awake() //----moved here to ensure it runs the exists check before other scripts to prtevent error---cf
    {
        BootStrappin bootStrappin = FindFirstObjectByType<BootStrappin>();
        if (bootStrappin != null)
        {
            bootStrappin.FindManagers();
        }
        //Changed cause the code below throws a couple of random errors depending on load orders ensures first load 

        //bootStrappin.FindManagers();
        //remove when instance is moved to survace hub
        if (instace == null)
        {

            instace = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      
     

        ////remove when instance is moved to survace hub
        //if (instace == null)
        //{
        //    instace = this;
        //    DontDestroyOnLoad(this.gameObject);
        //}
        //else
        //{
        //    Destroy(this.gameObject);
        //}

        LoadRecipe(currentRecpie);
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
        recipe.Add(new items("flour", 1));
        recipe.Add(new items("honey", 1));
        recipe.Add(new items("wheat", 5));
        
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
    public void SyncWithStageAndUI(string cleanItem, int newCount)//---- added to bridge to  Stageinteractmanager and PlayerHUD text strings----CF
    {
        StageInteractManager stageInteractManager = FindFirstObjectByType<StageInteractManager>();
        ObjectButtonsInteractables objectButtonsInteractables = FindFirstObjectByType<ObjectButtonsInteractables>();

        if (stageInteractManager != null)
        {
            switch (cleanItem)
            {
                case "apple":
                    stageInteractManager.AppleCount = newCount;
                    if (objectButtonsInteractables != null) objectButtonsInteractables.SetApple_TXT();
                    break;
                case "wheat":
                    stageInteractManager.WheatCount = newCount;
                    if (objectButtonsInteractables != null) objectButtonsInteractables.SetWheat_TXT();
                    break;
                case "flour":
                    stageInteractManager.FlourCount = newCount;
                    if (objectButtonsInteractables != null) objectButtonsInteractables.SetFlour_TXT();
                    break;
                case "honey":
                    stageInteractManager.HoneyCount = newCount;
                    if (objectButtonsInteractables != null) objectButtonsInteractables.SetHoney_TXT();
                    break;
            }
        }
    }
    public void AddToItem(string item)//---- major changes to try and getthe output to work accross scripts for the quest tracking hud outputs fullrework of method----CF
    {

        if (string.IsNullOrEmpty(item))
        {
            Debug.LogWarning("[ItemManager] AddToItem was called with an EMPTY item name! Check 'Item To Add' on your pickup prefab.");
            return;
        }

        if (recipe == null || recipe.Count == 0)
        {
            LoadRecipe(Recpie.one);
        }

        string cleanItem = item.Trim().ToLower();
        bool matched = false;

        for (int i = 0; i < recipe.Count; i++)
        {
            if (recipe[i].itemName.ToLower() == cleanItem)
            {
                matched = true;
                Debug.Log($"[ItemManager] Collected '{cleanItem}' -> New count: {recipe[i].amountHeld + 1}/{recipe[i].amountNeeded}");
                recipe[i].AddToItemCount();

                
                SyncWithStageAndUI(cleanItem, recipe[i].amountHeld);//sync with StageInteractManager and HUD UI

                if (recipe[i].amountHeld == recipe[i].amountNeeded)
                {
                    //do stuff here
                }
            }
        }

        if (!matched)
        {
            Debug.LogWarning($"[ItemManager] Picked up '{item}', which does not match 'apple', 'wheat', 'flour', or 'honey'!");
        }




        //item.ToLower();
        //for(int i = 0; i < recipe.Count; i++)
        //{

        //    //if (recipe[i].itemName.ToLower() == item.ToLower())
        //    if (recipe[i].itemName.ToLower() == cleanItem)//---- Changed to bridge to  Stageinteractmanager and PlayerHUD text strings----CF
        //    {

        //        Debug.Log($"you got {item}");
        //        recipe[i].AddToItemCount();

        //        SyncWithStageAndUI(cleanItem, recipe[i].amountHeld);//---- added to bridge to  Stageinteractmanager and PlayerHUD text strings----CF

        //        if (recipe[i].amountHeld == recipe[i].amountNeeded)
        //        {
        //            //do stuff here
        //        }
        //    }
        //}
    }


  


    public bool CheckItem(string item)
    {
        if (string.IsNullOrEmpty(item)) return false;//---- added to bridge to  Stageinteractmanager and PlayerHUD text strings----CF

        bool hasEnoughOfItem = false;
        //item.ToLower();
        string cleanItem = item.ToLower();//---- changed to bridge to  Stageinteractmanager and PlayerHUD text strings----CF

        for (int i = 0; i < recipe.Count; i++)

        {
            //if (recipe[i].itemName.ToLower() == item.ToLower())
            if (recipe[i].itemName.ToLower() == cleanItem)//---- Changed to bridge to  Stageinteractmanager and PlayerHUD text strings----CF
            {
                hasEnoughOfItem = recipe[i].HasEnoughOfItem();
                if (hasEnoughOfItem)
                {
                    Debug.Log($"had enough of {item}");
                }
            }
        }



        return hasEnoughOfItem;
        
    }
}
