using UnityEngine;

namespace PowerTrip
{
    public class PlayerMovement : MonoBehaviour
    {
        #region Fields
        [SerializeField] private float _speed = 2f;

        private Transform _t;

        private bool _isEnabled = false;
        #endregion

        private void Awake()
        {
            _t = transform;
        }

        public void SetState(bool state)
        {
            _isEnabled = state;
        }

        public void UpdateMovement(Vector3 input)
        {
            if (_isEnabled is false) return;

            Vector3 movement = new Vector3(input.x, input.y, 0f).normalized;

            _t.Translate(movement * (_speed * Time.deltaTime));
        }
    }
}