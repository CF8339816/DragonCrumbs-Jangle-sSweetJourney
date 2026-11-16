using UnityEngine;
using UnityEngine.UI;
using TMPro;
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

public class ObjGOInteractionManager : MonoBehaviour
{
   
     [Header("References")]
    public Transform firstPersonCam;
    public ObjectButtonsInteractables objectButtonsInteractables;
    public JangleController playerScript;
    public LayerMask RecipeButtons;
    public LayerMask EnvironmentButtons;
    public LayerMask combinedMask;
    public CharacterController characterController;

    [Header("Settings")]
    public float checkRange = 100f;//longer range for grapple and pull
    public float switchRange = 4f; // shorter range for button and switch
   
    [Header("Crosshair Settings")]
    public Image butterfly;
    public Image ApplePieRecipe;
    public Image PrepBoard;
    public Image Oven;
    public Image Ladder;
    public Image Basket;
    public Image MillStone;
    public Image BeeHive;
    public Image ApplePie;
    //[Header("Switches")]
   
    //[Header("UI Reference")]
   
    //private Rigidbody targetBody;

    private bool lookingAtOvenDoor = false;
    private bool lookingAtPrepBoard = false;
    private bool lookingAtApplePieRecipe = false;
    private bool lookingAtLadder = false;
    private bool lookingAtBeeHive = false;
    private bool lookingAtMillStone = false;
    private bool lookingAtBasket = false;
    private bool lookingAtApplePie = false;

    private string currentTargetTag = "";



    void Start()
    {
        combinedMask = RecipeButtons | EnvironmentButtons;
        if (objectButtonsInteractables == null)
        {
            objectButtonsInteractables = FindFirstObjectByType<ObjectButtonsInteractables>();
        }
        ResetCrosshairUI();
      //  HidePopups();

    }
    //private void HidePopups()
    //{
        
    //}
    void Update()
    {
        
        CheckTargets();
            UpdateCrosshairVisibility();
       

        if (Input.GetMouseButtonDown(0) && !string.IsNullOrEmpty(currentTargetTag))
        {
            HandleInteraction(currentTargetTag);

            if (lookingAtOvenDoor)
            {
                UpdateCrosshairVisibility();
               InteractOven();
            }
            if (lookingAtPrepBoard)
            {
                UpdateCrosshairVisibility();
                InteractPrep();
            }
            if (lookingAtApplePieRecipe)
            {
                UpdateCrosshairVisibility();
                objectButtonsInteractables.OnRecipeRePick();
            }
            if (lookingAtLadder)
            {
                UpdateCrosshairVisibility();
                Interactladder();
            }
            if (lookingAtBeeHive)
            {
                UpdateCrosshairVisibility();
                InteractHive();
            }
            if (lookingAtMillStone)
            {
                UpdateCrosshairVisibility();
                InteractMill();
            }
            if (lookingAtBasket)
            {
                UpdateCrosshairVisibility();
                InteractBasket();
            }
            if (lookingAtApplePie)
            {
                UpdateCrosshairVisibility();
                InteractPie();
            }

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
                UpdateCrosshairVisibility();
                return;
            }
        }

