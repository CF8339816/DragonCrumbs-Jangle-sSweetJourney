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
    public Camera targetCamera ;

    [Header("Filtering")]
    public string targetLayerName = "RecipeButtons";
    public string ApplePieTag = "ApplePie";
    //public string otherrecipeTag = "otherrecipewheninitiated";
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
        //Changed cause the code below throws a couple of random errors depending on load orders ensures first load 
        //BootStrappin bootStrappin = FindFirstObjectByType<BootStrappin>();
        //bootStrappin.FindManagers();
    }
    void Start()
    {
       
        if (targetCamera == null)// checks to ensure script on a cam  
            targetCamera = GetComponent<Camera>();

       
        if (targetCamera == null) //ensures the derfault is the main character cam withthe brain on it
            targetCamera = GameObject.Find("MainCharacterCamera")?.GetComponent<Camera>();

        if (targetCamera == null)
            Debug.LogError("ClickThroughRaycaster: No Camera found! Please assign your Camera in the Inspector.");

        RecipeButtons = LayerMask.GetMask(targetLayerName); // sets the layermask as defined above
        if (RecipeButtons == 0)
            Debug.LogWarning("Layer '" + targetLayerName + "' does not exist! Check Project Settings > Tags and Layers.");

        //EnvironmentButtons = LayerMask.GetMask(targetLayerName); // sets the layermask as defined above
        //if (EnvironmentButtons == 0)
        //    Debug.LogWarning("Layer '" + targetLayerName + "' does not exist! Check Project Settings > Tags and Layers.");


        combinedMask = RecipeButtons | EnvironmentButtons;

    }

    void Update()
    {
        if (Mouse.current == null || targetCamera == null) return;

       
        Vector2 mousePos = Mouse.current.position.ReadValue();// casts ray from mouse oposition
        Ray ray = targetCamera.ScreenPointToRay(mousePos);

       
        if (Physics.Raycast(ray, out RaycastHit hit, maxClickDistance, combinedMask)) // only registers hits on the RecipeButtons layer
        {
           
            if (hit.collider.CompareTag(ApplePieTag))// vetts  selectable  based on tag  may be changed to recipies later if more are added , is specifically singled out for demo
            { 
                cineCaminteractmouseover button = hit.collider.GetComponent<cineCaminteractmouseover>();

                
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

                return;
            }
        }


        ClearHover(); //calls to reset method to remove highlight
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