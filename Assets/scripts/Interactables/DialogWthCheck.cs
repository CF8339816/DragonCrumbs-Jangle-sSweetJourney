using UnityEngine;

public class DialogWthCheck : TalkOrRead,IInteractable
{

    public ItemManager itemManager;

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
    }
    public override void Interact()
    {
        Debug.Log("talked");
        if (!DialogManager.dialogManager.talking)
        {
            if (itemAdded)
            {
                DialogManager.dialogManager.StartDialog(DialogAfterGvingItem);
            }
            else if (itemManager.CheckItem(itemToCheck))
            {
                itemCheckPassed = true;
                DialogManager.dialogManager.StartDialog(Dialog, secondDialogPass);
            }
            else
            {
                DialogManager.dialogManager.StartDialog(Dialog, secondDialogFail);
            }
            
        }
        else
        {
            DialogManager.dialogManager.ContinueDialog();

        }
    }
    private void Update()
    {
        if (itemToAdd != null)
        {
            if (itemCheckPassed && DialogManager.dialogManager.talking == false && itemAdded == false)
            {
                Debug.Log($"gave {itemToAdd}");
                itemManager.AddToItem(itemToAdd);
                itemAdded = true;
            }
        }
    }
}
