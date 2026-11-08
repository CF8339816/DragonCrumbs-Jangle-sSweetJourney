using UnityEngine;

public class Talk_OrRead : MonoBehaviour,IInteractable
{
    [TextArea]public string[] Dialog;

    public virtual void Interact()
    {
        Debug.Log("talked");
        if (!Dialog_Manager.dialogManager.talking)
        {
            Dialog_Manager.dialogManager.StartDialog(Dialog);
        }
        else
        {
            Dialog_Manager.dialogManager.ContinueDialog();
        }
    }
}
