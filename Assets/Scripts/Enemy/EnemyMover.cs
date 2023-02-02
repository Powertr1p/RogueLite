using UnityEngine;

namespace Enemy
{
    public class EnemyMover : MonoBehaviour
    {
        [SerializeField] private float _speed = 2f;
        [SerializeField] private float _randomnessOffset = 5f;
        private Transform _target;
        private Transform _transform;

        private bool _isInitialized;

        private void Awake()
        {
            _transform = transform;
        }

        private void Update()
        {
            if (!_isInitialized) return;
            
            float xOffset = Random.Range(-_randomnessOffset, _randomnessOffset);
            float yOffset = Random.Range(-_randomnessOffset, _randomnessOffset);
            Vector3 targetPosition = _target.position + new Vector3(xOffset, yOffset, 0);
            
            _transform.position = Vector3.MoveTowards(_transform.position, targetPosition, _speed * Time.deltaTime);
        }

        public void Initialize(Transform target)
        {
            _target = target;
            _isInitialized = true;
        }
    }
}