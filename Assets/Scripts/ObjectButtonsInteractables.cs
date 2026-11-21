//using JetBrains.Annotations;
using TMPro;
//using Unity.Cinemachine;
//using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
//using UnityEngine.Rendering;
//using UnityEngine.Rendering.Universal;

#region coder & project
/// <summary>
/// NSCC GAME2065/4087/Game Development III(B)/Cameron,Jordan
/// Jam 3 :DragonCrumbs: Jangle's Sweet Journey
/// team: Chris French, Percy Hebb, Kevin Card, Myranda Roy
/// Coder current script: Chris French Second Year NSCC Game Programming 
/// Additions / annotations:
/// code review: 
/// </summary>
#endregion

public class ObjectButtonsInteractables : MonoBehaviour
{

    public MenuManager menuManager;
    public StageInteractManager stageInteractManager;

    [Header("UI Elements")]//---- Defines Game Elements for active et calls----CF
    [SerializeField] public GameObject ReferenePanel;
    [SerializeField] public GameObject OKButton;
    [SerializeField] public GameObject Accept;
    [SerializeField] public GameObject RePick;
    [SerializeField] public GameObject RecipeRefButton;
    [SerializeField] public GameObject CheckMark1;
    [SerializeField] public GameObject CrossOut1;
    [SerializeField] public GameObject CheckMark2;
    [SerializeField] public GameObject CrossOut2;
    [SerializeField] public GameObject CheckMark3;
    [SerializeField] public GameObject CrossOut3;
    [SerializeField] public GameObject QuestPanel;
    //[SerializeField] public GameObject ApplePieRecipeCard;
    //[SerializeField] public GameObject BrowniesRecipeCard;
    //[SerializeField] public GameObject PineNutFudgeRecipeCard;
    //[SerializeField] public GameObject CheeseCakeRecipeCard;
    //[SerializeField] public GameObject CookiesRecipeCard;
    //[SerializeField] public GameObject Instructions;
    [SerializeField] public GameObject DangleExit;

    [Header("Text outputs")]
    public TextMeshProUGUI FirstIngredientCollected;
    public TextMeshProUGUI SecondIngredientCollected;
    public TextMeshProUGUI ThirdIngredientCollected;
    public TextMeshProUGUI FourthIngredientCollected;


    [Header("Apple Pie recipie")]//---- defines quick ref visual objects for apple pie recipe----CF
    [SerializeField] public GameObject IngredientAP1;
    [SerializeField] public GameObject IngredientAP1A;
    [SerializeField] public GameObject IngredientAP1B;
    [SerializeField] public GameObject IngredientAP1C;
    [SerializeField] public GameObject IngredientAP1D;
    [SerializeField] public GameObject IngredientAP1E;
    [SerializeField] public GameObject IngredientAP2;
    [SerializeField] public GameObject IngredientAP2A;
    [SerializeField] public GameObject IngredientAP3;
    [SerializeField] public GameObject IngredientAP3A;
    [SerializeField] public GameObject IngredientMill1;
    [SerializeField] public GameObject IngredientWheat1A;
    [SerializeField] public GameObject IngredientWheat1B;
    [SerializeField] public GameObject IngredientWheat1C;
    [SerializeField] public GameObject IngredientWheat1D;
    [SerializeField] public GameObject IngredientWheat1E;
    [SerializeField] public GameObject BakedApplePieB;

    private string[] RecipeList;


    //[Header ("Additional recipie i there is time ")]//----Defines ingredient visuals for second recipie if there is time to incude one----CF
    //[SerializeField] public GameObject IngredientNA1;
    //[SerializeField] public GameObject IngredientNA2;
    //[SerializeField] public GameObject IngredientNA3;

    public bool isBakery = false;

    public bool isQuest = false;

    public string[] ActiveQuest = { "ApplePie", "Brownies", "PineNutFudge", "Cheesecake", "Cookies" };
    public int currentQuest;