        ResetCrosshairUI();
    }
    private void HandleInteraction(string targetTag)
    {
        switch (targetTag)
        {
            case "Oven":
                Debug.Log("Interacted with Oven");
                InteractOven();
                break;
            case "PrepBoard":
                Debug.Log("Interacted with PrepBoard");
                InteractPrep();
                break;
            case "ApplePieRecipe":
                if (objectButtonsInteractables != null)
                    objectButtonsInteractables.OnRecipeSelectApplePie();
                break;
            case "Ladder":
                Debug.Log("Interacted with Ladder");
                Interactladder();
                break;
            case "BeeHive":
                Debug.Log("Interacted with BeeHive");
                InteractHive();
                break;
            case "MillStone":
                Debug.Log("Interacted with MillStone");
                InteractMill();
                break;
            case "Basket":
                Debug.Log("Interacted with Basket");
                InteractBasket();
                break;
            case "ApplePie":
                Debug.Log("Interacted with ApplePie");
                InteractPie();
                break;
        }
    }

    
    public void UpdateCrosshairVisibility()
    {
        if (Physics.Raycast(firstPersonCam.position, firstPersonCam.forward, out RaycastHit hit, checkRange, combinedMask))
        {
            Debug.Log($"Raycast hit: {hit.collider.name} with tag: {hit.collider.tag}");

            if (hit.collider.tag == "ApplePieRecipe")
            {
                if (butterfly != null) butterfly.enabled = false;
                if (ApplePieRecipe != null) ApplePieRecipe.enabled = true;
                if (PrepBoard != null) PrepBoard.enabled = false;
                if (Oven != null) Oven.enabled = false;
                if (Ladder != null) Ladder.enabled = false;
                if (Basket != null) Basket.enabled = false;
                if (MillStone != null) MillStone.enabled = false;
                if (BeeHive != null) BeeHive.enabled = false;
                if (ApplePie != null) ApplePie.enabled = false;
            }
            else if (hit.collider.tag == "PrepBoard")
            {
                if (butterfly != null) butterfly.enabled = false;
                if (ApplePieRecipe != null) ApplePieRecipe.enabled = false;
                if (PrepBoard != null) PrepBoard.enabled = true;
                if (Oven != null) Oven.enabled = false;
                if (Ladder != null) Ladder.enabled = false;
                if (Basket != null) Basket.enabled = false;
                if (MillStone != null) MillStone.enabled = false;
                if (BeeHive != null) BeeHive.enabled = false;
                if (ApplePie != null) ApplePie.enabled = false;
            }
            else if (hit.collider.tag == "Oven")
            {
                if (butterfly != null) butterfly.enabled = false;
                if (ApplePieRecipe != null) ApplePieRecipe.enabled =false ;
                if (PrepBoard != null) PrepBoard.enabled = false;
                if (Oven != null) Oven.enabled =true ;
                if (Ladder != null) Ladder.enabled = false;
                if (Basket != null) Basket.enabled = false;
                if (MillStone != null) MillStone.enabled = false;
                if (BeeHive != null) BeeHive.enabled = false;
                if (ApplePie != null) ApplePie.enabled = false;
            }
            else if (hit.collider.tag == "Ladder")
            {
                if (butterfly != null) butterfly.enabled = false;
                if (ApplePieRecipe != null) ApplePieRecipe.enabled =false ;
                if (PrepBoard != null) PrepBoard.enabled = false;
                if (Oven != null) Oven.enabled = false;
                if (Ladder != null) Ladder.enabled =true ;
                if (Basket != null) Basket.enabled = false;
                if (MillStone != null) MillStone.enabled = false;
                if (BeeHive != null) BeeHive.enabled = false;
                if (ApplePie != null) ApplePie.enabled = false;
            }
            else if (hit.collider.tag == "Basket")
            {
                if (butterfly != null) butterfly.enabled = false;
                if (ApplePieRecipe != null) ApplePieRecipe.enabled = false;
                if (PrepBoard != null) PrepBoard.enabled =false;
                if (Oven != null) Oven.enabled = false;
                if (Ladder != null) Ladder.enabled = false;
                if (Basket != null) Basket.enabled = true ;
                if (MillStone != null) MillStone.enabled = false;
                if (BeeHive != null) BeeHive.enabled = false;
                if (ApplePie != null) ApplePie.enabled = false;
            }
            else if (hit.collider.tag == "MillStone")
            {
                if (butterfly != null) butterfly.enabled = false;
                if (ApplePieRecipe != null) ApplePieRecipe.enabled =false ;
                if (PrepBoard != null) PrepBoard.enabled = false;
                if (Oven != null) Oven.enabled = false;
                if (Ladder != null) Ladder.enabled = false;
                if (Basket != null) Basket.enabled = false;
                if (MillStone != null) MillStone.enabled =true ;
                if (BeeHive != null) BeeHive.enabled = false;
                if (ApplePie != null) ApplePie.enabled = false;
            }
            else if (hit.collider.tag == "BeeHive")
            {
                if (butterfly != null) butterfly.enabled = false;
                if (ApplePieRecipe != null) ApplePieRecipe.enabled = false;
                if (PrepBoard != null) PrepBoard.enabled = false;
                if (Oven != null) Oven.enabled = false;
                if (Ladder != null) Ladder.enabled = false;
                if (Basket != null) Basket.enabled = false;
                if (MillStone != null) MillStone.enabled = false;
                if (BeeHive != null) BeeHive.enabled =true ;
                if (ApplePie != null) ApplePie.enabled = false;
            }
            else if (hit.collider.tag == "ApplePie")
            {
                if (butterfly != null) butterfly.enabled = false;
                if (ApplePieRecipe != null) ApplePieRecipe.enabled = false;
                if (PrepBoard != null) PrepBoard.enabled = false;
                if (Oven != null) Oven.enabled = false;
                if (Ladder != null) Ladder.enabled = false;
                if (Basket != null) Basket.enabled = false;
                if (MillStone != null) MillStone.enabled = false;
                if (BeeHive != null) BeeHive.enabled = false;
                if (ApplePie != null) ApplePie.enabled =true ;
            }
            else
            {
                ResetCrosshairUI();
            }
        }
        else
        {
            ResetCrosshairUI();
        }
    }
    void InteractOven()
    {
        if (Physics.Raycast(firstPersonCam.position, firstPersonCam.forward, out RaycastHit hit, checkRange, combinedMask))
        {
            if (hit.collider.tag == ("Oven"))
            {

            }
        }
    }
    void InteractPrep()
    {
        if (Physics.Raycast(firstPersonCam.position, firstPersonCam.forward, out RaycastHit hit, checkRange, combinedMask))
        {
            if (hit.collider.tag == ("PrepBoard"))
            {

            }
        }
    }
    void Interactladder()
    {
        if (Physics.Raycast(firstPersonCam.position, firstPersonCam.forward, out RaycastHit hit, checkRange, combinedMask))
        {
            if (hit.collider.tag == ("Ladder"))
            {

            }
        }
    }
    void InteractHive()
    {
        if (Physics.Raycast(firstPersonCam.position, firstPersonCam.forward, out RaycastHit hit, checkRange, combinedMask))
        {
            if (hit.collider.tag == ("Oven"))
            {

            }
        }
    }
    void InteractMill()
    {
        if (Physics.Raycast(firstPersonCam.position, firstPersonCam.forward, out RaycastHit hit, checkRange, combinedMask))
        {
            if (hit.collider.tag == ("MillStone"))
            {

            }
        }
    }
    void InteractBasket()
    {
        if (Physics.Raycast(firstPersonCam.position, firstPersonCam.forward, out RaycastHit hit, checkRange, combinedMask))
        {
            if (hit.collider.tag == ("Basket"))
            {

            }
        }
    }
    void InteractPie()
    {
        if (Physics.Raycast(firstPersonCam.position, firstPersonCam.forward, out RaycastHit hit, checkRange, combinedMask))
        {
            if (hit.collider.tag == ("ApplePie"))
            {

            }
        }
    }

    void ResetCrosshairUI()
    {
        if (butterfly != null) butterfly.enabled = true;
        if (ApplePieRecipe != null) ApplePieRecipe.enabled = false;
        if (PrepBoard != null) PrepBoard.enabled = false;
        if (Oven != null) Oven.enabled = false;
        if (Ladder != null) Ladder.enabled = false;
        if (Basket != null) Basket.enabled = false;
        if (MillStone != null) MillStone.enabled = false;
        if (BeeHive != null) BeeHive.enabled = false;
    }
    
}


