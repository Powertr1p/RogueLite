using UnityEngine;

namespace PowerTrip
{
    public class PlayerInput : MonoBehaviour
    {
        #region Fields
        private Vector2 _direction;

        private bool _isEnabled = false;
        private bool _isInputActive = false;
        #endregion

        #region Accessors
        public Vector2 Direction { get => _direction; }
        public bool IsInputActive { get => _isInputActive; }
        #endregion

        private void Update()
        {
            if (_isEnabled is false)
            {
                _direction.x = 0f;
                _direction.y = 0f;

                _isInputActive = false;

                return;
            }

            _direction.x = Input.GetAxisRaw("Horizontal");
            _direction.y = Input.GetAxisRaw("Vertical");

            _isInputActive = _direction.x != 0 || _direction.y != 0;
        }

        public void SetState(bool state)
        {
            _isEnabled = state;
        }
    }
}
