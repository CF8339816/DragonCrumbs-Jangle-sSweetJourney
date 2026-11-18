using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

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

   public void InteractMillStone()
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

