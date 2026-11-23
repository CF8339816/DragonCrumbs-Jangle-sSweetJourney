using UnityEngine;
using UnityEngine.InputSystem;

public class jangleAnimationManager : MonoBehaviour
{

    public Vector2 direction;
    [SerializeField] private Animator animator;
    private int directionHash = Animator.StringToHash("Direction");
    private int groundedHash = Animator.StringToHash("Grounded");
    private int movingHash = Animator.StringToHash("Moving");
    private int flapTriggerHash = Animator.StringToHash("Jumping");
    private int gildingHash = Animator.StringToHash("Gliding");
    private int fireBreathHash = Animator.StringToHash("Breathing");

    private void Start()
    {
        if(animator == null)
        {
            animator = gameObject.GetComponent<Animator>();
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
    }

    
    void Update()
    {
        if (direction != Vector2.zero)
        {
            animator.SetBool(movingHash, true);
            animator.SetFloat(directionHash, direction.x);
            

        }
        else
        {
            animator.SetBool(movingHash, false);
        }
    }

    public void OnFlap()
    {
        animator.SetTrigger(flapTriggerHash);
    }
    public void SetFireBreath(bool breathingFire)
    {
        animator.SetBool(fireBreathHash, breathingFire);
    }
    public void SetGrounded(bool grounded)
    {
        animator.SetBool(groundedHash, grounded);
    }
    public void SetGliding(bool gliding)
    {
        animator.SetBool(gildingHash, gliding);
    }
}
