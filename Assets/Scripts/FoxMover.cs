using UnityEngine;
using UnityEngine.InputSystem;

public class FoxMover : MonoBehaviour
{

    public Animator aniFoxCtlr;
    public Vector2 moveInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        aniFoxCtlr = GetComponent<Animator>();

    }

    private int MoveInputXHash = Animator.StringToHash("MoveInputX");
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        aniFoxCtlr.SetFloat(MoveInputXHash, moveInput.x);

    }







}
