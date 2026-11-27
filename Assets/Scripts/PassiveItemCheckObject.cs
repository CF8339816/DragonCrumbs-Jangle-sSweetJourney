using UnityEngine;
/// <summary>
/// Kevin's scripts combined into one manager for  streamlining.---CF
/// I have had to rework the entire code block to convert the variable references and 
/// naming conventions to match the larger block of code as treying to bridge the two 
/// blocks did not work ..... A.K.A. "The Code of Theseus"
/// </summary>
/// 

public class PassiveItemCheckObject : MonoBehaviour, IInteractable
{
    public StageInteractManager stageInteractManager;
    public MenuManager menuManager;
    public DialogManager dialogManager;
    public string itemToCheck;
    public float timer = 5;
    private bool triggered = false;// ---- checkto see if the level change is triggered----CF
    public int nextSceneIndex; // ----Default back to Bakery----CF

    [Header("Dialogue Lines")]
    [TextArea] public string[] dialogPass;
    [TextArea] public string[] dialogFail;

    private bool quotaDialogStarted = false;
    

    void Awake()
    {
        BootStrappin bootStrappin = FindFirstObjectByType<BootStrappin>();
        if (bootStrappin != null)
        {
            bootStrappin.FindManagers();
            if (stageInteractManager == null) stageInteractManager = bootStrappin.stageInteractManager;
            if (menuManager == null) menuManager = bootStrappin.menuManager;
        }
    }
    public void Interact()
    {
        if (triggered) return;
        if (dialogManager == null) dialogManager = DialogManager.dialogManager;
        if (dialogManager == null) return;

        // If dialogue is already open, advance to the next line
        if (dialogManager.talking)
        {
            dialogManager.ContinueDialog();
            return;
        }

        if (stageInteractManager == null) stageInteractManager = FindFirstObjectByType<StageInteractManager>();

        bool quotaReached = (stageInteractManager != null)
            ? stageInteractManager.HasEnoughOfItem(itemToCheck)
            : (ItemManager.instace != null && ItemManager.instace.CheckItem(itemToCheck));

        if (quotaReached)
        {
            quotaDialogStarted = true;
            if (dialogPass != null && dialogPass.Length > 0)
            {
                dialogManager.StartDialog(dialogPass);
            }
            else
            {
                LoadNextScene(); // Fallback if no pass dialogue lines were entered
            }
        }
        else
        {
            if (dialogFail != null && dialogFail.Length > 0)
            {
                dialogManager.StartDialog(dialogFail);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (triggered) return;//---- added to ensure load level by index iss not called every frame once timer runs ou t----CF
        if (dialogManager == null) dialogManager = DialogManager.dialogManager;
        if (quotaDialogStarted && dialogManager != null && !dialogManager.talking)
        {
            LoadNextScene();
        }



    }


    private void LoadNextScene()
    {
        triggered = true;// ---- checkto see if the level change is triggered----CF
        if (menuManager == null) menuManager = MenuManager.Instance;
        if (menuManager != null)// ----enssures menumanager is present for call to level change----CF
        {

            if (nextSceneIndex == menuManager.BAKERYX)//---- call check to ensure Dangle begin object not loaded on reload of bakery----CF
            {
                menuManager.hasActiveQuest = true;
                menuManager.onBakery();
            }
            else// ----Calls next scene----CF
            {
                menuManager.LoadLevelByIndex(nextSceneIndex); 
            }
            
        }
    }

}