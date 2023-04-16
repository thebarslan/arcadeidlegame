using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Speeds")]
        [SerializeField] private float playerMovementSpeed;
        [SerializeField] private float playerRotationSpeed;
        [Space(10)]
        [SerializeField] private FloatingJoystick _joystick;
        [SerializeField] private Animator _animator;
    
    
        private void FixedUpdate()
        {
            AnimationHandle();
            MovementHandle();
            RotationHandle();
        }

        private void AnimationHandle()
        {
            if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
            {
                _animator.SetBool("isWalking", true);
            }
            else
            {
                _animator.SetBool("isWalking", false);
            }
        }
        private void MovementHandle()
        {
            transform.position = transform.position + new Vector3(_joystick.Horizontal * playerMovementSpeed * Time.deltaTime, 0f, _joystick.Vertical * playerMovementSpeed * Time.deltaTime);
        }
        private void RotationHandle()
        {
            Vector3 movementDirection = new Vector3(_joystick.Horizontal, 0f, _joystick.Vertical);
            movementDirection.Normalize();
        
            if(movementDirection != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, playerRotationSpeed * Time.deltaTime);
            }
        }
    }
}
