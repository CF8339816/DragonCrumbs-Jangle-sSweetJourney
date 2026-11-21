using UnityEngine;
/// <summary>
/// Kevin's scripts combined into one manager for  streamlining.---CF
/// I have had to rework the entire code block to convert the variable references and 
/// naming conventions to match the larger block of code as treying to bridge the two 
/// blocks did not work ..... A.K.A. "The Code of Theseus"
/// </summary>
/// 

public class DialogWthCheck : TalkOrRead, IInteractable
{
    public StageInteractManager stageInteractManager;
    public ObjectButtonsInteractables objectButtonsInteractables;
    private ItemManager itemManager;

    [TextArea] public string[] secondDialogPass;
    [TextArea] public string[] secondDialogFail;

    [TextArea] public string[] DialogAfterGvingItem;
    public string itemToCheck;

    private bool itemCheckPassed = false;
    public string itemToAdd;

    private bool itemAdded = false;

    private void Start()
    {
        //either set item manger to static or replace it in other code with static
        itemManager = ItemManager.instace;
        if (stageInteractManager == null) stageInteractManager = FindFirstObjectByType<StageInteractManager>();
        if (objectButtonsInteractables == null) objectButtonsInteractables = FindFirstObjectByType<ObjectButtonsInteractables>();
    }

    public override void Interact()
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

            if (itemAdded)
            {
                dialogManager.StartDialog(DialogAfterGvingItem);
            }
            else if (hasEnough)
            {
                itemCheckPassed = true;
                dialogManager.StartDialog(Dialog, secondDialogPass);
            }
            else
            {
                dialogManager.StartDialog(Dialog, secondDialogFail);
            }
        }
        else
        {
            dialogManager.ContinueDialog();
        }
    }

    private void Update()
    {
        if (!string.IsNullOrEmpty(itemToAdd) && dialogManager != null)
        {
            if (itemCheckPassed && dialogManager.talking == false && itemAdded == false)
            {
                Debug.Log($"gave {itemToAdd}");
                if (stageInteractManager == null) stageInteractManager = FindFirstObjectByType<StageInteractManager>();

                if (stageInteractManager != null)
                {
                    stageInteractManager.AddCollectedItem(itemToAdd);
                }
                else if (itemManager != null)
                {
                    itemManager.AddToItem(itemToAdd);
                }
                itemAdded = true;
            }
        }
    }
}