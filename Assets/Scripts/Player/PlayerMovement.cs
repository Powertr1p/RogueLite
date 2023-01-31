using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _speed = 2f;

        private PlayerInput _input;
        private int _direction;
        private Vector3 _lastPosition;

        private void Awake()
        {
            _input = GetComponent<PlayerInput>();
        }

        private void Update()
        {
            transform.Translate(_input.Axises * (_speed * Time.deltaTime));
        }
    }
}