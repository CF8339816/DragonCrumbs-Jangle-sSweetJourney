using UnityEngine;

public class items : MonoBehaviour
{
    public string itemName;
    public int amountHeld = 0;
    public int amountNeeded;
    public items(string name, int needed)
    {
        itemName = name;
        amountNeeded = needed;
    }
    
    public void GotItem()
    {
        amountHeld += 1;
        if(amountHeld > amountNeeded)
        {
            Debug.Log($"more of {itemName} that there should be");
            amountHeld = amountNeeded;

        }
    }
}
