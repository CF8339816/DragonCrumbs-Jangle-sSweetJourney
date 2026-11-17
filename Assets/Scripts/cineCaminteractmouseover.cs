using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEngine.UIElements.UxmlAttributeDescription;
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

public class cineCaminteractmouseover : MonoBehaviour
{
    [Header("Highlight Settings")]
    public Color highlightColor = Color.purple;
    private Renderer objRenderer;
    private Color originalColor;
    private bool isURP = false;
    [Header("Button Action")]
    public UnityEvent onObjectClicked;

    void Start()
    {
        objRenderer = GetComponentInChildren<Renderer>();

        if (objRenderer != null)
        {
           
            if (objRenderer.material.HasProperty("_BaseColor"))
            {
                isURP = true;
                originalColor = objRenderer.material.GetColor("_BaseColor");
            }
            else
            {
                originalColor = objRenderer.material.color;
            }
        }



        //objRenderer = GetComponent<Renderer>();
        //if (objRenderer != null)
        //    originalColor = objRenderer.material.color;
    }

    public void SetHighlight(bool isHighlighted)
    {
        if (objRenderer == null) return;

        Color targetColor = isHighlighted ? highlightColor : originalColor;

        if (isURP)
            objRenderer.material.SetColor("_BaseColor", targetColor);
        else
            objRenderer.material.color = targetColor;




        //if (objRenderer != null)
        //    objRenderer.material.color = isHighlighted ? highlightColor : originalColor;
    }

    public void TriggerClick()
    {
        Debug.Log(gameObject.name + " clicked!");
        onObjectClicked?.Invoke();
    }
}






#region Old method to do 

//[Header("Highlight controls")]
//[SerializeField] private Color Highlight = Color.yellow;
//private Color _originalColor; //defines variable to record inital material colour of the interactable assets not using a shader system
//private Renderer _renderer;
//public LayerMask RecipeButtons;
//public ObjectButtonsInteractables objectButtonsInteractables;
//public UnityEvent onObjectClicked;
//// Start is called once before the first execution of Update after the MonoBehaviour is created
//void Start()
//{
//    _renderer = GetComponent<Renderer>();// pulls the render component to check shader value of target

//    if (_renderer != null)
//    {
//        _originalColor = _renderer.material.color; //sets an original color value to revert the mouse over to correct material color for non  shader objects

//    }

//}

//// Update is called once per frame
//void Update()
//{

//}

//public void OnPointerEnter(PointerEventData eventData)
//{
//    if (EventSystem.current.IsPointerOverGameObject())
//    {
//        return;
//    }
//    if (_renderer != null)
//    {
//        _renderer.material.color = Highlight;// verifies the collider is working and picking up the mouse ratycast
//    }
//}

//public void OnPointerExit(PointerEventData eventData)
//{
//    if (_renderer != null)
//    {
//        _renderer.material.color = _originalColor; // switches back to default 
//    }
//}

//public void OnPointerDown(PointerEventData eventData)
//{
//    if (gameObject.layer == RecipeButtons) //|| EventSystem.current.IsPointerOverGameObject())// prevents click if over a non tagged fgame object or ui clickable tagged object  to prevent multi clicking 
//    {
//        if (gameObject.CompareTag("ApplePie"))
//        {
//            if (eventData.button == PointerEventData.InputButton.Left)
//            {
//                onObjectClicked?.Invoke();
//                //objectButtonsInteractables.OnRecipeSelectApplePie();
//               Debug.Log(gameObject.name + " was clicked!");

//            }
//        }
//    }



//}





//private void OnTriggerEnter(Collider other)
//{
//    if (other.CompareTag("interactive"))
//    {
//        _currentLocationCollider = other;
//        CamSwitchButton locationData = other.GetComponent<CamSwitchButton>();

//        if (locationData != null)
//        {
//            NameLocation = locationData.NameLocation;
//            Debug.Log($"{NameVessel} entered region: {NameLocation}");

//            if (myVesselInfoPopup != null) // sends data to local popup variable 
//            {
//                myVesselInfoPopup.UpdateLocationDisplay(NameLocation);
//            }
//            else if (vesselPopupController._vesselInfo != null)// defaults to main popup
//            {
//                vesselPopupController._vesselInfo.UpdateLocationDisplay(NameLocation);
//            }
//        }
//    }
//}
//private void OnTriggerExit(Collider other)
//{
//    if (_currentLocationCollider == other)
//    {
//        _currentLocationCollider = null;
//        NameLocation = "N/A";

//        if (myVesselInfoPopup != null)
//        {
//            myVesselInfoPopup.UpdateLocationDisplay(NameLocation);
//        }
//        else if (vesselPopupController._vesselInfo != null)
//        {
//            vesselPopupController._vesselInfo.UpdateLocationDisplay(NameLocation);
//        }
//    }
//}

#endregion



