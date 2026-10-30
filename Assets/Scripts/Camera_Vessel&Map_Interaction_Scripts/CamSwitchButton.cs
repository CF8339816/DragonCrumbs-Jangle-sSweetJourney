//using System.Collections.Generic;
//using System.Runtime.CompilerServices;
//using Unity.Cinemachine;
//using UnityEngine;
//using UnityEngine.Audio;
//using UnityEngine.EventSystems;
//using UnityEngine.Rendering;
//using UnityEngine.UI;

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

//public class CamSwitchButton : MonoBehaviour
//{
//    [Header("Camera settings")]
//    [SerializeField] public CinemachineCamera TargetCam; // assign approperate cam for object to this field
//    [SerializeField] public int activePriority = 20; // sets active prioity above inactive cameras but below free look camera
//    [SerializeField] public int inactivePriority = 10; //sets the inactive cams to the lowest cam level currenmtly implimented
//    private Color _originalColor; //defines variable to record inital material colour of the interactable assets not using a shader system
//    private Renderer _renderer;// defines renderer

//    [Header("Location Variables")]
//    [SerializeField] public string NameLocation; //allows defining  location name 
//    [SerializeField] public double LengthLocation; //allows defining location length
//    [SerializeField] public int VesselCount;  // allows defining  vessel count at location

//    [Header("Popup Placement ")]
//    [SerializeField] public Transform infoPin; // allows defining location popup should target
//    [SerializeField] public RectTransform uiPopup;  // allows defining the ui popup image
//    [SerializeField] public Vector3 offset;  // allows defining the offset for the popup to follow the target correctly


    
//    [Header("UI Elements")]
//    [SerializeField] private Slider volumeSlider; // defines  the slider ui element being used



//    //public enum HighlightColour { Yellow, Red, Blue, Green, Orange }  // to define specific colours later in use 
//    [Header("Highlight controls")]
//    [SerializeField] private Color Highlight;
       
//    private HashSet<Collider> vesselsInZone = new HashSet<Collider>();//ensures there is a true real time tracking of vesssels entering the target zone of each defind area and that each vessel only counted once
//     public int CurrentVesselCount => vesselsInZone.Count;// providesd a variable thatr is used to aid in maintaining a real time updated vessel count 

//    private AddAudioScript _audioScript; // defines  the audio manager script

//    [Header("audio clip")]
//    [SerializeField] private AudioClip locationClick;// defines clip 
//    private AudioSource source;// defines audio source
//    [SerializeField] private float startingVolume = .35f;
 
//    private void Awake()
//    {
//        GameObject sliderObject = GameObject.Find("Volume"); // finds slider object Volume

//        if (sliderObject != null)
//        {
//            volumeSlider = sliderObject.GetComponent<Slider>(); //gets volumeSlider and adds slider object Volume to it
//        }
//        //else
//        //{
//        //    Debug.LogError("Could not find a GameObject named 'Volume' in the scene!", gameObject);
//        //}

//        if (source == null)
//        {
//            source = GetComponent<AudioSource>();// checks for Audio Source
//        }
//        if (source == null)
//        {
//            source = gameObject.AddComponent<AudioSource>();// if none found creates one on the object
//        }

//        source.spatialBlend = 0f; //  2d ui style sound =0 . 3d spatial world sound =1
//        source.playOnAwake = false;//makes sure sound does not play at start only on click
//        source.volume = startingVolume;  //sets volume to preset starting volume
//        source.mute = false;// ensures the mute is toggled off( mine was muted through all my testing till i found a tutorial that mentioned this )
//        source.clip = locationClick;
//    }

// private void Start()
//    {
//        InitializeAudioState();



//        TargetCam.Priority = inactivePriority; //ensures camera set to inactive at start so only the Start position cam position is selected

//        if (gameObject.CompareTag("UIClickable"))// checks to see if object is a ui button tagged UIClickable that does not need a called renderer for its highlight
//        {
//            Debug.Log("This is a UI Button. Ignoring Renderer setup.");
//            return; // Exits the method early so the lines below don't run
//        }
//        else
//        {
//            _renderer = GetComponent<Renderer>();// pulls the render component to check shader value of target

//            _originalColor = _renderer.material.color; //sets an original color value to revert the mouse over to correct material color for non  shader objects
//        }
       
//    }


//    public void InitializeAudioState()
//    {
//        if (source == null)
//        {
//            source = GetComponent<AudioSource>();// checks for Audio Source backup call for prefabed calls
//        }
//        if (source == null)
//        {
//            source = gameObject.AddComponent<AudioSource>();// if none found creates one on the object  backup call for prefabed calls
//        }

//        if (volumeSlider == null) // sets up  volume slider if one is addded 
//        {


