using UnityEngine;

public class PassiveItemCheckObject : MonoBehaviour
{
    public string itemToCheck;
    public float timer = 5;
    private bool triggered = false;// ---- checkto see if the level change is triggered----CF
    public int nextSceneIndex = 4; // ----Default back to Bakery----CF

    // Update is called once per frame
    void Update()
    {
        if (triggered || ItemManager.instace == null) return;//---- added to ensure load level by index iss not called every frame once timer runs ou t----CF
        if (ItemManager.instace.CheckItem(itemToCheck))
        {
            Debug.Log("Item qouta reached, timer to next scene at " + timer);
            timer -= Time.deltaTime;
            //if (Time.deltaTime < 0)///----change  the format to allow timer to reach 0 to trigger level change ----CF
                if (timer <= 0)
            {
                triggered = true;// ---- checkto see if the level change is triggered----CF
                if (MenuManager.Instance != null)// ----enssures menumanager is present for call to level change----CF
                {
                    MenuManager.Instance.LoadLevelByIndex(nextSceneIndex); // ----Calls next scene----CF
                }
            }
        }
        
    }
}
