using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour, IPlayer
{
    private PlayerInputs playerInputs;
    
    public float movementInput;
    
    public bool jumpHeld { get; private set; }

    private void Start()
    {
        playerInputs = new PlayerInputs();

        playerInputs.InGame.Jump.performed += JumpHeld;
        playerInputs.InGame.Jump.canceled += JumpLetGo;
        
        playerInputs.Enable();
    }

    private void JumpHeld(InputAction.CallbackContext context)
    {
        jumpHeld = true;
        JumpEvent?.Invoke();
    }

    private void JumpLetGo(InputAction.CallbackContext context)
    {
        jumpHeld = false;
    }
    
    public event Action JumpEvent;

    private void FixedUpdate()
    {
        movementInput = playerInputs.InGame.MovementInput.ReadValue<float>();
    }

    public void OnDisable()
    {
        playerInputs.InGame.Jump.performed -= JumpHeld;
        playerInputs.InGame.Jump.canceled -= JumpLetGo;
    }
}