//            Slider[] allSliders = Resources.FindObjectsOfTypeAll<Slider>(); // searches entire scene for the Volume slider and identifies the one named "Volume"
//            foreach (Slider slider in allSliders)
//            {
//                if (slider.gameObject.name == "Volume")
//                {
//                    volumeSlider = slider;
//                    break;
//                }
//            }
//        }

//        if (volumeSlider != null) // sets up  volume slider if one is addded 
//        {

//            volumeSlider.minValue = 0f; // min is  muted at 0
//            volumeSlider.maxValue = 1f; // max full volume at 1
//            if (volumeSlider.value != 1f)// checks to ensure volume slidernot set differently and  defaults to it as set in scene
//            {
//                source.volume = volumeSlider.value;// sets source volume towhatever current slider setting is 
//            }
//            else
//            {
//                volumeSlider.value = source.volume;// sets slider bar to default preset start volume if the bar is set to max volume
//            }
//        }
//        else
//        {
//            source.volume = startingVolume;// hard fall back to starting volume value
//        }

//            volumeSlider.onValueChanged.AddListener(OnVolumeChanged);// listens and calls for volumre change method when adjustment to  slider
        
//    }

//    public void Update()
//    {
   
//    VesselCount = CurrentVesselCount;  //sets vessel count to the current number of vessels in the zoneeach update cycle
//    }

//    private void OnMouseEnter()
//    {
//        if (EventSystem.current.IsPointerOverGameObject() ) //&& !(gameObject.CompareTag("UIClickable")))
//        {
//            return;
//        }
//        _renderer.material.color = Highlight;//Color.green;// verifies the collider is working and picking up the mouse ratycast
//    }

//    private void OnMouseExit()
//    {
//        _renderer.material.color = _originalColor; // switches back to default 
//    }

//    public void OnMouseDown()
//    {
//        if (gameObject.CompareTag("UIClickable") || EventSystem.current.IsPointerOverGameObject())// prevents click if over a non tagged fgame object or ui clickable tagged object  to prevent multi clicking 
//        {
//            return;
//        }

//        if (gameObject.CompareTag("interactive"))
//        {
//            ExecuteCamSwitch();
//        }
//    }

//    public void ExecuteCamSwitch()
//    {
//        userController controller = FindFirstObjectByType<userController>();// get user controller 

//        if (controller != null)
//        {
//            controller.SnapToTarget(TargetCam.transform); // tell user to move to target cam's location & orientation
//        }

//        InfoPopupController popup = FindFirstObjectByType<InfoPopupController>(); //gets popup controller
//        if (popup != null)
//        {
//            popup.OpenInfo(this); //ensures popup is pulling data from the active target
//        }

        
//        if (volumeSlider != null)// forces the audio to fgrab the current slider volume value for the clip before playing in case this weas activatted from another function
//        {
//            source.volume = volumeSlider.value;
//        }
//        else
//        {

//            Slider[] allSliders = Resources.FindObjectsOfTypeAll<Slider>(); // searches entire scene for the Volume slider and identifies the one named "Volume"
//            foreach (Slider slider in allSliders)
//            {
//                if (slider.gameObject.name == "Volume")
//                {
//                    volumeSlider = slider;
//                    break;
//                }
//            }
//        }

//        if (volumeSlider != null && source != null)
//        {
//            source.volume = volumeSlider.value; // force volume to match the active slider state
//        }
//        else if (source != null)
//        {
//            source.volume = startingVolume; //emergency fallback to preset  script default starting volume if slider is totally missing
//        }

//        if (source != null && locationClick != null)// makes sure both audio source and sound clip are loaded and present
//        {
//            source.PlayOneShot(locationClick,1.0f);//plays audio and ensures it defaults to presets 

//            Debug.Log("Sound triggered on: " + gameObject.name  );// debug for verification
//            Debug.Log("Volume is : " + source.volume);// debug for verification
//        }
//        else
//        {
//            Debug.LogError("MISSING CLIP OR SOURCE ON: " + gameObject.name);  // Debug failure verification
//        }
//    }
//    public void OnTriggerEnter(Collider other)
//    {
//        if (other.CompareTag("interactiveVessel"))
//        {
//            if (vesselsInZone.Add(other))// using the hashset  for enter and exit methoids adds true if entering and false if it's already in there, for dark magics output
//            {
//                Debug.Log($"Vessel {other.name} entered. Total in zone: {vesselsInZone.Count}");
//            }
           
//        }
//    }

//    public void OnTriggerExit(Collider other)
//    {
//        if (other.CompareTag("interactiveVessel"))
//        {
//            if (vesselsInZone.Remove(other))
//            {
//                Debug.Log($"Vessel {other.name} left. Total in zone: {vesselsInZone.Count}");
//            }
//        }
//    }
//    public void OnVolumeChanged(float newVolume)
//    {
//        if (source != null)  //allose for volumme to be changedc based on slider valuie during play 
//        {
//            source.volume = newVolume;
//        }
//    }
//}





