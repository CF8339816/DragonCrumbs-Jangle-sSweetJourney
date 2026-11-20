using JetBrains.Annotations;
using TMPro;
using Unity.Cinemachine;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;
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


   [Header ("Apple Pie recipie")]//---- defines quick ref visual objects for apple pie recipe----CF
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
    [SerializeField] public GameObject RefCheckMark1;
    [SerializeField] public GameObject RefCrossOut1;
    [SerializeField] public GameObject RefCheckMark2;
    [SerializeField] public GameObject RefCrossOut2;
    [SerializeField] public GameObject RefCheckMark3;
    [SerializeField] public GameObject RefCrossOut3;

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
        }
        //Changed cause the code below throws a couple of random errors depending on load orders ensures first load 
        //BootStrappin bootStrappin = FindFirstObjectByType<BootStrappin>();
        //bootStrappin.FindManagers();
        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetBakeryState();
        DangleExit.SetActive(false);
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

    }
  

    public void OnRecipeCheck()
    {
    ReferenePanel.SetActive(true);    
    OKButton.SetActive(true);
    Accept.SetActive(false);
    RePick.SetActive(false);

    }
    public void OnRecipeOK()
    {
        ReferenePanel.SetActive(false);

    }

    public void OnRecipeConfirm()
    {
        SetQuestState();
        ReferenePanel.SetActive(false);
        menuManager.onTravel();
        ///set the info on the  corner recipe card and turn on the quest panel
        isQuest = true;
        isBakery = false;
    }
    public void OnRecipeRePick()
    {
        ReferenePanel.SetActive(false);

    }

    public void OnRecipeSelectApplePie()
    {
        ReferenePanel.SetActive(true);
        OKButton.SetActive(false);
        Accept.SetActive(true);
        RePick.SetActive(true);
        RefCheckMark1.SetActive(false);
        RefCrossOut1.SetActive(false);
        RefCheckMark2.SetActive(false);
        RefCrossOut2.SetActive(false);
        RefCheckMark3.SetActive(false);
        RefCrossOut3.SetActive(false);


    }
    public void OnRecipeSelectBrownies()
    {
        ReferenePanel.SetActive(true);
        OKButton.SetActive(false);
        Accept.SetActive(true);
        RePick.SetActive(true);
    
      
    }
    public void OnRecipeSelectPineNutFudge()
    {
        ReferenePanel.SetActive(true);
        OKButton.SetActive(false);
        Accept.SetActive(true);
        RePick.SetActive(true);
   
       
    }
    public void OnRecipeSelectCheesecake()
    {
        ReferenePanel.SetActive(true);
        OKButton.SetActive(false);
        Accept.SetActive(true);
        RePick.SetActive(true);
      
       
    }
    public void OnRecipeSelectCookies()
    {
        ReferenePanel.SetActive(true);
        OKButton.SetActive(false);
        Accept.SetActive(true);
        RePick.SetActive(true);
     
       
    }
   



    public void OnRecipeSelect()
    {
        ReferenePanel.SetActive(true);
        OKButton.SetActive(false);
        Accept.SetActive(true);
        RePick.SetActive(true);

    }

    public void OnApplePieStart()
    {
        GoToTravelMap();
        SetQuestState();
        ActiveQuest[0] = "ApplePie";
        currentQuest= 0;
        ResetRecipeQuestCard();


    }

    private void GoToTravelMap()
    {
        if (menuManager == null)
            menuManager = FindFirstObjectByType<MenuManager>();

        if (menuManager != null)
            menuManager.onTravel();
        else
            Debug.LogError("MenuManager could not be found in the scene!");
    }




    public void SetWheat_TXT()
    {
       FourthIngredientCollected.text = "Sub ingredient Wheat(to mill Flour)" + stageInteractManager.WheatCount.ToString()+"/5" ;
    }
    public void SetApple_TXT()
    {
        FirstIngredientCollected.text =  stageInteractManager.AppleCount.ToString() + "/5";
    }

    public void SetFlour_TXT()
    {
        SecondIngredientCollected.text =  stageInteractManager.FlourCount.ToString() + "/1";
    }

    public void SetHoney_TXT()
    {
        ThirdIngredientCollected.text =  stageInteractManager.HoneyCount.ToString() + "/1";
    }

    public void ResetRecipeQuestCard()
    {
        IngredientAP1.SetActive(false);
        IngredientAP1A.SetActive(false);
        IngredientAP1B.SetActive(false);
        IngredientAP1C.SetActive(false);
        IngredientAP1D.SetActive(false);
        IngredientAP1E.SetActive(false);
        IngredientAP2.SetActive(false);
        IngredientAP2A.SetActive(false);
        IngredientAP3.SetActive(false);
        IngredientAP3A.SetActive(false);
        IngredientMill1.SetActive(false);
        IngredientWheat1A.SetActive(false);
        IngredientWheat1B.SetActive(false);
        IngredientWheat1C.SetActive(false);
        IngredientWheat1D.SetActive(false);
        IngredientWheat1E.SetActive(false);
        RefCheckMark1.SetActive(false);
        RefCrossOut1.SetActive(false);
        RefCheckMark2.SetActive(false);
        RefCrossOut2.SetActive(false);
        RefCheckMark3.SetActive(false);
        RefCrossOut3.SetActive(false);
    }




}
