//using UnityEngine;
//using TMPro;


///// <summary>
///// Datifex internship project 20260422
///// Chris French First Year NSCC Game Programming 
///// 
///// </summary>


/////<summary>
/////
/////  commenting out the debug logs now that testing is done , leaving them in for future debugging needs 
/////  but want to ensure fast engine processing.
///// 
///// </summary>


//public class LocationButtonManager : MonoBehaviour
//{
//    [Header("Data Source")]
//    [SerializeField] private CamSwitchButton targetedLocation;

//    [Header("UI Text Fields (On This Button)")]
//    [SerializeField] private TMP_Text nameLoc;
//    [SerializeField] private TMP_Text lengthLoc;
//    [SerializeField] private TMP_Text autCountLoc;
//    [SerializeField] private TMP_Text unautCountLoc;
//    private ButtonObject _buttonObject;

//    private void Awake()
//    {
//        _buttonObject = GetComponent<ButtonObject>();
//    }

//    private void Start()
//    {
//       UpdateStaticText();// sets up text fields on  button
//    }

//    private void Update()
//    {
//        UpdateAuthorizedVesselCount();// tracks   authorized  vessel  count
       
//        UpdateUnAuthorizedVesselCount(); // tracks un authorized vessel  count

//    }

    
//    public void UpdateStaticText()// updates  static  fields like name andf length
//    {
//        if (targetedLocation == null) return;

//        if (nameLoc != null)
//        {
//            nameLoc.text = $"Location: {targetedLocation.NameLocation}";
//        }

//        if (lengthLoc != null)
//        {
//            lengthLoc.text = $"Length: {targetedLocation.LengthLocation}m";
//        }

//        UpdateAuthorizedVesselCount();
        
//        UpdateUnAuthorizedVesselCount();

//    }

   
//    private void UpdateAuthorizedVesselCount()// refreshes  count  on updatecycle  to  ensure accurate count
//    {
//        if (targetedLocation != null && autCountLoc != null)
//        {
//            autCountLoc.text = $"Vessels: {targetedLocation.VesselCount}";
//        }
//    }
//    private void UpdateUnAuthorizedVesselCount() 
//    {
//        if (targetedLocation != null && autCountLoc != null)
//        {
//            unautCountLoc.text = $"Un-Authorized Vessels = N/A";// { targetedLocation.unauthVesselCount}";///  to be added  later as a  futiurte function
//        }
//    }
 
//    public CamSwitchButton GetLinkedLocation()// calls  to specified  location to  get  name data  
//    {
//        return targetedLocation;
//    }
//}