    void Awake()
    {
        BootStrappin bootStrappin = FindFirstObjectByType<BootStrappin>();
        if (bootStrappin != null)
        {
            bootStrappin.FindManagers();
            if (menuManager == null) menuManager = bootStrappin.menuManager;
            if (stageInteractManager == null) stageInteractManager = bootStrappin.stageInteractManager;
        }

        if (menuManager == null)
        {
            menuManager = FindFirstObjectByType<MenuManager>();
        }
        if (stageInteractManager == null)
        {
            stageInteractManager = FindFirstObjectByType<StageInteractManager>();
        }

        //Changed cause the code below throws a couple of random errors depending on load orders ensures first load 
        //BootStrappin bootStrappin = FindFirstObjectByType<BootStrappin>();
        //bootStrappin.FindManagers();

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetDefaultText();
        int currentBuildIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;


        if ((currentBuildIndex >= 1 && currentBuildIndex <= 3) || (MenuManager.Instance != null && MenuManager.Instance.hasActiveQuest))
        {
            if (MenuManager.Instance != null) MenuManager.Instance.hasActiveQuest = true;
            SetQuestState();
            RefreshAllIngredientTexts();
        }
        else
        {
            SetDefaultText();
            SetBakeryState();
        }
        if (DangleExit != null) DangleExit.SetActive(false); // [CODE REVIEW] Null guard
        ////Instructions.SetActive(false);
        //ReferenePanel.SetActive(false);// ---- turns off the reference card when hud begins----CF
        //isQuest = false;
        //isBakery = true;
        //RecipeList = new string[] { "Apple Pie", "Brownies", "Pine Nut Fudge", "Cheesecake", "Cookies" };


    }
    public void SetBakeryState()
    {
        isBakery = true;
        isQuest = false;
        if (ReferenePanel != null)
        {
            ReferenePanel.SetActive(false);
        }
        if (QuestPanel != null)
        {
            QuestPanel.SetActive(false);
        }
    }

    public void SetQuestState()
    {
        isBakery = false;
        isQuest = true;
        if (ReferenePanel != null)
        {
            ReferenePanel.SetActive(false);
        }

        if (QuestPanel != null)
        {
            QuestPanel.SetActive(true);
        }


        SetApple_TXT();
        SetFlour_TXT();
        SetHoney_TXT();
        SetWheat_TXT();
    }

    public void OnRecipeCheck()
    {
        if (ReferenePanel != null) ReferenePanel.SetActive(true);
        if (OKButton != null) OKButton.SetActive(true);
        if (Accept != null) Accept.SetActive(false);
        if (RePick != null) RePick.SetActive(false);

    }
    public void OnRecipeOK()
    {
        if (ReferenePanel != null) ReferenePanel.SetActive(false);

    }

    public void OnRecipeConfirm()
    {
        if (menuManager == null) menuManager = FindFirstObjectByType<MenuManager>();
        if (menuManager != null) menuManager.hasActiveQuest = true;
        if (ReferenePanel != null) ReferenePanel.SetActive(false);
        if (menuManager != null) menuManager.onTravel();
    }
    public void OnRecipeRePick()
    {
        if (ReferenePanel != null) ReferenePanel.SetActive(false);

    }

    public void OnRecipeSelectApplePie()
    {
        if (ReferenePanel != null) ReferenePanel.SetActive(true);
        if (OKButton != null) OKButton.SetActive(false);
        if (Accept != null) Accept.SetActive(true);
        if (RePick != null) RePick.SetActive(true);
        //if (RefCheckMark1 != null) RefCheckMark1.SetActive(false);
        //if (RefCrossOut1 != null) RefCrossOut1.SetActive(false);
        //if (RefCheckMark2 != null) RefCheckMark2.SetActive(false);
        //if (RefCrossOut2 != null) RefCrossOut2.SetActive(false);
        //if (RefCheckMark3 != null) RefCheckMark3.SetActive(false);
        //if (RefCrossOut3 != null) RefCrossOut3.SetActive(false);


    }
    public void OnRecipeSelectBrownies()
    {
        if (ReferenePanel != null) ReferenePanel.SetActive(true);
        if (OKButton != null) OKButton.SetActive(false);
        if (Accept != null) Accept.SetActive(true);
        if (RePick != null) RePick.SetActive(true);


    }
    public void OnRecipeSelectPineNutFudge()
    {
        if (ReferenePanel != null) ReferenePanel.SetActive(true);
        if (OKButton != null) OKButton.SetActive(false);
        if (Accept != null) Accept.SetActive(true);
        if (RePick != null) RePick.SetActive(true);


    }
    public void OnRecipeSelectCheesecake()
    {
        if (ReferenePanel != null) ReferenePanel.SetActive(true);
        if (OKButton != null) OKButton.SetActive(false);
        if (Accept != null) Accept.SetActive(true);
        if (RePick != null) RePick.SetActive(true);


    }
    public void OnRecipeSelectCookies()
    {
        if (ReferenePanel != null) ReferenePanel.SetActive(true);
        if (OKButton != null) OKButton.SetActive(false);
        if (Accept != null) Accept.SetActive(true);
        if (RePick != null) RePick.SetActive(true);


    }

