using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        #region Fields

        [SerializeField, Header("Movement")]
        private bool canMove = true;
        
        [SerializeField, Range(0.0f, 15.0f)]
        private float walkSpeed = 6.0f;
        
        [SerializeField, Header("Rotation")]
        private bool canRotate = true;
        
        [SerializeField, Range(0.0f, 20.0f)]
        private float rotateSpeed = 10.0f;

        [SerializeField, Header("Sprint")]
        private bool canSprint = true;
        
        [SerializeField]
        private bool isSprinting = false;

        [SerializeField, Range(15.0f, 30.0f)]
        private float sprintSpeed = 15.0f;
        
        [SerializeField, Header("Jump")]
        private bool canJump = true;

        [SerializeField, Range(0.0f, 25.0f)]
        private float jumpForce = 20.0f;

        [SerializeField, Header("Pointers")]
        private Player owner = null;

        #endregion
        
        void Update()
        {
            if (canMove)
            {
                MoveVertical(Input.GetAxis(owner.Inputs.Vertical));
                MoveHorizontal(Input.GetAxis(owner.Inputs.Horizontal));
            }

            if (canRotate)
            {
                MoveYaw(Input.GetAxis(owner.Inputs.Yaw));
                MovePitch(Input.GetAxis(owner.Inputs.Pitch));
            }
            
            if (canSprint && Input.GetButtonDown(owner.Inputs.Sprint))
            {
                ToggleSprint();
            }
            
            if (canJump && Input.GetButtonDown(owner.Inputs.Jump))
            {
                Jump();
            }
        }

        private void MoveVertical(float _value)
        {
            transform.position += transform.forward * _value * (isSprinting ? sprintSpeed : walkSpeed) * Time.deltaTime;
        }
        private void MoveHorizontal(float _value)
        {
            transform.position += transform.right * _value * (isSprinting ? sprintSpeed : walkSpeed) * Time.deltaTime;
        }
        private void MoveYaw(float _value)
        {
            transform.eulerAngles += transform.up * _value * rotateSpeed * Time.deltaTime;
        }
        private void MovePitch(float _value)
        {
            transform.eulerAngles += transform.right * _value * rotateSpeed * Time.deltaTime;
        }

        private void ToggleSprint()
        {
            isSprinting = !isSprinting;
        }
        private void Jump()
        {   
            // OSEF
        }
    }
}
