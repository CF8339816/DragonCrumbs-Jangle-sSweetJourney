using System.Collections.Generic;
using UnityEngine;
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
public class CinemachineRaycastDriver : MonoBehaviour
{

    [Header("Camera Reference")]
    [Tooltip("CinemachineBrain cam (or leave empty to auto-find)")]
    public Camera targetCamera;

    [Header("Filtering")]
    public string targetLayerName1 = "RecipeButtons";
    public string targetLayerName2 = "EnvironmentButtons";

    public List<string> interactableTags = new List<string>
    {
        "ApplePie",
        "ApplePieRecipe",
        "MillStone",
        "NextLevelDebug",
        "Oven",
        "PrepBoard",
        "Ladder",
        "BeeHive",
        "Basket"
    };

    public float maxClickDistance = 100f;
    public LayerMask EnvironmentButtons;
    public LayerMask combinedMask;
    private LayerMask RecipeButtons;
    private cineCaminteractmouseover currentHoveredObject;


    void Awake()
    {
        BootStrappin bootStrappin = FindFirstObjectByType<BootStrappin>();
        if (bootStrappin != null)
        {
            bootStrappin.FindManagers();
        }
        SetupCameraAndLayers(); // [CODE REVIEW] Ensure camera & layers initialize immediately in Awake
    }

    void Start()
    {
        SetupCameraAndLayers(); // [CODE REVIEW] Re-verify in Start in case camera spawned after Awake
    }

    private void SetupCameraAndLayers()
    {
        if (targetCamera == null)// checks to ensure script on a cam  
        {
            targetCamera = GetComponent<Camera>();
        }

        if (targetCamera == null) //ensures the default is the main character cam with the brain on it
        {
            targetCamera = GameObject.Find("MainCharacterCamera")?.GetComponent<Camera>();
        }

        if (targetCamera == null)
        {
            targetCamera = Camera.main; // [CODE REVIEW] Extra fallback to Camera.main
        }

        RecipeButtons = LayerMask.GetMask(targetLayerName1); // sets the layermask as defined above
        EnvironmentButtons = LayerMask.GetMask(targetLayerName2);

        if (RecipeButtons == 0)
        {
            Debug.LogWarning("Layer '" + targetLayerName1 + "' does not exist! Check Project Settings > Tags and Layers.");
        }
        if (EnvironmentButtons == 0)
        {
            Debug.LogWarning("Layer '" + targetLayerName2 + "' does not exist! Check Project Settings > Tags and Layers.");
        }

        combinedMask = RecipeButtons | EnvironmentButtons;
    }

    void Update()
    {
        if (Mouse.current == null) return;

        // [CODE REVIEW] Re-acquire camera if scene changed and old camera was destroyed
        if (targetCamera == null)
        {
            SetupCameraAndLayers();
            if (targetCamera == null) return;
        }

        // [CODE REVIEW] Safety check in case combinedMask wasn't built yet
        if (combinedMask == 0)
        {
            SetupCameraAndLayers();
        }

        Vector2 mousePos = Mouse.current.position.ReadValue();// casts ray from mouse position
        Ray ray = targetCamera.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray, out RaycastHit hit, maxClickDistance, combinedMask))
        {
            if (interactableTags.Contains(hit.collider.tag))
            {
                HandleHoverAndClick(hit.collider.GetComponent<cineCaminteractmouseover>());
                return;
            }
        }

        RaycastHit2D hit2D = Physics2D.GetRayIntersection(ray, maxClickDistance, combinedMask);
        if (hit2D.collider != null && interactableTags.Contains(hit2D.collider.tag))
        {
            HandleHoverAndClick(hit2D.collider.GetComponent<cineCaminteractmouseover>());
            return;
        }

        ClearHover(); //calls to reset method to remove highlight
    }

    private void HandleHoverAndClick(cineCaminteractmouseover button)
    {
        if (button != currentHoveredObject)// highlights
        {
            ClearHover();
            currentHoveredObject = button;
            if (currentHoveredObject != null)
                currentHoveredObject.SetHighlight(true);
        }

        if (Mouse.current.leftButton.wasPressedThisFrame && currentHoveredObject != null)// triggers event
        {
            currentHoveredObject.TriggerClick();
        }
    }

    private void ClearHover()// clears the mouseover highlight
    {
        if (currentHoveredObject != null)
        {
            currentHoveredObject.SetHighlight(false);
            currentHoveredObject = null;
        }
    }
}