    public void OnRecipeSelect()
    {
        if (ReferenePanel != null) ReferenePanel.SetActive(true);
        if (OKButton != null) OKButton.SetActive(false);
        if (Accept != null) Accept.SetActive(true);
        if (RePick != null) RePick.SetActive(true);

    }

    public void OnApplePieStart()
    {
        if (menuManager == null) menuManager = FindFirstObjectByType<MenuManager>();
        if (menuManager != null) menuManager.hasActiveQuest = true;
        ActiveQuest[0] = "ApplePie";
        currentQuest = 0;
        if (ItemManager.instace != null) ItemManager.instace.LoadRecipe(ItemManager.Recpie.one);
        SetQuestState();
        ResetRecipeQuestCard();
        GoToTravelMap();

    }
    public void RefreshAllIngredientTexts()
    {
        SetApple_TXT();
        SetFlour_TXT();
        SetHoney_TXT();
        SetWheat_TXT();
    }
    private void GoToTravelMap()
    {
        if (menuManager == null) menuManager = FindFirstObjectByType<MenuManager>();
        if (menuManager != null) menuManager.onTravel();
        else  Debug.LogError("MenuManager could not be found in the scene!");
    }
    public void SetDefaultText()
    {
        if (FirstIngredientCollected != null) FirstIngredientCollected.text = "";
        if (SecondIngredientCollected != null) SecondIngredientCollected.text = "";
        if (ThirdIngredientCollected != null) ThirdIngredientCollected.text = "";
        if (FourthIngredientCollected != null) FourthIngredientCollected.text = "";
    }
    public void SetWheat_TXT()
    {
        if (stageInteractManager == null) stageInteractManager = FindFirstObjectByType<StageInteractManager>();
        if (FourthIngredientCollected != null && stageInteractManager != null)
            FourthIngredientCollected.text = "Sub ingredient Wheat(to mill Flour)" + stageInteractManager.WheatCount.ToString() + "/5";
    }

    public void SetApple_TXT()
    {
        if (stageInteractManager == null) stageInteractManager = FindFirstObjectByType<StageInteractManager>();
        if (FirstIngredientCollected != null && stageInteractManager != null)
            FirstIngredientCollected.text = stageInteractManager.AppleCount.ToString() + "/5";
    }

    public void SetFlour_TXT()
    {
        if (stageInteractManager == null) stageInteractManager = FindFirstObjectByType<StageInteractManager>();
        if (SecondIngredientCollected != null && stageInteractManager != null)
            SecondIngredientCollected.text = stageInteractManager.FlourCount.ToString() + "/1";
    }

