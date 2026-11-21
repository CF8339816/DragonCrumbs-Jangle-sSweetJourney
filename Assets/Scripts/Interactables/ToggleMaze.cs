using UnityEngine;

public class ToggleMaze : MonoBehaviour,IInteractable
{
   public StageInteractManager stageInteractManager;
    //repercents current state
    public bool enableMaze = false;
    //refernace to all triggers to turn on
    public GameObject maze;
    public virtual void Interact()
    {
        enableMaze = !enableMaze;
        if (enableMaze)
        {
            Debug.Log("activating maze");
            maze.SetActive(enableMaze);
            stageInteractManager.AppleCount = 5;
            stageInteractManager.FlourCount = 1;
            stageInteractManager.HoneyCount = 1;
}
        else
        {
            Debug.Log("deactivating maze");
            maze.SetActive(enableMaze);
        }
       
    }
}
