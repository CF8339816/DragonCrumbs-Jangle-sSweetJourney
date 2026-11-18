using UnityEngine;

public class ToggleMaze : MonoBehaviour,IInteractable
{
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
        }
        else
        {
            Debug.Log("deactivating maze");
            maze.SetActive(enableMaze);
        }
    }
}
