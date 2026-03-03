using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		[SerializeField]
		private Vector2 move;
        [SerializeField]
        private Vector2 look;
        [SerializeField]
        private bool jump;
        [SerializeField]
        private bool sprint;
        [SerializeField]
        private bool dodge;
		[SerializeField]
		private bool attack;

        [Header("Movement Settings")]
        [SerializeField]
        private bool analogMovement;

		[Header("Mouse Cursor Settings")]
        [SerializeField]
        private bool cursorLocked = true;
        [SerializeField]
        public bool cursorInputForLook = true;

#if ENABLE_INPUT_SYSTEM
		public void OnMove(InputAction.CallbackContext value)
		{
			MoveInput(value.ReadValue<Vector2>());
		}

		public void OnLook(InputAction.CallbackContext value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.ReadValue<Vector2>());
			}
		}

		public void OnJump(InputAction.CallbackContext value)
		{
			JumpInput(value.action.triggered);
		}

		public void OnSprint(InputAction.CallbackContext value)
		{
			SprintInput(value.action.ReadValue<float>() == 1);
		}

		public void OnDodge(InputAction.CallbackContext value)
		{
			DodgeInput(value.action.triggered);
		}

		public void OnAttack(InputAction.CallbackContext value)
		{
			AttackInput(value.started);
		}
#endif

		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}
        public void DodgeInput(bool newDodgeState)
        {
            dodge = newDodgeState;
        }

        public void AttackInput(bool newAttackState)
        {
            attack = newAttackState;
        }

		public Vector2 GetMove()
		{
			return move;
		}

		public Vector2 GetLook()
		{
			return look;
		}

		public bool IsJumping()
		{
			return jump;
		}

		public bool IsSprinting()
		{
			return sprint;
		}

		public bool IsDodging()
		{
			return dodge;
		}
		public bool IsAttacking() 
		{
			return attack; 
		}

		public bool IsAnalog()
		{
			return analogMovement;
		}

        private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}

		
	}
	
}