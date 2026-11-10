
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Audio;

public class cineCaminteractmouseover : MonoBehaviour
{
    [Header("Highlight controls")]
    [SerializeField] private Color Highlight;
    private Color _originalColor; //defines variable to record inital material colour of the interactable assets not using a shader system
    private Renderer _renderer;
    public LayerMask RecipeButtons;
    public ObjectButtonsInteractables objectButtonsInteractables;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _renderer = GetComponent<Renderer>();// pulls the render component to check shader value of target

        _originalColor = _renderer.material.color; //sets an original color value to revert the mouse over to correct material color for non  shader objects

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        _renderer.material.color = Highlight;//Color.green;// verifies the collider is working and picking up the mouse ratycast
    }

    private void OnMouseExit()
    {
        _renderer.material.color = _originalColor; // switches back to default 
    }

    private void OnMouseDown()
    {

        if (gameObject.layer == RecipeButtons) //|| EventSystem.current.IsPointerOverGameObject())// prevents click if over a non tagged fgame object or ui clickable tagged object  to prevent multi clicking 
        {

            if (gameObject.CompareTag("ApplePie"))
            {
                objectButtonsInteractables.OnRecipeSelectApplePie();



            }
        }



    }



}

















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





