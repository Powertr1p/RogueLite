using UnityEngine;

namespace Player
{
    public class PlayerInput : MonoBehaviour
    {
        public Vector3 Axises => _inputAxises;
        public bool IsInputActive => _isInputActive;
        
        private Vector3 _inputAxises;
        private bool _isInputActive;

        private void Update()
        {
            _inputAxises = new Vector3(Input.GetAxisRaw("Horizontal") , Input.GetAxisRaw("Vertical"), 0);
            _isInputActive = _inputAxises.x != 0 || _inputAxises.y != 0;
        }
    }
}
