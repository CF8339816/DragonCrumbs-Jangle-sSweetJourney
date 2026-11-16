using UnityEngine;

public class servicehub : MonoBehaviour
{
    public static servicehub instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(instance = null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public ItemManager itemManager;
    public LevelManager levelManager;

    public DialogManager dialogManager;
    
}
