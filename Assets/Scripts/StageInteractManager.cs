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
    private AddAudio addAudio;
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
            if (objectButtonsInteractables == null) objectButtonsInteractables = bootStrappin.objectButtonsInteractables;
            if (objGOInteractionManager == null) objGOInteractionManager = bootStrappin.objGOInteractionManager;
            if (pieVisabilityManager == null) pieVisabilityManager = bootStrappin.pieVisabilityManager;
            if (menuManager == null) menuManager = bootStrappin.menuManager;
            if (travelMapManager == null) travelMapManager = bootStrappin.travelMapManager;
        }
        //Changed cause the code below throws a couple of random errors depending on load orders ensures first load 
        //BootStrappin bootStrappin = FindFirstObjectByType< BootStrappin > ();
        //bootStrappin.FindManagers();

        if (addAudio == null) addAudio = FindFirstObjectByType<AddAudio>();
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
        if (playerScript == null)
        {
            playerScript = FindFirstObjectByType<JangleController>();
        }

        LoadCountsFromPersistent();

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
                if (MenuManager.Instance != null && MenuManager.Instance.hasActiveQuest)
                    objectButtonsInteractables.SetQuestState();
                else
                    objectButtonsInteractables.SetBakeryState();
            }
        }

        DisableWheatCollider();
    }

    void Update()
    {

        UpdateCheckBoxes();


    }

    public void LoadCountsFromPersistent()
    {
        if (ItemManager.instace != null)
        {
            AppleCount = ItemManager.instace.GetItemCountDirect("apple");
            WheatCount = ItemManager.instace.GetItemCountDirect("wheat");
            FlourCount = ItemManager.instace.GetItemCountDirect("flour");
            HoneyCount = ItemManager.instace.GetItemCountDirect("honey");
        }
    }

    public void SaveCountsToPersistent()
    {
        if (ItemManager.instace != null)
        {
            ItemManager.instace.SetItemCountDirect("apple", AppleCount);
            ItemManager.instace.SetItemCountDirect("wheat", WheatCount);
            ItemManager.instace.SetItemCountDirect("flour", FlourCount);
            ItemManager.instace.SetItemCountDirect("honey", HoneyCount);
        }
    }

    public void AddCollectedItem(string itemName)
    {
        if (string.IsNullOrEmpty(itemName)) return;
        if (objectButtonsInteractables == null) objectButtonsInteractables = FindFirstObjectByType<ObjectButtonsInteractables>();

        switch (itemName.Trim().ToLower())
        {
            case "apple":
                AppleCount = Mathf.Min(AppleCount + 1, 5);
                if (objectButtonsInteractables != null) objectButtonsInteractables.SetApple_TXT();
                break;

            case "wheat":
                WheatCount = Mathf.Min(WheatCount + 1, 5);
                if (objectButtonsInteractables != null) objectButtonsInteractables.SetWheat_TXT();
                break;

            case "flour":
                FlourCount = Mathf.Min(FlourCount + 1, 1);
                if (objectButtonsInteractables != null) objectButtonsInteractables.SetFlour_TXT();
                break;

            case "honey":
                HoneyCount = Mathf.Min(HoneyCount + 1, 1);
                if (objectButtonsInteractables != null) objectButtonsInteractables.SetHoney_TXT();
                break;
        }

        SaveCountsToPersistent();
        if (addAudio != null) addAudio.OnPickupSound();
    }

    public bool HasEnoughOfItem(string itemName)
    {
        if (string.IsNullOrEmpty(itemName)) return false;

        switch (itemName.Trim().ToLower())
        {
            case "apple": return AppleCount >= 5;
            case "wheat": return WheatCount >= 5;
            case "flour": return FlourCount >= 1;
            case "honey": return HoneyCount >= 1;
            default: return false;
        }
    }

    public void UpdateCheckBoxes()
    {
        if (objectButtonsInteractables == null) return;
        if (AppleCount >= 5)
        {
            if (objectButtonsInteractables.CheckMark1 != null) objectButtonsInteractables.CheckMark1.SetActive(true);
            if (objectButtonsInteractables.CrossOut1 != null) objectButtonsInteractables.CrossOut1.SetActive(false);
        }
        else
        {
            if (objectButtonsInteractables.CheckMark1 != null) objectButtonsInteractables.CheckMark1.SetActive(false);
            if (objectButtonsInteractables.CrossOut1 != null) objectButtonsInteractables.CrossOut1.SetActive(true);
        }
        if (FlourCount >= 1)
        {
            if (objectButtonsInteractables.CheckMark2 != null) objectButtonsInteractables.CheckMark2.SetActive(true);
            if (objectButtonsInteractables.CrossOut2 != null) objectButtonsInteractables.CrossOut2.SetActive(false);
        }
        else
        {
            if (objectButtonsInteractables.CheckMark2 != null) objectButtonsInteractables.CheckMark2.SetActive(false);
            if (objectButtonsInteractables.CrossOut2 != null) objectButtonsInteractables.CrossOut2.SetActive(true);
        }
        if (HoneyCount >= 1)
        {
            if (objectButtonsInteractables.CheckMark3 != null) objectButtonsInteractables.CheckMark3.SetActive(true);
            if (objectButtonsInteractables.CrossOut3 != null) objectButtonsInteractables.CrossOut3.SetActive(false);
        }
        else
        {
            if (objectButtonsInteractables.CheckMark3 != null) objectButtonsInteractables.CheckMark3.SetActive(false);
            if (objectButtonsInteractables.CrossOut3 != null) objectButtonsInteractables.CrossOut3.SetActive(true);
        }

        if (AppleCount >= 1 && objectButtonsInteractables.IngredientAP1A != null) objectButtonsInteractables.IngredientAP1A.SetActive(true);
        if (AppleCount >= 2 && objectButtonsInteractables.IngredientAP1B != null) objectButtonsInteractables.IngredientAP1B.SetActive(true);
        if (AppleCount >= 3 && objectButtonsInteractables.IngredientAP1C != null) objectButtonsInteractables.IngredientAP1C.SetActive(true);
        if (AppleCount >= 4 && objectButtonsInteractables.IngredientAP1D != null) objectButtonsInteractables.IngredientAP1D.SetActive(true);
        if (AppleCount >= 5 && objectButtonsInteractables.IngredientAP1E != null) objectButtonsInteractables.IngredientAP1E.SetActive(true);
        if (FlourCount >= 1 && objectButtonsInteractables.IngredientAP2A != null) objectButtonsInteractables.IngredientAP2A.SetActive(true);
        if (HoneyCount >= 1 && objectButtonsInteractables.IngredientAP3A != null) objectButtonsInteractables.IngredientAP3A.SetActive(true);
        if (WheatCount >= 1 && objectButtonsInteractables.IngredientWheat1A != null) objectButtonsInteractables.IngredientWheat1A.SetActive(true);
        if (WheatCount >= 2 && objectButtonsInteractables.IngredientWheat1B != null) objectButtonsInteractables.IngredientWheat1B.SetActive(true);
        if (WheatCount >= 3 && objectButtonsInteractables.IngredientWheat1C != null) objectButtonsInteractables.IngredientWheat1C.SetActive(true);
        if (WheatCount >= 4 && objectButtonsInteractables.IngredientWheat1D != null) objectButtonsInteractables.IngredientWheat1D.SetActive(true);
        if (WheatCount >= 5 && objectButtonsInteractables.IngredientWheat1E != null) objectButtonsInteractables.IngredientWheat1E.SetActive(true);
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
        SaveCountsToPersistent();
        if (objectButtonsInteractables != null)
            objectButtonsInteractables.SetWheat_TXT();
    }
    public void InteractMillStone()
    {
        if (WheatCount >= 5)
        {
            WheatCount = 0;
            FlourCount = 1;
            SaveCountsToPersistent();
        }
        if (objectButtonsInteractables != null)
        {
            objectButtonsInteractables.SetWheat_TXT();
            objectButtonsInteractables.SetFlour_TXT();
        }
    }
    public void InteractBeeHive()
    {
        addAudio.OnSuccess();
        HoneyCount = 1;
        SaveCountsToPersistent();
        if (objectButtonsInteractables != null)
            objectButtonsInteractables.SetHoney_TXT();
    }
    public void InteractSythe()
    {
        addAudio.OnSuccess();
        EnableWheatCollider();
    }
    public void InteractNextLevelDebug()
    {
        if (menuManager != null)
            menuManager.onLoadNextLevel();
    }
    public void DisableWheatCollider()
    {
        foreach (GameObject obj in FindObjectsByType<GameObject>(FindObjectsInactive.Include, FindObjectsSortMode.None))
        {
            if (obj.name.StartsWith("Wheat") && obj.TryGetComponent(out BoxCollider2D boxCollider2D))
            {
                boxCollider2D.enabled = false;
            }
        }

        //GameObject wheat = GameObject.Find("Wheat");
        //if (wheat != null && wheat.TryGetComponent(out BoxCollider2D boxCollider2D))
        //{
        //    boxCollider2D.enabled = false;
        //}
    }
    public void EnableWheatCollider()
    {
        foreach (GameObject obj in FindObjectsByType<GameObject>(FindObjectsInactive.Include, FindObjectsSortMode.None))
        {
            if (obj.name.StartsWith("Wheat") && obj.TryGetComponent(out BoxCollider2D boxCollider2D))
            {
                boxCollider2D.enabled = true;
            }
        }

        //GameObject wheat = GameObject.Find("Wheat");
        //if (wheat != null && wheat.TryGetComponent(out BoxCollider2D boxCollider2D))
        //{
        //    boxCollider2D.enabled = true;
        //}
    }
    public void Interact()
    {
        if (gameObject.CompareTag("PassiveInteractable"))
        {
            Debug.Log("Interacted with " + gameObject.name);
            if (gameObject.name == "Wheat")
            {
                addAudio.OnPickupSound();
                WheatCount++;
                SaveCountsToPersistent();
            }
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
            {
                addAudio.OnPickupSound();
                AppleCount++;
                SaveCountsToPersistent();
            }
            Destroy(gameObject);
            if (objectButtonsInteractables != null)
            {
                objectButtonsInteractables.SetApple_TXT();
            }
        }
    }
    public void OnMakePie()
    {
        if (AppleCount >= 5 && FlourCount >= 1 && HoneyCount >= 1)
        {
            AppleCount = 0;
            FlourCount = 0;
            HoneyCount = 0;
            SaveCountsToPersistent();
            if (objectButtonsInteractables != null)
            {
                objectButtonsInteractables.RefreshAllIngredientTexts();
            }
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
            
        }

        if (playerScript != null) playerScript.SetFireBreath(true);

        if (playerScript != null) playerScript.flameOn = true;
        addAudio.OnFireBreath();
        yield return new WaitForSeconds(28f);
        if (playerScript != null) playerScript.flameOn = false;

        if (playerScript != null) playerScript.SetFireBreath(false);

        if (pieVisabilityManager != null)
        {
            pieVisabilityManager.ProxyOvenRawPie.SetActive(false);
            pieVisabilityManager.ProxyBakedPie.SetActive(true);
        }
        addAudio.OnSuccess();
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
        addAudio.OnVoiceDangle();
        if (FreshBakedPie == 1)
        {
            FreshBakedPie = 0;
            if (objectButtonsInteractables != null && objectButtonsInteractables.DangleExit != null)
            {
                objectButtonsInteractables.DangleExit.SetActive(true);
            }
        }
    }
}