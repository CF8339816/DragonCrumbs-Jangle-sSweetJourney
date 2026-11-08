using UnityEngine;

public class items
{
    public string itemName;
    public int amountHeld = 0;
    public int amountNeeded;
    public items(string name, int needed)
    {
        itemName = name;
        amountNeeded = needed;
    }
    
    
    public bool HasEnoughOfItem()
    {
        if(amountHeld == amountNeeded)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
    public void AddToItemCount()
    {
        amountHeld += 1;
        if (amountHeld > amountNeeded)
        {
            Debug.Log($"more of {itemName} that there should be");
            amountHeld = amountNeeded;

        }
    }
}
