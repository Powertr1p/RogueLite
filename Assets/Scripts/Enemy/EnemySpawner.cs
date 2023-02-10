using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Transform _player;
        [SerializeField] private UnityEngine.Camera _camera;
        [SerializeField] private EnemyBase _enemyPrefab;
        
        [Header("Spawn Parameters")]
        [SerializeField] private int _enemyLimit = 10;
        [SerializeField] private float _cooldown = 1f;
        
        [Header("Round Spawner Parameters")]
        [SerializeField] private float _radius = 10f;
        [SerializeField] private int _spawnAmount = 50;

        [Header("Regular Spawner")] 
        [RangeAttribute(1.1f, 2f)]
        [SerializeField] private float _minSpawnDistance = 1.1f;
        [RangeAttribute(1.5f, 3f)]
        [SerializeField] private float _maxSpawnDistance = 2f;

        private float _timeUntilNextSpawn;

        private const float MinPosition = -1f;
        private const float MaxPosition = 1f;

        private void Start()
        {
            SetupNewCooldownTimer();
        }

        private void Update()
        {
            if (IsCooldownEnded())
            {
                Spawn();
                SetupNewCooldownTimer();
            }
        }

        private void RoundSpawn()
        {
            float nextAngle = 2 * MathF.PI / _spawnAmount;
            float angle = 0;

            for (int i = 0; i < _spawnAmount; i++)
            {
                float x = Mathf.Cos(angle) * _radius;
                float y = Mathf.Sin(angle) * _radius;

                var instance = Instantiate(_enemyPrefab, _player.transform.position + new Vector3(x, y, 0), Quaternion.identity);
                instance.Initialize(_player);
                
                angle += nextAngle;
            }
        }

        private void Spawn()
        {
            for (int i = 0; i < _spawnAmount; i++)
            {
                Vector2 spawnPosition = GenerateSpawnPosition();
                
                var instance = Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);
                instance.Initialize(_player);
            }
        }

        private Vector2 GenerateSpawnPosition()
        {
            Vector3 finalPosition;

            do
            {
                var positionOutsideCamera = GetRandomPositionOutsideCamera();
                finalPosition = PositionWithRandomOffset(positionOutsideCamera);

            } while (IsInCameraView(finalPosition));

            return finalPosition;
        }

        private Vector3 GetRandomPositionOutsideCamera()
        {
            return _camera.ViewportToWorldPoint(new Vector3(Random.Range(1.1f, 2f), Random.Range(1.1f, 2f), _camera.nearClipPlane));
        }

        private Vector2 PositionWithRandomOffset(Vector2 position)
        {
            position.x = GetRandomOffset(position.x);
            position.y = GetRandomOffset(position.y);

            return position;
        }

        private float GetRandomOffset(float position)
        {
            position *= GetRandomScreenSide();
            position *= Random.Range(MinPosition, MaxPosition);

            return position;
        }

        private int GetRandomScreenSide()
        {
            return Random.Range(MinPosition, MaxPosition) < 0 ? -1 : 1;
        }
        
        private bool IsInCameraView(Vector3 position)
        {
            Vector3 viewPos = _camera.WorldToViewportPoint(position);
            return (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0);
        }

        private bool IsCooldownEnded()
        {
            return Time.time >= _timeUntilNextSpawn;
        }

        private void SetupNewCooldownTimer()
        {
            _timeUntilNextSpawn = Time.time + _cooldown;
        }
    }
}