    public void SetHoney_TXT()
    {
        if (stageInteractManager == null) stageInteractManager = FindFirstObjectByType<StageInteractManager>();
        if (ThirdIngredientCollected != null && stageInteractManager != null)
            ThirdIngredientCollected.text = stageInteractManager.HoneyCount.ToString() + "/1";
    }
    public void ResetRecipeQuestCard()
    {

        if (BakedApplePieB !=null) BakedApplePieB.SetActive(false);
        if (IngredientAP1 != null) IngredientAP1.SetActive(true);
        if (IngredientAP1A != null) IngredientAP1A.SetActive(false);
        if (IngredientAP1B != null) IngredientAP1B.SetActive(false);
        if (IngredientAP1C != null) IngredientAP1C.SetActive(false);
        if (IngredientAP1D != null) IngredientAP1D.SetActive(false);
        if (IngredientAP1E != null) IngredientAP1E.SetActive(false);
        if (IngredientAP2 != null) IngredientAP2.SetActive(true);
        if (IngredientAP2A != null) IngredientAP2A.SetActive(false);
        if (IngredientAP3 != null) IngredientAP3.SetActive(true);
        if (IngredientAP3A != null) IngredientAP3A.SetActive(false);
        if (IngredientMill1 != null) IngredientMill1.SetActive(false);
        if (IngredientWheat1A != null) IngredientWheat1A.SetActive(false);
        if (IngredientWheat1B != null) IngredientWheat1B.SetActive(false);
        if (IngredientWheat1C != null) IngredientWheat1C.SetActive(false);
        if (IngredientWheat1D != null) IngredientWheat1D.SetActive(false);
        if (IngredientWheat1E != null) IngredientWheat1E.SetActive(false);
        //if (RefCheckMark1 != null) RefCheckMark1.SetActive(false);
        //if (RefCrossOut1 != null) RefCrossOut1.SetActive(false);
        //if (RefCheckMark2 != null) RefCheckMark2.SetActive(false);
        //if (RefCrossOut2 != null) RefCrossOut2.SetActive(false);
        //if (RefCheckMark3 != null) RefCheckMark3.SetActive(false);
        //if (RefCrossOut3 != null) RefCrossOut3.SetActive(false);
    }

    public void UpdateQuestCard()
    {
        if (BakedApplePieB != null) if (stageInteractManager.FreshBakedPie <= 1) BakedApplePieB.SetActive(true);
        if (IngredientAP1A != null) if (stageInteractManager.AppleCount <= 1) IngredientAP1A.SetActive(true);
        if (IngredientAP1B != null) if (stageInteractManager.AppleCount <= 2) IngredientAP1B.SetActive(true);
        if (IngredientAP1C != null) if (stageInteractManager.AppleCount <= 3) IngredientAP1C.SetActive(true);
        if (IngredientAP1D != null) if (stageInteractManager.AppleCount <= 4) IngredientAP1D.SetActive(true);
        if (IngredientAP1E != null) if (stageInteractManager.AppleCount <= 5) IngredientAP1E.SetActive(true);
      
        if (IngredientAP2A != null) if (stageInteractManager.FlourCount <= 1) IngredientAP2A.SetActive(true);
       
        if (IngredientAP3A != null) if (stageInteractManager.HoneyCount <= 5) IngredientAP3A.SetActive(true);

        if (IngredientMill1 != null) if (stageInteractManager.WheatCount <= 5) IngredientMill1.SetActive(true);
        if (IngredientWheat1A != null) if (stageInteractManager.WheatCount <= 1) IngredientWheat1A.SetActive(true);
        if (IngredientWheat1B != null) if (stageInteractManager.WheatCount <= 2) IngredientWheat1B.SetActive(true);
        if (IngredientWheat1C != null) if (stageInteractManager.WheatCount <= 3) IngredientWheat1C.SetActive(true);
        if (IngredientWheat1D != null) if (stageInteractManager.WheatCount <= 4) IngredientWheat1D.SetActive(true);
        if (IngredientWheat1E != null) if (stageInteractManager.WheatCount <= 5) IngredientWheat1E.SetActive(true);
        //if (RefCheckMark1 != null) if () RefCheckMark1.SetActive(false);
        //if (RefCrossOut1 != null) if () RefCrossOut1.SetActive(false);
        //if (RefCheckMark2 != null) if () RefCheckMark2.SetActive(false);
        //if (RefCrossOut2 != null) if () RefCrossOut2.SetActive(false);
        //if (RefCheckMark3 != null) if () RefCheckMark3.SetActive(false);
        //if (RefCrossOut3 != null) if (l) RefCrossOut3.SetActive(false);
    }






}




































//        using JetBrains.Annotations;
//using TMPro;
//using Unity.Cinemachine;
//using Unity.VisualScripting.Antlr3.Runtime.Tree;
//using UnityEngine;
//using UnityEngine.Rendering;
//using UnityEngine.Rendering.Universal;
//using UnityEngine.UIElements;
//#region coder & project
///// <summary>
///// NSCC GAME2065/4087/Game Development III(B)/Cameron,Jordan
///// Jam 3 :DragonCrumbs: Jangle's Sweet Journey
///// team: Chris French, Percy Hebb, Kevin Card, Myranda Roy
///// Coder current script: Chris French Second Year NSCC Game Programming 
///// Additions / annotations:
///// code review: 
///// </summary>
//#endregion

