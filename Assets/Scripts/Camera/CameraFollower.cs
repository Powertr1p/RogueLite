using UnityEngine;

namespace PowerTrip
{
    public class CameraFollower : MonoBehaviour
    {
        [SerializeField] private float _dampTime = 10f;
        [SerializeField] private Transform _target;
        [SerializeField] private Vector2 _offset;

        private float _margin = 0.1f;

        private void Update()
        {
            var position = _target.position;
            
            var targetX = position.x + _offset.x;
            var targetY = position.y + _offset.y;

            if (Mathf.Abs(transform.position.x - targetX) > _margin)
                targetX = Mathf.Lerp(transform.position.x, targetX, 1 / _dampTime * Time.deltaTime);

            if (Mathf.Abs(transform.position.y - targetY) > _margin)
                targetY = Mathf.Lerp(transform.position.y, targetY, _dampTime * Time.deltaTime);

            transform.position = new Vector3(targetX, targetY, transform.position.z);
        }
    }
}
