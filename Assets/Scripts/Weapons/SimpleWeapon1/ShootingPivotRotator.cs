using UnityEngine;

namespace PowerTrip
{
    public class ShootingPivotRotator : MonoBehaviour
    {
        private Transform _transform;
        public Vector3 Direction => transform.right;

        private float _angle;
        private float _lastAngle;

        private void Awake()
        {
            _transform = transform;
        }

        private void Update()
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");

            if (verticalInput == 0 && horizontalInput == 0) return;
            
            _angle = Mathf.Atan2(verticalInput, horizontalInput) * Mathf.Rad2Deg;

            _transform.rotation = Quaternion.Euler(0f, 0f, _angle);
            _lastAngle = _angle;
        }
    }
}