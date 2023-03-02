using UnityEngine;

namespace PowerTrip
{
    public class EnemySpriteAngleRotator : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _sprite;
        [SerializeField] private EnemyMover _mover;
        [SerializeField] private float _rotationSpeed = 5f;

        private readonly float _maxAngle = 90f;
        
        private Transform _transform;
        private Vector3 _previousPosition;

        private void Awake()
        {
            _transform = transform;
        }

        private void LateUpdate()
        {
            var direction = _mover.Target.position - transform.position;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            var targetRotation = Quaternion.Euler(0f, 0f, angle);
            
            _sprite.flipY = direction.normalized.x < 0 || direction.normalized.x > 0 && angle > _maxAngle;
            
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }
    }
}
    