//public class ObjectButtonsInteractables : MonoBehaviour
//{    

//     public MenuManager menuManager;
//    public StageInteractManager stageInteractManager;

//    [Header("UI Elements")]//---- Defines Game Elements for active et calls----CF
//    [SerializeField] public GameObject ReferenePanel;
//    [SerializeField] public GameObject OKButton;
//    [SerializeField] public GameObject Accept;
//    [SerializeField] public GameObject RePick;
//    [SerializeField] public GameObject RecipeRefButton;
//    [SerializeField] public GameObject CheckMark1;
//    [SerializeField] public GameObject CrossOut1;
//    [SerializeField] public GameObject CheckMark2;
//    [SerializeField] public GameObject CrossOut2;
//    [SerializeField] public GameObject CheckMark3;
//    [SerializeField] public GameObject CrossOut3;
//    [SerializeField] public GameObject QuestPanel;
//    //[SerializeField] public GameObject ApplePieRecipeCard;
//    //[SerializeField] public GameObject BrowniesRecipeCard;
//    //[SerializeField] public GameObject PineNutFudgeRecipeCard;
//    //[SerializeField] public GameObject CheeseCakeRecipeCard;
//    //[SerializeField] public GameObject CookiesRecipeCard;
//    //[SerializeField] public GameObject Instructions;
//    [SerializeField] public GameObject DangleExit;

//    [Header("Text outputs")]
//    public TextMeshProUGUI FirstIngredientCollected;
//    public TextMeshProUGUI SecondIngredientCollected;
//    public TextMeshProUGUI ThirdIngredientCollected;
//    public TextMeshProUGUI FourthIngredientCollected;


//   [Header ("Apple Pie recipie")]//---- defines quick ref visual objects for apple pie recipe----CF
//    [SerializeField] public GameObject IngredientAP1;
//    [SerializeField] public GameObject IngredientAP1A;
//    [SerializeField] public GameObject IngredientAP1B;
//    [SerializeField] public GameObject IngredientAP1C;
//    [SerializeField] public GameObject IngredientAP1D;
//    [SerializeField] public GameObject IngredientAP1E;
//    [SerializeField] public GameObject IngredientAP2;
//    [SerializeField] public GameObject IngredientAP2A;
//    [SerializeField] public GameObject IngredientAP3;
//    [SerializeField] public GameObject IngredientAP3A;
//    [SerializeField] public GameObject IngredientMill1;
//    [SerializeField] public GameObject IngredientWheat1A;
//    [SerializeField] public GameObject IngredientWheat1B;
//    [SerializeField] public GameObject IngredientWheat1C;
//    [SerializeField] public GameObject IngredientWheat1D;
//    [SerializeField] public GameObject IngredientWheat1E;
//    [SerializeField] public GameObject RefCheckMark1;
//    [SerializeField] public GameObject RefCrossOut1;
//    [SerializeField] public GameObject RefCheckMark2;
//    [SerializeField] public GameObject RefCrossOut2;
//    [SerializeField] public GameObject RefCheckMark3;
//    [SerializeField] public GameObject RefCrossOut3;

//    private string[] RecipeList;


//    //[Header ("Additional recipie i there is time ")]//----Defines ingredient visuals for second recipie if there is time to incude one----CF
//    //[SerializeField] public GameObject IngredientNA1;
//    //[SerializeField] public GameObject IngredientNA2;
//    //[SerializeField] public GameObject IngredientNA3;

//    public bool isBakery = false;

//    public bool isQuest = false;

//    public string[] ActiveQuest = { "ApplePie", "Brownies", "PineNutFudge", "Cheesecake", "Cookies" };
//    public int currentQuest;


