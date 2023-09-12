using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Oscar
{
    /// <summary>
    /// This will become the generic PlayerControls script.
    /// Its modular enough to have multiple scripts use this script for their controls.
    /// </summary>

    public class PlayerControls : MonoBehaviour, IPlayer, IDataPersistence
    {
        private PlayerInputs playerInputs;

        /// <summary>
        /// these variables will need to be deleted soon
        /// </summary>
        #region Dont Need Variables
        
        public float aimInput;
        public float movementInput;
        public bool jumpHeld { get; private set; }
        
        #endregion
        
        public event Action JumpEvent;
        public event Action JumpLetGoEvent;

        public event Action AttackEvent;

        public event Action GrappleEvent;

        public event Action<float> MovementInputEvent;
        public event Action<float> AimInputEvent;

        private void Start()
        {
            playerInputs = new PlayerInputs();
            
            playerInputs.InGame.MovementInput.performed += MovementInputOnperformed;
            playerInputs.InGame.MovementInput.canceled += MovementInputOnperformed;
            
            playerInputs.InGame.AimInput.performed += AimInputOnPerformedAndCanceled;
            playerInputs.InGame.AimInput.canceled += AimInputOnPerformedAndCanceled;
            
            playerInputs.InGame.Attack.performed += Attack;

            playerInputs.InGame.Jump.performed += JumpHeld;
            playerInputs.InGame.Jump.canceled += JumpLetGo;

            playerInputs.InGame.Grapple.performed += Grapple;
            
            playerInputs.Enable();
        }

        private void AimInputOnPerformedAndCanceled(InputAction.CallbackContext obj)
        {
            AimInputEvent?.Invoke(obj.ReadValue<float>());
            print(obj.ReadValue<float>());
        }

        private void MovementInputOnperformed(InputAction.CallbackContext obj)
        {
            MovementInputEvent?.Invoke(obj.ReadValue<float>());
        }

        private void Attack(InputAction.CallbackContext context)
        {
            AttackEvent?.Invoke();
        }
        
        private void JumpHeld(InputAction.CallbackContext context)
        {
            JumpEvent?.Invoke();
        }

        private void JumpLetGo(InputAction.CallbackContext context)
        {
            JumpLetGoEvent?.Invoke();
        }

        private void Grapple(InputAction.CallbackContext context)
        {
            GrappleEvent?.Invoke();
        }

        /// <summary>
        /// will need to be removed.
        /// </summary>
        // private void FixedUpdate()
        // {
        //     movementInput = playerInputs.InGame.MovementInput.ReadValue<float>();
        //     aimInput = playerInputs.InGame.AimInput.ReadValue<float>();
        // }

        public void OnDisable()
        {
            playerInputs.InGame.MovementInput.performed -= MovementInputOnperformed;
            playerInputs.InGame.MovementInput.canceled -= MovementInputOnperformed;
            
            playerInputs.InGame.AimInput.performed -= AimInputOnPerformedAndCanceled;
            playerInputs.InGame.AimInput.canceled -= AimInputOnPerformedAndCanceled;

            playerInputs.InGame.Attack.performed -= Attack;

            playerInputs.InGame.Jump.performed -= JumpHeld;
            playerInputs.InGame.Jump.canceled -= JumpLetGo;

            playerInputs.InGame.Grapple.performed -= Grapple;
        }

        /// <SavingAndLoading>
        /// Saving data will be moved into the model script or alternatively a saving character script that may need
        /// to be created per character.
        /// </SavingAndLoading>
        public void LoadData(GameData data)
        {
            transform.position = data.playerPos;
        }

        public void SaveData(ref GameData data)
        {
            data.playerPos = transform.position;
        }
    }
}
