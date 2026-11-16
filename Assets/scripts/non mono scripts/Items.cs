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
        //if(amountHeld == amountNeeded)
        if (amountHeld >= amountNeeded)//----safer in case  accidental pick up more than limit----CF
        {
            amountHeld = amountNeeded;// creates a hard cut off at cap on item outputs to prevent errors
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
        Debug.Log($"you have {amountHeld} out of {amountNeeded} {itemName}s");
        if (amountHeld > amountNeeded)
        {
            Debug.Log($"more of {itemName} that there should be");
            amountHeld = amountNeeded;

        }
    }
}