//    void Awake()
//    {
//        BootStrappin bootStrappin = FindFirstObjectByType<BootStrappin>();
//        if (bootStrappin != null)
//        {
//            bootStrappin.FindManagers();
//            if (menuManager == null) menuManager = bootStrappin.menuManager;
//            if (stageInteractManager == null) stageInteractManager = bootStrappin.stageInteractManager;
//        }

//        if (menuManager == null)
//        { 
//            menuManager = FindFirstObjectByType<MenuManager>(); 
//        }
//        if (stageInteractManager == null)
//        { 
//            stageInteractManager = FindFirstObjectByType<StageInteractManager>();
//        }

//        //Changed cause the code below throws a couple of random errors depending on load orders ensures first load 
//        //BootStrappin bootStrappin = FindFirstObjectByType<BootStrappin>();
//        //bootStrappin.FindManagers();

//    }
//    // Start is called once before the first execution of Update after the MonoBehaviour is created
//    void Start()
//    {
//        SetDefaultText();
//        if (MenuManager.Instance != null && MenuManager.Instance.hasActiveQuest)
//        {
//            SetQuestState();
//            RefreshAllIngredientTexts();
//        }
//        else
//        {
//            SetDefaultText();
//            SetBakeryState();
//        }
//        DangleExit.SetActive(false);
//        ////Instructions.SetActive(false);
//        //ReferenePanel.SetActive(false);// ---- turns off the reference card when hud begins----CF
//        //isQuest = false;
//        //isBakery = true;
//        //RecipeList = new string[] { "Apple Pie", "Brownies", "Pine Nut Fudge", "Cheesecake", "Cookies" };


//    }
//    public void SetBakeryState()
//    {
//        isBakery = true;
//        isQuest = false;
//        if (ReferenePanel != null)
//        {
//            ReferenePanel.SetActive(false);
//        }
//        if (QuestPanel != null)
//        {
//            QuestPanel.SetActive(false);
//        }
//    }

//    public void SetQuestState()
//    {
//        isBakery = false;
//        isQuest = true;
//        if (ReferenePanel != null)
//        {
//            ReferenePanel.SetActive(false);
//        }

//        if (QuestPanel != null)
//        {
//            QuestPanel.SetActive(true);
//        }


//        SetApple_TXT();
//        SetFlour_TXT();
//        SetHoney_TXT();
//        SetWheat_TXT();
//    }

//    public void OnRecipeCheck()
//    {
//    ReferenePanel.SetActive(true);    
//    OKButton.SetActive(true);
//    Accept.SetActive(false);
//    RePick.SetActive(false);

//    }
//    public void OnRecipeOK()
//    {
//        ReferenePanel.SetActive(false);

//    }

//    public void OnRecipeConfirm()
//    {
//        MenuManager.Instance.hasActiveQuest = true;
//        //SetQuestState();
//        ReferenePanel.SetActive(false);
//        menuManager.onTravel();
//        ///set the info on the  corner recipe card and turn on the quest panel
//        //isQuest = true;
//        //isBakery = false;
//    }
//    public void OnRecipeRePick()
//    {
//        ReferenePanel.SetActive(false);

//    }

//    public void OnRecipeSelectApplePie()
//    {
//        ReferenePanel.SetActive(true);
//        OKButton.SetActive(false);
//        Accept.SetActive(true);
//        RePick.SetActive(true);
//        RefCheckMark1.SetActive(false);
//        RefCrossOut1.SetActive(false);
//        RefCheckMark2.SetActive(false);
//        RefCrossOut2.SetActive(false);
//        RefCheckMark3.SetActive(false);
//        RefCrossOut3.SetActive(false);


//    }
//    public void OnRecipeSelectBrownies()
//    {
//        ReferenePanel.SetActive(true);
//        OKButton.SetActive(false);
//        Accept.SetActive(true);
//        RePick.SetActive(true);


//    }
//    public void OnRecipeSelectPineNutFudge()
//    {
//        ReferenePanel.SetActive(true);
//        OKButton.SetActive(false);
//        Accept.SetActive(true);
//        RePick.SetActive(true);


//    }
//    public void OnRecipeSelectCheesecake()
//    {
//        ReferenePanel.SetActive(true);
//        OKButton.SetActive(false);
//        Accept.SetActive(true);
//        RePick.SetActive(true);


