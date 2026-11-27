using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;



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


[RequireComponent(typeof(Rigidbody2D))]
public class JangleController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpSpeed = 8f;
    [SerializeField] private float glideSpeed = -1.5f; // ----Gentle downward speed cap for glide---- CF

    [Header("References")]
    [SerializeField] private GameObject jangleFlame; // ----set firebreath object----CF
    [SerializeField] private GameObject interactPromptUI; //----setting up  interacxt popup----CF
    private AddAudio addAudio;
    private PlayerControls inputActions;
    private Vector2 moveInput;
    private Rigidbody2D rb2d;

    private bool isGliding = false;
    public bool flameOn = false;
    private bool isFacingRight = false;
    public bool isBaking = false;
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
        addAudio = FindFirstObjectByType<AddAudio>();
        inputActions = new PlayerControls();
        rb2d = GetComponent<Rigidbody2D>();
        if (interactPromptUI != null) interactPromptUI.SetActive(false);
        if (rb2d.gravityScale == 0)// ----maks sure rigid body handles gravity---- CF
        {
            rb2d.gravityScale = 1.5f; //----tweaking gravity value for feel of game play---- CF
        }
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();

        inputActions.Player.Move.performed += OnMoveInput; // ---- WASD Movement ---- CF
        inputActions.Player.Move.canceled += OnMoveStopped;

        inputActions.Player.JumpGlide.performed += OnJumpGlideInput; // ---- Spacebar Jump & Glide ---- CF
        inputActions.Player.JumpGlide.canceled += OnJumpGlideStopped;

        inputActions.Player.FireBreath.performed += OnFireBreathInput;   // ----Ctrl either left or right  Fire Breath  changed from shift to avoid sticky keys if player spams breath---- CF
        inputActions.Player.FireBreath.canceled += OnFireBreathStopped;

        inputActions.Player.Interact.performed += OnInteract;
    }

    private void OnDisable()
    {
        inputActions.Player.Move.performed -= OnMoveInput;
        inputActions.Player.Move.canceled -= OnMoveStopped;

        inputActions.Player.JumpGlide.performed -= OnJumpGlideInput;
        inputActions.Player.JumpGlide.canceled -= OnJumpGlideStopped;

        inputActions.Player.FireBreath.performed -= OnFireBreathInput;
        inputActions.Player.FireBreath.canceled -= OnFireBreathStopped;

        inputActions.Player.Interact.performed -= OnInteract;

        inputActions.Player.Disable();
    }

    //  direct E-key fallback in case PlayerControls.Player.Interact isn't bound to Keyboard E
    private void Update()
    {
        if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame && currentInteractable != null)
        {
            TriggerCurrentInteractable();
        }
    }

    private void FixedUpdate()
    {
        if (DialogManager.dialogManager != null && DialogManager.dialogManager.talking|| isBaking)
        {
            moveInput = Vector2.zero;
            rb2d.linearVelocity = new Vector2(0f, rb2d.linearVelocity.y);
            return;
        }

        MovePlayer();
        HandleGlide();

    }

    private void OnMoveInput(InputAction.CallbackContext context)
    {
        if (DialogManager.dialogManager != null && DialogManager.dialogManager.talking)
        {
            moveInput = Vector2.zero;
            return;
        }

        moveInput = context.ReadValue<Vector2>();
        if (addAudio == null) addAudio = FindFirstObjectByType<AddAudio>();
        if (addAudio != null) addAudio.OnWalkSound();
    }

    private void OnMoveStopped(InputAction.CallbackContext context)
    {
        moveInput = Vector2.zero;
        if (addAudio != null) addAudio.StopWalk();
    }

    private void OnJumpGlideInput(InputAction.CallbackContext context)
    {
        if (DialogManager.dialogManager != null && DialogManager.dialogManager.talking) return; //blocks jumping while reading dialogue

        rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, jumpSpeed);//---- applies upward force for jump when spacebar is pressed---- CF
        if (addAudio == null) addAudio = FindFirstObjectByType<AddAudio>();
        if (addAudio != null) addAudio.OnWingFlap();

        isGliding = true;// ----marks gliding state true as long as spacebar is held down---- CF
    }

    private void OnJumpGlideStopped(InputAction.CallbackContext context)
    {

        isGliding = false;// ----  reverts to standard gravity force when space bar released ---- CF
        if (addAudio != null) addAudio.StopWingFlap();

    }

    private void OnFireBreathInput(InputAction.CallbackContext context)
    {
        flameOn = true;
        ToggleFireBreath();
        if (addAudio == null) addAudio = FindFirstObjectByType<AddAudio>();
        if (addAudio != null) addAudio.OnFireBreath();

    }

    private void OnFireBreathStopped(InputAction.CallbackContext context)
    {
        flameOn = false;
        ToggleFireBreath();
        if (addAudio != null) addAudio.StopFireBreath();
    }

    private void MovePlayer()
    {
        rb2d.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb2d.linearVelocity.y);// ---- adds gravity to movement  to apply it naturally and ensure ground contact---- CF
        if (moveInput.x > 0 && !isFacingRight) // ---- checks the direction of motion and ensures player sprite parent is facing the direction of motion----CF
        {
            FlipCharacter();
        }
        else if (moveInput.x < 0 && isFacingRight)// -----else flips it the other way if moving the other direction ---- CF
        {
            FlipCharacter();
        }

    }

    private void HandleGlide()
    {


        if (isGliding && rb2d.linearVelocity.y < 0)// ----checks for glide and  then applies the glide slow if falling ---- CF
        {
            rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, glideSpeed);
        }
    }

    private void ToggleFireBreath()
    {
        if (jangleFlame != null)//---- verifies the flameobject exists ---- CF
        {
            jangleFlame.SetActive(flameOn); // ---- Brings the Fwoooooshhhhhhhhhhh---- CF
        }
    }
    public void SetFireBreath(bool active)
    {
        flameOn = active;
        ToggleFireBreath();
    }

    private void FlipCharacter()
    {
        isFacingRight = !isFacingRight;//---- checks facing ----CF
        Vector3 currentScale = transform.localScale; // ----selects the parent's scale value asvariable to adjust----CF
        currentScale.x *= -1; // ---- flips sprite packet parrnt through neg x to turn it over / flip direction---- CF
        transform.localScale = currentScale; // ---- sets new scale transform as currnet value---- CF
    }

    private IInteractable currentInteractable;
    private float lastInteractTime = -1f;

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed && currentInteractable != null)
        {
            TriggerCurrentInteractable();
        }
    }

    
    private void TriggerCurrentInteractable()
    {
        if (Time.unscaledTime - lastInteractTime < 0.1f) return;
        lastInteractTime = Time.unscaledTime;

        if (interactPromptUI != null) interactPromptUI.SetActive(false); // hide prompt once dialogue opens
        currentInteractable.Interact();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Interactable"))
        {
          
            currentInteractable = collision.GetComponent<IInteractable>()
                ?? collision.GetComponentInParent<IInteractable>()
                ?? collision.GetComponentInChildren<IInteractable>();

            if (currentInteractable != null && interactPromptUI != null)
            {
                interactPromptUI.SetActive(true); //  popup when Jangle enters NPC range
            }
        }
        if (collision.gameObject.CompareTag("PassiveInteractable"))
        {
            IInteractable passive = collision.GetComponent<IInteractable>()
                ?? collision.GetComponentInParent<IInteractable>()
                ?? collision.GetComponentInChildren<IInteractable>();
            passive?.Interact();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Interactable"))
        {
            IInteractable exiting = collision.GetComponent<IInteractable>()
                ?? collision.GetComponentInParent<IInteractable>()
                ?? collision.GetComponentInChildren<IInteractable>();

            if (currentInteractable == exiting)
            {
                currentInteractable = null;
                if (interactPromptUI != null)
                {
                    interactPromptUI.SetActive(false); // hides the popup when Jangle walks away
                }
            }
        }
    }

}