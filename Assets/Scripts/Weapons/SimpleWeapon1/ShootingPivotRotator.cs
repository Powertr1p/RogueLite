using UnityEngine;

namespace PowerTrip
{
    public class ShootingPivotRotator : MonoBehaviour
    {
        private Transform _transform;
        public Vector3 Direction => transform.right;

        private float _lastAngle;

        private void Awake()
        {
            _transform = transform;
        }

        private void Update()
        {
            float angle = Mathf.Atan2(Input.GetAxisRaw("Vertical"), Input.GetAxisRaw("Horizontal")) * Mathf.Rad2Deg;

            if (angle == 0)
                angle = _lastAngle;
            
            _transform.rotation = Quaternion.Euler(0f, 0f, angle);
            _lastAngle = angle;
        }
    }
}