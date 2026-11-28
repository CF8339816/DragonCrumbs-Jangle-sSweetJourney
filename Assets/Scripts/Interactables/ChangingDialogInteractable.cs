using UnityEngine;

public class ChangingDialogInteractable : MonoBehaviour,IInteractable
{
    public StageInteractManager stageInteractManager;
    public ObjectButtonsInteractables objectButtonsInteractables;
    private ItemManager itemManager;

    public DialogManager dialogManager;

    private int currentDialog = 1;
    //plays when first talking, enables actions
    [TextArea] public string[] meetingDialog;
    // remider dialog for after talking the first time but before getting the item needed
    [TextArea] public string[] regualarDialog;
    //plays after item has been colected
    [TextArea] public string[] fianlDialog;



    public string itemToCheck;

    private bool itemCheckPassed = false;
    

    public void updateDialog()
    {
        currentDialog += 1;
        Debug.Log("updating dialog");
    }

    private void Start()
    {
        
        //either set item manger to static or replace it in other code with static
        itemManager = ItemManager.instace;
        if (stageInteractManager == null) stageInteractManager = FindFirstObjectByType<StageInteractManager>();
        if (objectButtonsInteractables == null) objectButtonsInteractables = FindFirstObjectByType<ObjectButtonsInteractables>();
    }

    public void Interact()
    {
        if (dialogManager == null) dialogManager = DialogManager.dialogManager;
        if (dialogManager == null) return;
        if (stageInteractManager == null) stageInteractManager = FindFirstObjectByType<StageInteractManager>();
        
        Debug.Log("talked");
        if (!dialogManager.talking)
        {
            bool hasEnough = (stageInteractManager != null)
                ? stageInteractManager.HasEnoughOfItem(itemToCheck)
                : (itemManager != null && itemManager.CheckItem(itemToCheck));

            if (currentDialog <= 1)
            {
                Debug.Log("dialog 1");
                dialogManager.StartDialog(meetingDialog);
                // enable actions for scene
                updateDialog();
            }
            else if (currentDialog == 2)
            {
                Debug.Log("dialog 2");
                dialogManager.StartDialog(regualarDialog);
            }
            else if (currentDialog >= 3)
            {
                Debug.Log("dialog 3");
                dialogManager.StartDialog(fianlDialog);
                //trigger move on to next scene
            }

        }
        else
        {
            dialogManager.ContinueDialog();
        }
    }

    private void Update()
    {
        if (dialogManager != null)
        {
            if (!itemCheckPassed && itemManager.CheckItem(itemToCheck))
            {
                itemCheckPassed = true;
                updateDialog();
                
            }
            
        }
    }
}
