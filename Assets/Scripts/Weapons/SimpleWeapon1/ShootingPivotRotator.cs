using UnityEngine;

namespace PowerTrip
{
    public class ShootingPivotRotator : MonoBehaviour
    {
        private Transform _transform;
        public Vector3 Direction => transform.right;

        private void Awake()
        {
            _transform = transform;
        }

        private void Update()
        {
            float angle = Mathf.Atan2(Input.GetAxisRaw("Vertical"), Input.GetAxisRaw("Horizontal")) * Mathf.Rad2Deg;
            _transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }
}