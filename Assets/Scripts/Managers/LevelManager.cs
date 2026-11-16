using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    
    public void LoadLevel(string level)
    {
        Debug.Log("loaded " + level);
        SceneManager.LoadScene(level);
    }
    public void QuitGame()
    {
        Debug.Log("quit game");
        Application.Quit();
    }
    
}
