using System.Threading;
using _3C.Character.Statics;
using UnityEngine;

namespace _3C.Character
{
    public class PlayerMovement : MonoBehaviour
    {
        #region Fields

        [Header("Movement values")]
        [SerializeField] private bool canMove = true;
        [SerializeField] private bool canSprint = true;
        [SerializeField, Range(0.0f, 1.0f)] private float minimalSpeedForSprinting = 0.8f;
        [SerializeField, Range(0.0f, 20.0f)] private float rotateSpeed = 10.0f;
        [SerializeField, Range(0.0f, 5.0f)] private float dampTime = 0.2f;
        [SerializeField] private Animator animator = null;
        
        [Header("SlowFactor")]
        [SerializeField, Range(0.0f, 100.0f)] private float startSlowAtPercent = 50.0f;
        
        private float slowFactor = 1.0f;
        private bool isSprinting = false;

        #endregion
        
        private void Update()
        {
            if (!animator || !canMove) return;
            
            MoveVertical(Input.GetAxis(Inputs.Vertical));
            MoveHorizontal(Input.GetAxis(Inputs.Horizontal));
            
            string _sprintInput = Inputs.Sprint;
            if (canSprint && Input.GetButton(_sprintInput))
            {
                if (!isSprinting && Input.GetButtonDown(_sprintInput))
                {
                    SetSprintStatus(true);
                }
                
                else if (isSprinting || Input.GetButtonUp(_sprintInput))
                {
                    SetSprintStatus(false);
                }
            }
        }

        private void MoveVertical(float _value)
        {
            if (_value < minimalSpeedForSprinting)
            {
                SetSprintStatus(false);
            }
            
            animator.SetFloat(Animations.VERTICAL, _value * slowFactor, dampTime, Time.deltaTime);
        }
        
        private void MoveHorizontal(float _value)
        {
            transform.eulerAngles += transform.up * (_value * rotateSpeed * 5.0f * Time.deltaTime);
            animator.SetFloat(Animations.HORIZONTAL, _value, dampTime, Time.deltaTime);
        }        

        private void SetSprintStatus(bool _status)
        {
            if (_status.Equals(isSprinting)) return;
            isSprinting = _status;
            animator.SetBool(Animations.SPRINT, _status);
        }

        public void SetCanMove(bool _status)
        {
            canMove = _status;
            animator.SetFloat(Animations.VERTICAL, 0.0f);
            animator.SetFloat(Animations.HORIZONTAL, 0.0f);
            SetSprintStatus(false);
        }

        public void ApplySlowFactor(float _health)
        {
            slowFactor = Mathf.Clamp(_health / startSlowAtPercent, 0.0f, 1.0f);
        }
    }
}