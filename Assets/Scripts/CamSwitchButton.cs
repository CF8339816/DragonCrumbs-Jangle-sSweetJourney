//using UnityEngine;
//using Unity.Cinemachine;


///// <summary>
///// Datifex internship project 20260422
///// Chris French First Year NSCC Game Programming 
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

//    private void Start()
//    {
//        TargetCam.Priority = inactivePriority; //ensures camera set to inactive at start so only the Start position cam position is selected

//        _renderer = GetComponent<Renderer>();// pulls the render component to check shader value of target
                
//        _originalColor = _renderer.material.color; //sets an original color value to revert the mouse over to correct material color for non  shader objects
    
//    }
//    private void OnMouseEnter()
//    {
//        GetComponent<Renderer>().material.color = Color.green;// verifies the collider is working and picking up the mouse ratycast
//    }

//    private void OnMouseExit()
//    {
//        GetComponent<Renderer>().material.color = _originalColor; // switches back to default 
//    }

//    private void OnMouseDown()
//    {
//        if (gameObject.CompareTag("interactive"))
//        {
//         userController controller = FindFirstObjectByType<userController>();// get user controller 

//            if (controller != null)
//            {
//                controller.SnapToTarget(TargetCam.transform); // tell user to move to target cam's location & orientation
//            }
//        }
//    }
//}
