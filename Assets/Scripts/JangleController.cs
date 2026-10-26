using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
#region coder & project
/// <summary>
/// GAME2065/4087/Game Development III(B)/Cameron,Jordan
/// Dragon Crumbs: Jangle's SweetJourney  GameJam3  Percy Hebb, Chris French, Kevin Card, Myranda Roy
/// Coder current script: Chris French Second Year NSCC Game Programming 
/// Additions / annotations:
/// 
/// </summary>
#endregion   

[RequireComponent(typeof(Rigidbody2D))]
public class JangleController : MonoBehaviour
{  
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpSpeed = 8f;
    [SerializeField] private float glideSpeed = -1f;
    [SerializeField] private float Gravity = -9.8f;

    [Header("References")]
    [SerializeField] private GameObject jangleFlameProxy; // set firebreath object

    private PlayerControls inputActions;
    private Vector2 moveInput;
    private Rigidbody2D rb2d;

    private bool isGliding = false;
    private bool flameOn = false;

    private void Awake()
    {
        inputActions = new PlayerControls();
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();

           
        inputActions.Player.Move.performed += OnMoveInput; // WASD Movement
        inputActions.Player.Move.canceled += OnMoveStopped;

           
        inputActions.Player.JumpGlide.performed += OnJumpGlideInput; // Spacebar Jump & Glide
        inputActions.Player.JumpGlide.canceled += OnJumpGlideStopped;

         
        inputActions.Player.FireBreath.performed += OnFireBreathInput;   // Shift Fire Breath
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

    private void Update()
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
           
        rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, jumpSpeed ); // does a quick jump upwards when space pressed
        isGliding = true;        
    }

    private void OnJumpGlideStopped(InputAction.CallbackContext context)
    {
        rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, -Gravity);
        isGliding = false;
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
          
        rb2d.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb2d.linearVelocity.y);
    }

    private void HandleGlide()
    {
           
        if (isGliding && rb2d.linearVelocity.y < 0)
        {
            rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, glideSpeed );// causes a slow glide if space held while falling allows for jump and for glide
        }
    }

    private void ToggleFireBreath()
    {
        if (jangleFlameProxy != null)
        {
            jangleFlameProxy.SetActive(flameOn);
        }
    }
}

