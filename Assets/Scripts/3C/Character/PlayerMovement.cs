using Player;
using UnityEngine;

namespace _3C.Character
{
    public class PlayerMovement : MonoBehaviour
    {
        #region Fields

        [Header("Movement")]
        [SerializeField] private bool canMove = true;
        [SerializeField, Range(0.0f, 15.0f)] private float walkSpeed = 6.0f;
        private float verticalSpeed;
        
        [Header("Rotation")]
        [SerializeField] private bool canRotate = true;
        [SerializeField, Range(0.0f, 20.0f)] private float rotateSpeed = 10.0f;

        [Header("Sprint")]
        [SerializeField] private bool canSprint = true;
        [SerializeField] private bool isSprinting = false;
        [SerializeField, Range(15.0f, 30.0f)] private float sprintSpeed = 15.0f;

        [Header("Animation")]
        [SerializeField] private float dampTime = 0.2f;
        
        [Header("Pointers")]
        [SerializeField] private Player owner = null;

        #endregion
        
        void Update()
        {
            if (!owner) return;
            Inputs _inputs = owner.Inputs;
            
            if (canMove)
            {
                MoveVertical(Input.GetAxis(_inputs.Vertical));
                MoveHorizontal(Input.GetAxis(_inputs.Horizontal));
            }

            if (canRotate)
            { 
                MoveYaw(Input.GetAxis(_inputs.Yaw));
                //MovePitch(Input.GetAxis(owner.Inputs.Pitch));
            }

            string _sprintInput = _inputs.Sprint;
            if (canSprint && Input.GetButton(_sprintInput))
            {
                if (!isSprinting && Input.GetButtonDown(_sprintInput))
                {
                    SetSprintStatus(true);
                }
                
                else if (!isSprinting || Input.GetButtonUp(_sprintInput) || verticalSpeed < 0.1f)
                {
                    SetSprintStatus(false);
                }
            }
        }

        #region Movement

        private void MoveVertical(float _value)
        {
            float _speed = _value * (isSprinting ? sprintSpeed : walkSpeed);
            transform.position += transform.forward * (_speed * Time.deltaTime);
            verticalSpeed = _speed;
            owner.Animator.SetFloat(Animator.StringToHash("Vertical"), _speed, dampTime, Time.deltaTime);
        }
        
        private void MoveHorizontal(float _value)
        {
            MoveYaw(_value);
            owner.Animator.SetFloat(Animator.StringToHash("Horizontal"), _value * rotateSpeed, dampTime, Time.deltaTime);
        }        

        private void SetSprintStatus(bool _status)
        {
            if (_status.Equals(isSprinting)) return;
            isSprinting = _status;
            owner.Animator.SetBool(Animator.StringToHash("Sprint"), _status);
        }
        
        #endregion

        #region Rotation

        private void MoveYaw(float _value)
        {
            transform.eulerAngles += transform.up * (_value * rotateSpeed * 10.0f * Time.deltaTime);
        }
        
        private void MovePitch(float _value)
        {
            transform.eulerAngles += transform.right * (_value * rotateSpeed * Time.deltaTime);
        }

        #endregion
    }
}
