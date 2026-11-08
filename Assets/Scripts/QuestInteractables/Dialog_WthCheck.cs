using UnityEngine;

public class Dialog_WthCheck : TalkOrRead,IInteractable
{

    public Item_Manager itemManager;

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
    }
    public override void Interact()
    {
        Debug.Log("talked");
        if (!Dialog_Manager.dialogManager.talking)
        {
            if (itemAdded)
            {
                Dialog_Manager.dialogManager.StartDialog(DialogAfterGvingItem);
            }
            else if (itemManager.CheckItem(itemToCheck))
            {
                itemCheckPassed = true;
                Dialog_Manager.dialogManager.StartDialog(Dialog, secondDialogPass);
            }
            else
            {
                Dialog_Manager.dialogManager.StartDialog(Dialog, secondDialogFail);
            }
            
        }
        else
        {
            Dialog_Manager.dialogManager.ContinueDialog();

        }
    }
    private void Update()
    {
        if (itemToAdd != null)
        {
            if (itemCheckPassed && Dialog_Manager.dialogManager.talking == false && itemAdded == false)
            {
                Debug.Log($"gave {itemToAdd}");
                itemManager.AddToItem(itemToAdd);
                itemAdded = true;
            }
        }
    }
}
