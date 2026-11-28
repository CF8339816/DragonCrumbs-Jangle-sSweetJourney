using UnityEngine;
/// <summary>
/// Kevin's scripts combined into one manager for  streamlining.---CF
/// I have had to rework the entire code block to convert the variable references and 
/// naming conventions to match the larger block of code as treying to bridge the two 
/// blocks did not work ..... A.K.A. "The Code of Theseus"
/// </summary>
/// 

public class TalkOrRead : MonoBehaviour, IInteractable
{
    public DialogManager dialogManager;
    [TextArea] public string[] Dialog;

    protected virtual void Awake()
    {
        BootStrappin bootStrappin = FindFirstObjectByType<BootStrappin>();
        if (bootStrappin != null)
        {
            bootStrappin.FindManagers();
            if (dialogManager == null) dialogManager = bootStrappin.dialogManager;
        }
        if (dialogManager == null) dialogManager = DialogManager.dialogManager;
    }

    public virtual void Interact()
    {
        if (dialogManager == null) dialogManager = DialogManager.dialogManager;
        if (dialogManager == null) return;

        Debug.Log("talked");
        if (!dialogManager.talking)
        {
            dialogManager.StartDialog(Dialog);
        }
        else
        {
            dialogManager.ContinueDialog();
        }
    }
}