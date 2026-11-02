using UnityEngine;

public class DialogWthCheck : TalkOrRead
{

    public ItemManager itemManager;

    [TextArea] public string[] secondDialogPass;
    [TextArea] public string[] secondDialogFail;
    public string itemToCheck;
    private void Start()
    {
        //either set item manger to static or replace it in other code with static
    }
    public override void Interact()
    {
        Debug.Log("talked");
        if (!DialogManager.dialogManager.talking)
        {
            if (itemManager.CheckItem(itemToCheck))
            {
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
}
