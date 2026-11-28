using UnityEngine;
/// <summary>
/// Kevin's scripts combined into one manager for  streamlining.---CF
/// I have had to rework the entire code block to convert the variable references and 
/// naming conventions to match the larger block of code as treying to bridge the two 
/// blocks did not work ..... A.K.A. "The Code of Theseus"
/// </summary>
/// 

public interface IInteractable
{
    void Interact();
}

[System.Serializable]
public class items
{
    public string itemName;
    public int amountHeld = 0;
    public int amountNeeded;

    public items(string name, int needed)
    {
        itemName = name;
        amountNeeded = needed;
    }

    public bool HasEnoughOfItem()
    {
        if (amountHeld >= amountNeeded) // ----safer in case accidental pick up more than limit----cf
        {
            amountHeld = amountNeeded; // ----creates a hard cut off at cap on item outputs to prevent errors----cf
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AddToItemCount()
    {
        amountHeld += 1;
        Debug.Log($"you have {amountHeld} out of {amountNeeded} {itemName}s");
        if (amountHeld > amountNeeded) //prevents overfollow of item
        {
            Debug.Log($"more of {itemName} that there should be");
            amountHeld = amountNeeded;
        }
    }
}


public class InteractableManager : MonoBehaviour, IInteractable
{
    public enum InteractionType
    {
        TalkOrRead,
        DialogWithCheck,
        ItemPickUp
    }

    [Header("Core Manager References")]
    public StageInteractManager stageInteractManager;
    public ObjectButtonsInteractables objectButtonsInteractables;
    public DialogManager dialogManager;

    [Header("Interaction Mode")]
    public InteractionType interactionType = InteractionType.TalkOrRead;

    [Header("Base Dialogue")]
    [TextArea] public string[] Dialog;

    [Header("Conditional Dialogue (DialogWithCheck)")]
    public string itemToCheck;
    [TextArea] public string[] secondDialogPass;
    [TextArea] public string[] secondDialogFail;
    [TextArea] public string[] DialogAfterGivingItem;

    [Header("Item Reward / Pickup")]
    public string itemToAdd;

    private ItemManager itemManager;
    private bool itemCheckPassed = false;
    private bool itemAdded = false;

    void Awake()
    {
        BootStrappin bootStrappin = FindFirstObjectByType<BootStrappin>();
        if (bootStrappin != null)
        {
            bootStrappin.FindManagers();
            if (stageInteractManager == null) stageInteractManager = bootStrappin.stageInteractManager;
            if (objectButtonsInteractables == null) objectButtonsInteractables = bootStrappin.objectButtonsInteractables;
            if (dialogManager == null) dialogManager = bootStrappin.dialogManager;
        }
    }

    private void Start()
    {
        itemManager = ItemManager.instace;
        if (stageInteractManager == null) stageInteractManager = FindFirstObjectByType<StageInteractManager>();
        if (objectButtonsInteractables == null) objectButtonsInteractables = FindFirstObjectByType<ObjectButtonsInteractables>();
        if (dialogManager == null) dialogManager = DialogManager.dialogManager;
    }

    public void Interact()
    {
        switch (interactionType)
        {
            case InteractionType.TalkOrRead:
                HandleTalkOrRead();
                break;

            case InteractionType.DialogWithCheck:
                HandleDialogWithCheck();
                break;

            case InteractionType.ItemPickUp:
                HandleItemPickUp();
                break;
        }
    }

    private void HandleTalkOrRead()
    {
        Debug.Log("talked");
        if (dialogManager == null) dialogManager = DialogManager.dialogManager;
        if (dialogManager == null) return;//---- added to prevent nullstate break----CF

        if (!dialogManager.talking)
        {
            dialogManager.StartDialog(Dialog);
        }
        else
        {
            dialogManager.ContinueDialog();
        }
    }

    private void HandleDialogWithCheck()
    {
        Debug.Log("talked");
        if (dialogManager == null) dialogManager = DialogManager.dialogManager;
        if (dialogManager == null) return; //---- added to prevent nullstate break----CF
        if (stageInteractManager == null) stageInteractManager = FindFirstObjectByType<StageInteractManager>();

        bool hasEnough = (stageInteractManager != null)
            ? stageInteractManager.HasEnoughOfItem(itemToCheck)
            : (itemManager != null && itemManager.CheckItem(itemToCheck));

        if (!dialogManager.talking)
        {
            if (itemAdded)
            {
                dialogManager.StartDialog(DialogAfterGivingItem);
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

    private void HandleItemPickUp()
    {
        if (stageInteractManager == null) stageInteractManager = FindFirstObjectByType<StageInteractManager>();

        if (!string.IsNullOrEmpty(itemToAdd))
        {
            if (stageInteractManager != null)
            {
                stageInteractManager.AddCollectedItem(itemToAdd);
            }
            else
            {
                if (itemManager == null) itemManager = ItemManager.instace;//---- added to prevent nullstate break----CF
                if (itemManager != null) itemManager.AddToItem(itemToAdd);
            }
        }
        Destroy(gameObject);
    }

    private void Update()
    {
        if (interactionType == InteractionType.DialogWithCheck && !string.IsNullOrEmpty(itemToAdd))
        {
            if (dialogManager == null) dialogManager = DialogManager.dialogManager;
            if (dialogManager == null) return;

            if (itemCheckPassed && !dialogManager.talking && !itemAdded)
            {
                Debug.Log($"gave {itemToAdd}");
                if (stageInteractManager == null) stageInteractManager = FindFirstObjectByType<StageInteractManager>();

                if (stageInteractManager != null)
                {
                    stageInteractManager.AddCollectedItem(itemToAdd);
                }
                else
                {
                    if (itemManager == null) itemManager = ItemManager.instace; //----reverify fall back to prevent null breaks----CF
                    if (itemManager != null) itemManager.AddToItem(itemToAdd);
                }
                itemAdded = true;
            }
        }
    }
}