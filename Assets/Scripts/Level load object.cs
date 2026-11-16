using UnityEngine;

public class Levelloadobject : MonoBehaviour
{
    public string levelToLoad;
    
    public void LoadLevel()
    {
        Debug.Log("tried to load " + levelToLoad);
        servicehub.instance.levelManager.LoadLevel(levelToLoad);
        
    }
    public void QuitGame()
    {
        servicehub.instance.levelManager.QuitGame();
    }
}
