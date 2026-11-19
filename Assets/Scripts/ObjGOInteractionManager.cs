using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
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

public class ObjGOInteractionManager : MonoBehaviour
{
   
    [Header("Camera & Crosshair References")]
    [Tooltip("CinemachineBrain cam (or leave empty to auto-find)")]
    public Camera targetCamera;
    public Transform firstPersonCam;
    //public ObjGOInteractionManager crosshairManager;

    [Header("Filtering")]
    public string recipeLayerName = "RecipeButtons";
    public string environmentLayerName = "EnvironmentButtons";
    public float maxClickDistance = 100f;

    [Tooltip("All tags the mouse pointer can interact with")]
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

    public LayerMask EnvironmentButtons;
    public LayerMask combinedMask;
    private LayerMask RecipeButtons;
    private cineCaminteractmouseover currentHoveredObject;

    void Start()
    {
        EnsureReferences();

        RecipeButtons = LayerMask.GetMask(recipeLayerName);
        if (RecipeButtons == 0)
            Debug.LogWarning("Layer '" + recipeLayerName + "' does not exist!");
        if (targetCamera == null)
            targetCamera = GameObject.Find("MainCharacterCamera")?.GetComponent<Camera>();
        if (EnvironmentButtons == 0)
            EnvironmentButtons = LayerMask.GetMask(environmentLayerName);

        combinedMask = RecipeButtons | EnvironmentButtons;
    }

    private void EnsureReferences()
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

        if (targetCamera == null)
            targetCamera = GetComponent<Camera>();
        if (targetCamera == null)
            targetCamera = GameObject.Find("MainCharacterCamera")?.GetComponent<Camera>();
        if (targetCamera == null)
            targetCamera = Camera.main;

        //if (crosshairManager == null)
        //    crosshairManager = FindFirstObjectByType<ObjGOInteractionManager>();
    }

    void Update()
    {
        //if (Mouse.current == null) return;
        ////if (targetCamera == null || crosshairManager == null) EnsureReferences();
       if (targetCamera == null)
        {
            EnsureReferences();
            if (targetCamera == null) return; // Exit safely if no camera exists yet
        }



        Vector2 mousePos = Mouse.current.position.ReadValue();
        Ray ray = targetCamera.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray, out RaycastHit hit, maxClickDistance, combinedMask))
        {
            string hitTag = hit.collider.tag;

            if (interactableTags.Contains(hitTag))
            {
               
                //if (crosshairManager != null)
                //    crosshairManager.SetCrosshairByTag(hitTag);

                cineCaminteractmouseover button = hit.collider.GetComponent<cineCaminteractmouseover>();
                if (button != currentHoveredObject)
                {
                    ClearHover();
                    currentHoveredObject = button;
                    if (currentHoveredObject != null)
                        currentHoveredObject.SetHighlight(true);
                }

                // 3. Trigger the object's local UnityEvent on click
                if (Mouse.current.leftButton.wasPressedThisFrame && currentHoveredObject != null)
                {
                    currentHoveredObject.TriggerClick();
                }

                return;
            }
        }

        ClearHover();
    }

    private void ClearHover()
    {
        if (currentHoveredObject != null)
        {
            currentHoveredObject.SetHighlight(false);
            currentHoveredObject = null;
        }

        //    if (crosshairManager != null)
        //        crosshairManager.ResetCrosshairUI();
    }
}




