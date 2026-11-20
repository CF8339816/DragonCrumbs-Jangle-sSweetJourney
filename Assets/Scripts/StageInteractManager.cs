using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

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


public class StageInteractManager : MonoBehaviour, IInteractable
{ 
      [Header("References")]
    public Transform firstPersonCam;
    public ObjectButtonsInteractables objectButtonsInteractables;
    public ObjGOInteractionManager objGOInteractionManager;
    public MenuManager menuManager;
    public PieVisabilityManager pieVisabilityManager;
    public TravelMapManager travelMapManager;

    public JangleController playerScript;
    public LayerMask RecipeButtons;
    public LayerMask EnvironmentButtons;
    public LayerMask combinedMask;
    private CharacterController characterController;

    private string currentTargetTag = "";

    [Header("Settings")]
    public float checkRange = 100f;//longer range for grapple and pull
    public float switchRange = 4f; // shorter range for button and switch

    //[Header("Interactables")]
    //public GameObject ProxyOvenRawPie;
    //public GameObject ProxyBakedPie;
    //public GameObject RawApplePieProxy;
    //public GameObject PrepBlock;
    //public GameObject Dangle;
    //public GameObject MillstoneClickable;
    //public GameObject Wheat;
    //public GameObject Sythe;

    [Header("Collection Variables")]
    public int WheatCount = 0;
    public int AppleCount = 0;
    public int FlourCount = 0;
    public int HoneyCount = 0;
    public int FreshBakedPie = 0;


    void Awake()
    {
        BootStrappin bootStrappin = FindFirstObjectByType<BootStrappin>();
        if (bootStrappin != null)
        {
            bootStrappin.FindManagers();
        }
        //Changed cause the code below throws a couple of random errors depending on load orders ensures first load 
        //BootStrappin bootStrappin = FindFirstObjectByType< BootStrappin > ();
        //bootStrappin.FindManagers();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //if (SceneManager.GetActiveScene().buildIndex >= 1 && SceneManager.GetActiveScene().buildIndex <= 3)
        //{
        //    objectButtonsInteractables.SetQuestState();
        //}
        //else if (SceneManager.GetActiveScene().buildIndex == 4 )
        //{
        //    objectButtonsInteractables.SetBakeryState();
        //}

        ////combinedMask = RecipeButtons | EnvironmentButtons;
        //if (objectButtonsInteractables == null)
        //{
        //    objectButtonsInteractables = FindFirstObjectByType<ObjectButtonsInteractables>();
        //}
        //if (objGOInteractionManager == null)
        //{
        //    objGOInteractionManager = FindFirstObjectByType<ObjGOInteractionManager>();
        //}

      
        if (objectButtonsInteractables == null)
        {
            objectButtonsInteractables = FindFirstObjectByType<ObjectButtonsInteractables>();
        }
        if (objGOInteractionManager == null)
        {
            objGOInteractionManager = FindFirstObjectByType<ObjGOInteractionManager>();
        }
        if (pieVisabilityManager == null)
        {
            pieVisabilityManager = FindFirstObjectByType<PieVisabilityManager>();
        }
        if (menuManager == null)
        {
            menuManager = FindFirstObjectByType<MenuManager>();
        }

        if (objectButtonsInteractables != null)
        {
            if (SceneManager.GetActiveScene().buildIndex >= 1 && SceneManager.GetActiveScene().buildIndex <= 3)
            {
                objectButtonsInteractables.SetQuestState();
            }
            else if (SceneManager.GetActiveScene().buildIndex == 4)
            {
                objectButtonsInteractables.SetBakeryState();
            }
        }

        DisableWheatCollider();
    }

    void Update()
    {

        UpdateCheckBoxes();


    }

    public void UpdateCheckBoxes()
    {
        if (objectButtonsInteractables == null) return;
        if (AppleCount >=5)
        {
            objectButtonsInteractables.CheckMark1.SetActive(true);
            objectButtonsInteractables.CrossOut1.SetActive(false);
        }
        else
        {
            objectButtonsInteractables.CheckMark1.SetActive(false);
            objectButtonsInteractables.CrossOut1.SetActive(true);
        }
        if (FlourCount >= 1)
        {
            objectButtonsInteractables.CheckMark2.SetActive(true);
            objectButtonsInteractables.CrossOut2.SetActive(false);
        }
        else
        {
            objectButtonsInteractables.CheckMark2.SetActive(false);
            objectButtonsInteractables.CrossOut2.SetActive(true);
        }
        if (HoneyCount >= 1)
        {
            objectButtonsInteractables.CheckMark3.SetActive(true);
            objectButtonsInteractables.CrossOut3.SetActive(false);
        }
        else
        {
            objectButtonsInteractables.CheckMark3.SetActive(false);
            objectButtonsInteractables.CrossOut3.SetActive(true);
        }

        if (AppleCount >=1) objectButtonsInteractables.IngredientAP1A.SetActive(true);
        else if (AppleCount >= 2) objectButtonsInteractables.IngredientAP1B.SetActive(true);
        else if (AppleCount >= 3) objectButtonsInteractables.IngredientAP1C.SetActive(true);
        else if (AppleCount >= 4) objectButtonsInteractables.IngredientAP1D.SetActive(true);
        else if (AppleCount >= 5) objectButtonsInteractables.IngredientAP1E.SetActive(true);

        if (FlourCount >= 1) objectButtonsInteractables.IngredientAP2A.SetActive(true);
       
        if (HoneyCount >= 1) objectButtonsInteractables.IngredientAP3A.SetActive(true);
              
        if (WheatCount >= 1) objectButtonsInteractables.IngredientWheat1A.SetActive(true);
        else if (WheatCount >= 2) objectButtonsInteractables.IngredientWheat1B.SetActive(true);
        else if (WheatCount >= 3) objectButtonsInteractables.IngredientWheat1C.SetActive(true);
        else if (WheatCount >= 4) objectButtonsInteractables.IngredientWheat1D.SetActive(true);
        else if (WheatCount >= 5) objectButtonsInteractables.IngredientWheat1E.SetActive(true);

    }
    public void HandleInteraction(string targetTag)
    {
        switch (targetTag)
        {
            case "MillStone":
                Debug.Log("Interacted with MillStone");
                InteractMillStone();
                break;

            case "NextLevelDebug":              
                Debug.Log("Interacted with NextLevelDebug");
                InteractNextLevelDebug();
                break;

          
        }
    }
    public void WheatCollect()
    {
        WheatCount++;
        if (ItemManager.instace != null) ItemManager.instace.AddToItem("wheat");

        if (objectButtonsInteractables != null)
            objectButtonsInteractables.SetWheat_TXT();
    }
    public void InteractMillStone()
    {       
        if (WheatCount >= 5)
        {
            WheatCount = 0;
            FlourCount = 1;
            if (ItemManager.instace != null) ItemManager.instace.AddToItem("flour");
        }
        if (objectButtonsInteractables != null)
            objectButtonsInteractables.SetFlour_TXT();       

    }