//    }
//    public void OnRecipeSelectCookies()
//    {
//        ReferenePanel.SetActive(true);
//        OKButton.SetActive(false);
//        Accept.SetActive(true);
//        RePick.SetActive(true);


//    }




//    public void OnRecipeSelect()
//    {
//        ReferenePanel.SetActive(true);
//        OKButton.SetActive(false);
//        Accept.SetActive(true);
//        RePick.SetActive(true);

//    }

//    public void OnApplePieStart()
//    {
//        if (menuManager == null) menuManager = FindFirstObjectByType<MenuManager>();
//        if (menuManager != null) menuManager.hasActiveQuest = true; 
//        ActiveQuest[0] = "ApplePie";
//        currentQuest = 0;
//        if (ItemManager.instace != null) ItemManager.instace.LoadRecipe(ItemManager.Recpie.one);
//        SetQuestState();
//        ResetRecipeQuestCard();
//        GoToTravelMap();

//    }
//    public void RefreshAllIngredientTexts()
//    {
//        SetApple_TXT();
//        SetFlour_TXT();
//        SetHoney_TXT();
//        SetWheat_TXT();
//    }
//    private void GoToTravelMap()
//    {
//        if (menuManager == null)
//            menuManager = FindFirstObjectByType<MenuManager>();

//        if (menuManager != null)
//            menuManager.onTravel();
//        else
//            Debug.LogError("MenuManager could not be found in the scene!");
//    }


//    public void SetDefaultText()
//    {
//        FirstIngredientCollected.text = "";
//        SecondIngredientCollected.text = "";
//        ThirdIngredientCollected.text = "";
//        FourthIngredientCollected.text = "";
//    }

//    public void SetWheat_TXT()
//    {

//        if (stageInteractManager == null) stageInteractManager = FindFirstObjectByType<StageInteractManager>();
//        if (FourthIngredientCollected != null && stageInteractManager != null)
//            FourthIngredientCollected.text = "Sub ingredient Wheat(to mill Flour)" + stageInteractManager.WheatCount.ToString() + "/5";
//    }

//    public void SetApple_TXT()
//    {
//        if (stageInteractManager == null) stageInteractManager = FindFirstObjectByType<StageInteractManager>();
//        if (FirstIngredientCollected != null && stageInteractManager != null)
//            FirstIngredientCollected.text = stageInteractManager.AppleCount.ToString() + "/5";
//    }

//    public void SetFlour_TXT()
//    {
//        if (stageInteractManager == null) stageInteractManager = FindFirstObjectByType<StageInteractManager>();
//        if (SecondIngredientCollected != null && stageInteractManager != null)
//            SecondIngredientCollected.text = stageInteractManager.FlourCount.ToString() + "/1";
//    }

//    public void SetHoney_TXT()
//    {
//        if (stageInteractManager == null) stageInteractManager = FindFirstObjectByType<StageInteractManager>();
//        if (ThirdIngredientCollected != null && stageInteractManager != null)
//            ThirdIngredientCollected.text = stageInteractManager.HoneyCount.ToString() + "/1";
//    }

//    public void ResetRecipeQuestCard()
//    {
//        IngredientAP1.SetActive(false);
//        IngredientAP1A.SetActive(false);
//        IngredientAP1B.SetActive(false);
//        IngredientAP1C.SetActive(false);
//        IngredientAP1D.SetActive(false);
//        IngredientAP1E.SetActive(false);
//        IngredientAP2.SetActive(false);
//        IngredientAP2A.SetActive(false);
//        IngredientAP3.SetActive(false);
//        IngredientAP3A.SetActive(false);
//        IngredientMill1.SetActive(false);
//        IngredientWheat1A.SetActive(false);
//        IngredientWheat1B.SetActive(false);
//        IngredientWheat1C.SetActive(false);
//        IngredientWheat1D.SetActive(false);
//        IngredientWheat1E.SetActive(false);
//        RefCheckMark1.SetActive(false);
//        RefCrossOut1.SetActive(false);
//        RefCheckMark2.SetActive(false);
//        RefCrossOut2.SetActive(false);
//        RefCheckMark3.SetActive(false);
//        RefCrossOut3.SetActive(false);
//    }




//}