#region old code
    //private void CheckTargets()
    //{
    //    lookingAtOvenDoor = false;
    //    lookingAtPrepBoard = false;
    //    lookingAtApplePieRecipe = false;
    //    lookingAtLadder = false;
    //    lookingAtBeeHive = false;
    //    lookingAtMillStone = false;
    //    lookingAtBasket = false;
    //    lookingAtApplePie = false;
    //    HidePopups();

    //    combinedMask = RecipeButtons | EnvironmentButtons;// combines both layer masks for distance checking

    //    if (Physics.Raycast(firstPersonCam.position, firstPersonCam.forward, out RaycastHit hit, checkRange, combinedMask))
    //    {
    //        if (hit.distance <= switchRange)// checks for  hitting a sswitch in range
    //        {
    //            if (hit.collider.CompareTag("ApplePieRecipe"))
    //            {
    //                lookingAtApplePieRecipe = true;
    //                UpdateCrosshairVisibility();
    //                //if (popupFWUI != null && popupFWText != null)
    //                //{
    //                //    popupFWText.text = $"Left-Click to Interact";
    //                //    popupFWUI.SetActive(true);
    //                //}
    //                return;
    //            }

    //            if (hit.collider.CompareTag("ApplePie"))
    //            {
    //                lookingAtApplePie = true;
    //                UpdateCrosshairVisibility();
    //                //if (popupFWUI != null && popupFWText != null)
    //                //{
    //                //    popupFWText.text = $"Left-Click to Interact";
    //                //    popupFWUI.SetActive(true);
    //                //}
    //                return;
    //            }

    //            else if (hit.collider.CompareTag("Oven"))
    //            {
    //                lookingAtOvenDoor = true;
    //                UpdateCrosshairVisibility();
    //                //if (popupDenUI != null && popupDenText != null)
    //                //{
    //                //    popupDenText.text = $"Left-Click to \nde-/-phase\n blocks";
    //                //    popupDenUI.SetActive(true);
    //                //}
    //                return;
    //            }
    //            else if (hit.collider.CompareTag("PrepBoard"))
    //            {
    //                lookingAtPrepBoard = true;
    //                UpdateCrosshairVisibility();
    //                //if (popupDenUI != null && popupDenText != null)
    //                //{
    //                //    popupDenText.text = $"Left-Click to \nde-/-phase\n blocks";
    //                //    popupDenUI.SetActive(true);
    //                //}
    //                return;
    //            }
    //            else if (hit.collider.CompareTag("Ladder"))
    //            {
    //                lookingAtLadder = true;
    //                UpdateCrosshairVisibility();
    //                //if (popupDenUI != null && popupDenText != null)
    //                //{
    //                //    popupDenText.text = $"Left-Click to \nde-/-phase\n blocks";
    //                //    popupDenUI.SetActive(true);
    //                //}
    //                return;
    //            }
    //            else if (hit.collider.CompareTag("BeeHive"))
    //            {
    //                lookingAtBeeHive = true;
    //                UpdateCrosshairVisibility();
    //                //if (popupDenUI != null && popupDenText != null)
    //                //{
    //                //    popupDenText.text = $"Left-Click to \nde-/-phase\n blocks";
    //                //    popupDenUI.SetActive(true);
    //                //}
    //                return;
    //            }
    //            else if (hit.collider.CompareTag("PrepBoard"))
    //            {
    //                lookingAtPrepBoard = true;
    //                UpdateCrosshairVisibility();
    //                //if (popupDenUI != null && popupDenText != null)
    //                //{
    //                //    popupDenText.text = $"Left-Click to \nde-/-phase\n blocks";
    //                //    popupDenUI.SetActive(true);
    //                //}
    //                return;
    //            }
    //            else if (hit.collider.CompareTag("Ladder"))
    //            {
    //                lookingAtLadder = true;
    //                UpdateCrosshairVisibility();
    //                //if (popupDenUI != null && popupDenText != null)
    //                //{
    //                //    popupDenText.text = $"Left-Click to \nde-/-phase\n blocks";
    //                //    popupDenUI.SetActive(true);
    //                //}
    //                return;
    //            }
    //            else if (hit.collider.CompareTag("BeeHive"))
    //            {
    //                lookingAtBeeHive = true;
    //                UpdateCrosshairVisibility();
    //                //if (popupDenUI != null && popupDenText != null)
    //                //{
    //                //    popupDenText.text = $"Left-Click to \nde-/-phase\n blocks";
    //                //    popupDenUI.SetActive(true);
    //                //}
    //                return;
    //            }


    //        }

    //    }

    //} 
    //    void LateUpdate()
    //    {
    //    }
    #endregion