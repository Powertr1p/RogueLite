using UnityEngine;

namespace PowerTrip
{
    public class AnimationHandler : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private PlayerInput _input;
        
        private readonly int _isWalking = Animator.StringToHash("IsWalking");

        private void Update()
        {
            if (_animator.GetBool(_isWalking) != _input.IsInputActive)
                _animator.SetBool(_isWalking, _input.IsInputActive);
        }
    }
}