    public void InteractBeeHive()
    {
        HoneyCount = 1;
        if (ItemManager.instace != null) ItemManager.instace.AddToItem("honey");

        if (objectButtonsInteractables != null)
            objectButtonsInteractables.SetHoney_TXT();
    }

    public void InteractSythe()
    {
        EnableWheatCollider();

    }
    public void InteractNextLevelDebug()
    { 
        if (menuManager != null)
            menuManager.onLoadNextLevel();
    }


    public void DisableWheatCollider()
    {       
        GameObject wheat = GameObject.Find("Wheat");
              
        if (wheat != null && wheat.TryGetComponent<BoxCollider2D>(out BoxCollider2D boxCollider2D))
        {
            boxCollider2D.enabled = false;
        }
    }
    public void EnableWheatCollider()
    {      
        GameObject wheat = GameObject.Find("Wheat");

        if (wheat != null && wheat.TryGetComponent<BoxCollider2D>(out BoxCollider2D boxCollider2D))
        {
            boxCollider2D.enabled = true;
        }
    }


    public void Interact()
    {


        if (gameObject.CompareTag("PassiveInteractable"))
        {
            Debug.Log("Interacted with " + gameObject.name);
            if (gameObject.name == "Wheat")
                WheatCount++;
            Destroy(gameObject);
            if (objectButtonsInteractables != null)
            {
                objectButtonsInteractables.SetWheat_TXT();
            }
        }

        else if (gameObject.CompareTag("Apple"))
        {
            Debug.Log("Interacted with " + gameObject.name);
            if (gameObject.name == "Apple")
                AppleCount++;
            Destroy(gameObject);
            if (objectButtonsInteractables != null)
            {
                objectButtonsInteractables.SetApple_TXT();
            }
        }


    }

    
       public void OnMakePie()
    {

        if (AppleCount == 5 && FlourCount == 1 && HoneyCount == 1)
        {
            AppleCount = 0;
            FlourCount = 0;
            HoneyCount = 0;
            setPieVisibility();
        }
    }

    public void PieForDangle()
    {
        if (pieVisabilityManager == null) return;
        pieVisabilityManager.RawApplePieProxy.SetActive(false);
        pieVisabilityManager.ProxyOvenRawPie.SetActive(false);
        pieVisabilityManager.ProxyBakedPie.SetActive(false);
        FreshBakedPie = 1;
    }

    public void IntaDaOven()
    {
        if (pieVisabilityManager == null) return;
        pieVisabilityManager.RawApplePieProxy.SetActive(false);
        pieVisabilityManager.ProxyOvenRawPie.SetActive(true);
        pieVisabilityManager.ProxyBakedPie.SetActive(false);
    }

    public void OnBakingTime()
    {
        //pieVisabilityManager.RawApplePieProxy.SetActive(false);
        //pieVisabilityManager.ProxyOvenRawPie.SetActive(false);
        //playerScript.flameOn = true;
        //await Task.Delay(5000); // 5000 milliseconds = 5 seconds
        //playerScript.flameOn = false;
        //pieVisabilityManager.ProxyBakedPie.SetActive(true);
    
        StartCoroutine(BakePieRoutine());
    }

    private IEnumerator BakePieRoutine()
    {
        if (pieVisabilityManager != null)
        {
            pieVisabilityManager.RawApplePieProxy.SetActive(false);
            pieVisabilityManager.ProxyOvenRawPie.SetActive(false);
        }
        if (playerScript != null) playerScript.flameOn = true;

        yield return new WaitForSeconds(5f);

        if (playerScript != null) playerScript.flameOn = false;
        if (pieVisabilityManager != null)
        {
            pieVisabilityManager.ProxyBakedPie.SetActive(true);
        }
    }

    public void setPieVisibility()
    {
        if (pieVisabilityManager == null) return;
        pieVisabilityManager.RawApplePieProxy.SetActive(true);
        pieVisabilityManager.ProxyOvenRawPie.SetActive(false);
        pieVisabilityManager.ProxyBakedPie.SetActive(false);   
    }

    public void interactDangle()
    {
        if (FreshBakedPie == 1)
        {
            FreshBakedPie = 0;
            objectButtonsInteractables.DangleExit.SetActive(true);
        }


    }



}

