using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
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


public class StageInteractManager : MonoBehaviour
{ 
      [Header("References")]
    public Transform firstPersonCam;
    public ObjectButtonsInteractables objectButtonsInteractables;
    public ObjGOInteractionManager objGOInteractionManager;
    public MenuManager menuManager;
    public JangleController playerScript;
    public LayerMask RecipeButtons;
    public LayerMask EnvironmentButtons;
    public LayerMask combinedMask;
    public CharacterController characterController;

    private string currentTargetTag = "";

    [Header("Settings")]
    public float checkRange = 100f;//longer range for grapple and pull
    public float switchRange = 4f; // shorter range for button and switch

    [Header("Interactables")]
    public GameObject ProxyOvenRawPie;
    public GameObject ProxyBakedPie;
    public GameObject RawApplePieProxy;
    public GameObject PrepBlock;
    public GameObject Dangle;
    public GameObject MillstoneClickable;
    public GameObject Wheat;

    [Header("Collection Variables")]
    public int WheatCount = 0;
    public int AppleCount = 0;
    public int FlourCount = 0;
    public int HoneyCount = 0;





    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex >= 1 && SceneManager.GetActiveScene().buildIndex <= 3)
        {
            objectButtonsInteractables.SetQuestState();
        }
        else if (SceneManager.GetActiveScene().buildIndex == 4 )
        {
            objectButtonsInteractables.SetBakeryState();
        }


        DisableWheatCollider();

        //combinedMask = RecipeButtons | EnvironmentButtons;
        if (objectButtonsInteractables == null)
        {
            objectButtonsInteractables = FindFirstObjectByType<ObjectButtonsInteractables>();
        }
        if (objGOInteractionManager == null)
        {
            objGOInteractionManager = FindFirstObjectByType<ObjGOInteractionManager>();
        }
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
    }
   public void InteractMillStone()
    {
        if (WheatCount == 5)
        {
            WheatCount = 0;
            FlourCount = 1;
        }
        else
        {
            FlourCount = 0;
        }

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
              
        if (wheat != null && wheat.TryGetComponent<BoxCollider>(out BoxCollider boxCollider))
        {
            boxCollider.enabled = false;
        }
    }
    public void EnableWheatCollider()
    {      
        GameObject wheat = GameObject.Find("Wheat");

        if (wheat != null && wheat.TryGetComponent<BoxCollider>(out BoxCollider boxCollider))
        {
            boxCollider.enabled = true;
        }
    }















}

