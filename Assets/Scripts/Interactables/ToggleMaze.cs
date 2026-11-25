using UnityEngine;
/// <summary>
/// Kevin's scripts combined into one manager for  streamlining.---CF
/// I have had to rework the entire code block to convert the variable references and 
/// naming conventions to match the larger block of code as treying to bridge the two 
/// blocks did not work ..... A.K.A. "The Code of Theseus"
/// </summary>
/// 

public class ToggleMaze : MonoBehaviour, IInteractable
{
    public StageInteractManager stageInteractManager;
    public ObjectButtonsInteractables objectButtonsInteractables;
    //repercents current state
    public bool enableMaze = false;
    //refernace to all triggers to turn on
    public GameObject maze;
    private AddAudio addAudio;
    void Awake()
    {
        BootStrappin bootStrappin = FindFirstObjectByType<BootStrappin>();
        if (bootStrappin != null)
        {
            bootStrappin.FindManagers();
            if (stageInteractManager == null) stageInteractManager = bootStrappin.stageInteractManager;
            if (objectButtonsInteractables == null) objectButtonsInteractables = bootStrappin.objectButtonsInteractables;
        }
    }
    void Start()
    {
       
    }
    public virtual void Interact()
    {
      

        if (stageInteractManager == null) stageInteractManager = FindFirstObjectByType<StageInteractManager>();
        if (objectButtonsInteractables == null) objectButtonsInteractables = FindFirstObjectByType<ObjectButtonsInteractables>();

        enableMaze = !enableMaze;
        if (enableMaze)
        {
            Debug.Log("activating maze");
            if (maze != null)
            {
                maze.SetActive(enableMaze);
                addAudio.OnBeeHive();

            }
            if (stageInteractManager != null)
            {
                stageInteractManager.AppleCount = 5;
                stageInteractManager.FlourCount = 1;
                stageInteractManager.HoneyCount = 1;
                stageInteractManager.SaveCountsToPersistent();
            }

            if (objectButtonsInteractables != null)
            {
                objectButtonsInteractables.RefreshAllIngredientTexts();
            }
        }
        else
        {
            Debug.Log("deactivating maze");
            if (maze != null) maze.SetActive(enableMaze);
       
          
        }
    }
}