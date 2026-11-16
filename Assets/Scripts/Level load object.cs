using UnityEngine;

public class Levelloadobject : MonoBehaviour
{
    public string levelToLoad;
    
    public void LoadLevel()
    {
        Debug.Log("tried to load " + levelToLoad);
        //GameManager.instance.levelManager.LoadLevel(levelToLoad);
        
    }
    public void QuitGame()
    {
        //GameManager.instance.levelManager.QuitGame();
    }
}
