using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

#region coder & project
/// <summary>
/// GAME2065/4087/Game Development III(B)/Cameron,Jordan
/// Dragon Crumbs: Jangle's SweetJourney  GameJam3  Percy Hebb, Chris French, Kevin Card, Myranda Roy
/// Coder current script: Chris French Second Year NSCC Game Programming 
/// Additions / annotations: Fixed floating jump bugs, established clear gravity transitions.
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

    private PlayerControls inputActions;
    private Vector2 moveInput;
    private Rigidbody2D rb2d;

    private bool isGliding = false;
    private bool flameOn = false;
    private bool isFacingRight = false;

    private void Awake()
    {
        inputActions = new PlayerControls();
        rb2d = GetComponent<Rigidbody2D>();

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
    }

    private void OnDisable()
    {
        inputActions.Player.Move.performed -= OnMoveInput;
        inputActions.Player.Move.canceled -= OnMoveStopped;

        inputActions.Player.JumpGlide.performed -= OnJumpGlideInput;
        inputActions.Player.JumpGlide.canceled -= OnJumpGlideStopped;

        inputActions.Player.FireBreath.performed -= OnFireBreathInput;
        inputActions.Player.FireBreath.canceled -= OnFireBreathStopped;

        inputActions.Player.Disable();
    }

    private void FixedUpdate()
    {
        MovePlayer();
        HandleGlide();

    }

    private void OnMoveInput(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void OnMoveStopped(InputAction.CallbackContext context)
    {
        moveInput = Vector2.zero;
    }

    private void OnJumpGlideInput(InputAction.CallbackContext context)
    {

        rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, jumpSpeed);//---- applies upward force for jump when spacebar is pressed---- CF


        isGliding = true;// ----marks gliding state true as long as spacebar is held down---- CF
    }

    private void OnJumpGlideStopped(InputAction.CallbackContext context)
    {

        isGliding = false;// ----  reverts to standard gravity force when space bar released ---- CF
    }

    private void OnFireBreathInput(InputAction.CallbackContext context)
    {
        flameOn = true;
        ToggleFireBreath();
    }

    private void OnFireBreathStopped(InputAction.CallbackContext context)
    {
        flameOn = false;
        ToggleFireBreath();
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

    private void FlipCharacter()
    {
        isFacingRight = !isFacingRight;//---- checks facing ----CF
        Vector3 currentScale = transform.localScale; // ----selects the parent's scale value asvariable to adjust----CF
        currentScale.x *= -1; // ---- flips sprite packet parrnt through neg x to turn it over / flip direction---- CF
        transform.localScale = currentScale; // ---- sets new scale transform as currnet value---- CF
    }
    
}


