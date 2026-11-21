using UnityEngine;
/// <summary>
/// Kevin's scripts combined into one manager for  streamlining.---CF
/// I have had to rework the entire code block to convert the variable references and 
/// naming conventions to match the larger block of code as treying to bridge the two 
/// blocks did not work ..... A.K.A. "The Code of Theseus"
/// </summary>
/// 

public class ItemPickUp : MonoBehaviour, IInteractable
{
    public string itemToAdd;
    public StageInteractManager stageInteractManager;
    public ObjectButtonsInteractables objectButtonsInteractables;

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

    public void Interact()
    {
        if (stageInteractManager == null) stageInteractManager = FindFirstObjectByType<StageInteractManager>();
        if (objectButtonsInteractables == null) objectButtonsInteractables = FindFirstObjectByType<ObjectButtonsInteractables>();

        if (stageInteractManager != null && !string.IsNullOrEmpty(itemToAdd))
        {
            stageInteractManager.AddCollectedItem(itemToAdd);
        }
        else if (ItemManager.instace != null)
        {
            ItemManager.instace.AddToItem(itemToAdd);
        }

        Destroy(this.gameObject);
    }
}