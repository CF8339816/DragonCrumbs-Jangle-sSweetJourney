//using Unity.Cinemachine;
//using UnityEngine;

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



//public class userController : MonoBehaviour
//{
   
//    [Header("Components")]
//    [SerializeField] private CinemachineCamera freeLookCam;
//    [SerializeField] private CinemachineCamera TargetCam ;
//    [SerializeField] private CinemachineCamera StartCam;

//    [Header("Rotation (Middle Mouse)")]
//    public float rotationSpeed = 5.0f;
//    private float yaw;
//    private float pitch;

//    [Header("Position (Right Click)")]
//    public float moveSpeed = 0.5f;

//    [Header("Zoom (Scroll)")]
//    public float zoomSpeed = 5.0f;
//    public float minZoom = 2f;
//    public float maxZoom = 30f;
    
//    void Start()
//    {
//        InitializeCameraPosition();// processes initial camera location and allows for free look cam to sync to that location and still move around

//    }

//    private void InitializeCameraPosition()
//    {
        
//        StartingCameraLocation starter = Object.FindFirstObjectByType<StartingCameraLocation>();//looks for star cam script

//        if (starter != null && starter.TargetCam != null)
//        {
//            SyncToCamera(starter.TargetCam);
//            //Debug.Log("synced to start cam: " + starter.TargetCam.name); //Debugs are good
//            return; // exits since start cam found 
//        }
//        //else
//        //{
//        //    Debug.LogWarning("start cam not found was not found!"); // more debugs are better ^_^
//        //}


//        CamSwitchButton switcher = Object.FindFirstObjectByType<CamSwitchButton>();// continues to look for cam targets if not at starty cam poition
//        if (switcher != null && switcher.TargetCam != null)
//        {
//            SyncToCamera(switcher.TargetCam);
//            //Debug.Log("synced to target cam: " + switcher.TargetCam.name);//Debugs are good
//        }
//        //else
//        //{
//        //    Debug.LogWarning("Camswitch or targetcam was not found!"); // more debugs are better ^_^
//        //}
//    }

//    private void SyncToCamera(CinemachineCamera cam)
//    {
//        TargetCam = cam;
//        transform.position = cam.transform.position;  // sets position from referenced target cam
//        transform.rotation = cam.transform.rotation;  // sets rotation from referenced target cam

//        Vector3 euler = transform.eulerAngles; // makes sure that the angles are standardised readable in the world space
//        yaw = euler.y;
//        pitch = euler.x;

       
//        freeLookCam.Priority = 30; // Ensure the freeLookCam attached to user contrioller is master active, sets to highest currently established priority
//    }

//    void Update()  // verifies movement function seach update cycle
//    {
//        HandleOrbit();
//        HandleMovement();
//        HandleZoom();
//    }

//    private void HandleOrbit()
//    {    
//        if (Input.GetMouseButton(0))// left mouse button to rotate camera changed to beter emulate the demop app
//        {
//            yaw += Input.GetAxis("Mouse X") * rotationSpeed;
//            pitch -= Input.GetAxis("Mouse Y") * rotationSpeed;
//            pitch = Mathf.Clamp(pitch, -40f, 85f);
//            transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
//            //Debug.Log("successfully recieved pan data " + pitch  + yaw ); //Debugs are good.
//        }
//        //else
//        //{
//        //    Debug.LogWarning("pan move input not found"); // more debugs are better ^_^
//        //}
//    }

//    private void HandleMovement()
//    {
//        if (Input.GetMouseButton(1))// right click to pan
//        {
//            float moveX = Input.GetAxis("Mouse X") * moveSpeed;
//            float moveY = Input.GetAxis("Mouse Y") * moveSpeed;

         
//            Vector3 move = (transform.right *50* -moveX) + (transform.up *100* -moveY); // moves dependant on camera facing position
//            transform.position += move;

//            ApplyYLimit(); // sets pan to not move user into negative y value position 

//            //Debug.Log("successfully recieved pan data " + move); //Debugs are good.
//        }
//        //else
//        //{
//        //    Debug.LogWarning("pan move input not found"); // more debugs are better ^_^
//        //}
//    }

//    private void HandleZoom()
//    {
//        //float scroll = Input.GetAxis("Mouse ScrollWheel");

//        float scroll = Input.mouseScrollDelta.y;
//        if (Mathf.Abs(scroll) > 0.001f)
//        {

//            Vector3 zoomMove = transform.forward * (scroll * zoomSpeed * 400f * Time.deltaTime);// applies mouse scroll input to the user object the camera is tied to .
//            transform.position += zoomMove; // sets the uset location based on above input.

//            ApplyYLimit(); // sets zoom to not move user into negative y value position 


//            //Debug.Log("successfully recieved zoom data " + zoomMove); //Debugs are good.
//        }
//        //else
//        //{
//        //    Debug.LogWarning("mouse wheel scroll input not found"); // more debugs are better ^_^
//        //}
//    }

//    public void SnapToTarget(Transform TargetCamTransform)
//    {
        
//        transform.position = TargetCamTransform.position;//move to new cam position

//        transform.rotation = TargetCamTransform.rotation;// match target cam rotation

       
//        Vector3 euler = transform.eulerAngles;// makes sur mouse does not revert to old values
//        yaw = euler.y;
//        pitch = euler.x;

     
//        freeLookCam.Priority = 30; // Makes the freelook cam boss  above static cams value (20)
//    }

//    private void ApplyYLimit()
//    {
//        if (transform.position.y < 0f)  // sets dependicy for min value in y
//        {
//            transform.position = new Vector3(transform.position.x, 0f, transform.position.z); // prevents y values from entering the negative 
//        }
//    }
//}