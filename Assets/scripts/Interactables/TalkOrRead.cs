using UnityEngine;

public class TalkOrRead : MonoBehaviour,IInteractable
{
    [TextArea]public string[] Dialog;

    public virtual void Interact()
    {
        Debug.Log("talked");
        if (!servicehub.instance.dialogManager.talking)
        {
            servicehub.instance.dialogManager.StartDialog(Dialog);
        }
        else
        {
            servicehub.instance.dialogManager.ContinueDialog();
        }
    }
}
