using UnityEngine;
/// <summary>
/// Kevin's scripts combined into one manager for  streamlining.---CF
/// 
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
        }
        //Changed cause the code below throws a couple of random errors depending on load orders ensures first load 
        //BootStrappin bootStrappin = FindFirstObjectByType<BootStrappin>(); //----Added to ensure the managerts are assigned---CF
        //bootStrappin.FindManagers();//----Added to ensure the managerts are assigned---CF

    }
    private void Start()
    {
        itemManager = ItemManager.instace;
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

        if (DialogManager.dialogManager == null) return;//---- added to prevent nullstate break----CF
       
        if (!DialogManager.dialogManager.talking)
        {
            DialogManager.dialogManager.StartDialog(Dialog);
        }
        else
        {
            DialogManager.dialogManager.ContinueDialog();
        }
    }

    private void HandleDialogWithCheck()
    {
        Debug.Log("talked");
        if (DialogManager.dialogManager == null || itemManager == null) return; //---- added to prevent nullstate break----CF
        
        if (!DialogManager.dialogManager.talking)
        {
            if (itemAdded)
            {
                DialogManager.dialogManager.StartDialog(DialogAfterGivingItem);
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

    private void HandleItemPickUp()
    {

        if (itemManager == null) itemManager = ItemManager.instace;//---- added to prevent nullstate break----CF

        if (!string.IsNullOrEmpty(itemToAdd))
        {
            itemManager.AddToItem(itemToAdd);
        }
        Destroy(gameObject);
    }

    private void Update()
    {
        if (interactionType == InteractionType.DialogWithCheck && !string.IsNullOrEmpty(itemToAdd))
        {
            if (itemManager == null) itemManager = ItemManager.instace; //----reverify fall back to prevent null breaks----CF

            if (itemCheckPassed && !DialogManager.dialogManager.talking && !itemAdded)
            {
                Debug.Log($"gave {itemToAdd}");
                itemManager.AddToItem(itemToAdd);
                itemAdded = true;
            }
        }
    }
}