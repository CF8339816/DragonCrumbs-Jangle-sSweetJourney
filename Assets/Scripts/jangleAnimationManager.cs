using UnityEngine;
using UnityEngine.InputSystem;

public class jangleAnimationManager : MonoBehaviour
{

    public Vector2 direction;
    [SerializeField] private Animator animator;
    [SerializeField] private Collider2D col;
    private int directionHash = Animator.StringToHash("Direction");
    private int groundedHash = Animator.StringToHash("Grounded");
    private int movingHash = Animator.StringToHash("Moving");
    private int flapTriggerHash = Animator.StringToHash("Jumping");
    private int gildingHash = Animator.StringToHash("Gliding");
    private int fireBreathHash = Animator.StringToHash("Breathing fire");

    private void Start()
    {
        //if animatior is null then checks children
        if(animator == null && gameObject.GetComponent<Animator>())
        {
            animator = gameObject.GetComponent<Animator>();
        }
        else if (animator == null && GetComponentInChildren<Animator>())
        {
            animator = GetComponentInChildren<Animator>();
        }
        //gets collider from parrent if missing
        if(col == null)
        {
            col = GetComponent<Collider2D>();
        }
    }
    //pass input contect or vector 2 to handle movement animations
    public void OnMove(InputAction.CallbackContext context) 
    {
        direction = context.ReadValue<Vector2>();
    }
    public void OnMove(Vector2 context)
    {
        direction = context;
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
        SetGrounded(Physics2D.Raycast(transform.position, -Vector3.up, col.bounds.extents.y + 0.0001f));
        
    }
    //triggers jump animation
    public void OnFlap()
    {
        animator.SetTrigger(flapTriggerHash);
    }
    //sets if fire animation should play or not
    public void SetFireBreath(bool breathingFire)
    {
        animator.SetBool(fireBreathHash, breathingFire);
    }
    //sets if grounded
    public void SetGrounded(bool grounded)
    {
        animator.SetBool(groundedHash, grounded);
        if(grounded == true)
        {
            SetGliding(false);
        }
    }
    //sets if glide animation should play or not
    public void SetGliding(bool gliding)
    {
        animator.SetBool(gildingHash, gliding);
    }
}
