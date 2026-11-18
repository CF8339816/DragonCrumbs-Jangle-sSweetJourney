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

        combinedMask = RecipeButtons | EnvironmentButtons;
        if (objectButtonsInteractables == null)
        {
            objectButtonsInteractables = FindFirstObjectByType<ObjectButtonsInteractables>();
        }
        if (objGOInteractionManager == null)
        {
            objGOInteractionManager = FindFirstObjectByType<ObjGOInteractionManager>();
        }
    }
    private bool EnsureReferences()  // ensures cam and hud references are found if scene change destroys them
    {
        if (firstPersonCam == null)
        {
            if (Camera.main != null)
                firstPersonCam = Camera.main.transform;
            else
            {
                GameObject camObj = GameObject.Find("MainCharacterCamera");
                if (camObj != null)
                    firstPersonCam = camObj.transform;
            }
        }

        if (objectButtonsInteractables == null)
            objectButtonsInteractables = FindFirstObjectByType<ObjectButtonsInteractables>();

        return firstPersonCam != null;
    }
    // Update is called once per frame
    void Update()
    {
        if (!EnsureReferences())
        {
            return;

        }

        CheckTargets();
        //   UpdateCrosshairVisibility();


        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame && !string.IsNullOrEmpty(currentTargetTag))
        {
            HandleInteraction(currentTargetTag);
        }
    }

    private void CheckTargets()
    {
        currentTargetTag = "";

        if (Physics.Raycast(firstPersonCam.position, firstPersonCam.forward, out RaycastHit hit, checkRange, combinedMask))
        {
            if (hit.distance <= switchRange)
            {
                currentTargetTag = hit.collider.tag;
                //UpdateCrosshairVisibility();
                return;
            }
        }
    }
    private void HandleInteraction(string targetTag)
    {
        switch (targetTag)
        {
            case "MillStone":
                Debug.Log("Interacted with MillStone");
                InteractMillStone();
                break;

            case "PrepBoard":              
                Debug.Log("Interacted with NextLevelDebug");
                InteractNextLevelDebug();
                break;

          
        }
    }

    void InteractMillStone()
    {
        if (Physics.Raycast(firstPersonCam.position, firstPersonCam.forward, out RaycastHit hit, checkRange, combinedMask))
        {
            if (hit.collider.tag == ("MillStone"))
            {

            }
        }
    }
    void InteractNextLevelDebug()
    {
        if (Physics.Raycast(firstPersonCam.position, firstPersonCam.forward, out RaycastHit hit, checkRange, combinedMask))
        {
            if (hit.collider.tag == ("NextLevelDebug"))
            {
                if (menuManager != null)
                    menuManager.onLoadNextLevel();
            }
        }
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
#region unused ref code
             //case "ApplePieRecipe":
            //    if (objectButtonsInteractables != null)
            //        objectButtonsInteractables.OnRecipeSelectApplePie();
            //    break;
            //case "Ladder":
            //    Debug.Log("Interacted with Ladder");
            //    Interactladder();
            //    break;
            //case "BeeHive":
            //    Debug.Log("Interacted with BeeHive");
            //    InteractHive();
            //    break;
          
            //case "Basket":
            //    Debug.Log("Interacted with Basket");
            //    InteractBasket();
            //    break;
            //case "ApplePie":
            //    Debug.Log("Interacted with ApplePie");
            //    InteractPie();
            //    break;
   
//void Interactladder()
    //{
    //    if (Physics.Raycast(firstPersonCam.position, firstPersonCam.forward, out RaycastHit hit, checkRange, combinedMask))
    //    {
    //        if (hit.collider.tag == ("Ladder"))
    //        {

    //        }
    //    }
    //}
    //void InteractHive()
    //{
    //    if (Physics.Raycast(firstPersonCam.position, firstPersonCam.forward, out RaycastHit hit, checkRange, combinedMask))
    //    {
    //        if (hit.collider.tag == ("Oven"))
    //        {

    //        }
    //    }
    //}


    //void InteractBasket()
    //{
    //    if (Physics.Raycast(firstPersonCam.position, firstPersonCam.forward, out RaycastHit hit, checkRange, combinedMask))
    //    {
    //        if (hit.collider.tag == ("Basket"))
    //        {

    //        }
    //    }
    //}
    //void InteractPie()
    //{
    //    if (Physics.Raycast(firstPersonCam.position, firstPersonCam.forward, out RaycastHit hit, checkRange, combinedMask))
    //    {
    //        if (hit.collider.tag == ("ApplePie"))
    //        {

    //        }
    //    }
    //}


#endregion
