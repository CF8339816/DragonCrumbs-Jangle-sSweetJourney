using UnityEngine;

public class PassiveItemCheckObject : MonoBehaviour
{
    public string itemToCheck;
    public float timer = 5;

    // Update is called once per frame
    void Update()
    {
        if (ItemManager.instace.CheckItem(itemToCheck))
        {
            Debug.Log("Item qouta reached, timer to next scene at " + timer);
            timer -= Time.deltaTime;
            if(Time.deltaTime < 0)
            {
                //level change here
            }
        }
    }
}
