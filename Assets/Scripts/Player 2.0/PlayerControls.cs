using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour, IPlayer, IDataPersistence
{
    private PlayerInputs playerInputs;

    public float aimInput;
    
    public float movementInput;
    
    public bool jumpHeld { get; private set; }
    
    public event Action JumpEvent;

    public event Action AttackEvent;

    public event Action GrappleEvent;

    private void Start()
    {
        playerInputs = new PlayerInputs();

        playerInputs.InGame.Attack.performed += Attack;

        playerInputs.InGame.Jump.performed += JumpHeld;
        playerInputs.InGame.Jump.canceled += JumpLetGo;

        playerInputs.InGame.Grapple.performed += Grapple;
        
        playerInputs.Enable();
    }

    private void Attack(InputAction.CallbackContext context)
    {
        AttackEvent?.Invoke();
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

    private void Grapple(InputAction.CallbackContext context)
    {
        GrappleEvent?.Invoke();
    }

    private void FixedUpdate()
    {
        movementInput = playerInputs.InGame.MovementInput.ReadValue<float>();
        aimInput = playerInputs.InGame.AimInput.ReadValue<float>();
    }

    public void OnDisable()
    {
        playerInputs.InGame.Jump.performed -= JumpHeld;
        playerInputs.InGame.Jump.canceled -= JumpLetGo;
    }

    public void LoadData(GameData data)
    {
        transform.position = data.playerPos;
    }

    public void SaveData(ref GameData data)
    {
        data.playerPos = transform.position;
    }
}