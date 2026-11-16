using UnityEngine;

public class PassiveItemCheckObject : MonoBehaviour
{
    public string itemToCheck;
    public float timer = 5;

    public Levelloadobject levelToLoad;

    // Update is called once per frame
    void Update()
    {
        if (servicehub.instance.itemManager.CheckItem(itemToCheck))
        {
            Debug.Log("Item qouta reached, timer to next scene at " + timer);
            timer -= Time.deltaTime;
            if(Time.deltaTime < 0)
            {
                levelToLoad.LoadLevel();
            }
        }
        
    }
}
