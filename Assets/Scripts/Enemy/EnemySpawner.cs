using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Transform _player;
        [SerializeField] private UnityEngine.Camera _camera;
        [SerializeField] private EnemyFactory _factory;
        
        [Header("Параметры спавна врагов")]
        [Tooltip("Максимальный лимит врагов. Общее количество врагов не будет превышать это число.")]
        [SerializeField] private int _enemyLimit = 10;
        [Tooltip("Промежуток времени, через который будут спавнится враги")]
        [SerializeField] private float _cooldown = 1f;
        [Tooltip("Количество врагов, которое будет спавнится в каждой волне")]
        [SerializeField] private int _spawnRate = 50;
        
        [Header("Round Spawner Parameters")]
        [Tooltip("Радиус спавна врагов по кругу. Чем больше радиус, тем больше круг")]
        [SerializeField] private float _radius = 10f;

        [Header("Regular Spawner")] 
        [RangeAttribute(1.1f, 2f)]
        [Tooltip("Минимальное расстояние спавна врагов от камеры. 1 - это уже внутри камеры")]
        [SerializeField] private float _minSpawnDistance = 1.1f;
        [RangeAttribute(1.5f, 3f)]
        [Tooltip("Максимальное расстояние спавна врагов от камеры")]
        [SerializeField] private float _maxSpawnDistance = 2f;

        [Header("Переключатель спавнеров")]
        [Tooltip("Если включить эту опцию, то следующая волна будет круговой. После кругового спавна, опция автоматически выключится")]
        [SerializeField] private bool _isRound;

        private List<EnemyBase> _activeEnemies = new List<EnemyBase>();
        
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
                SpawnEnemies();
                SetupNewCooldownTimer();
            }
        }

        private void SpawnEnemies()
        {
            if (_isRound)
                RoundSpawn();
            else
                OutsideCameraSpawn();
        }

        private void RoundSpawn()
        {
            float nextAngle = 2 * MathF.PI / _spawnRate;
            float angle = 0;

            for (int i = 0; i < _spawnRate; i++)
            {
                float x = Mathf.Cos(angle) * _radius;
                float y = Mathf.Sin(angle) * _radius;

                var instance = _factory.GetEnemy(EnemyType.SimpleEnemy, _player);
                instance.transform.position = _player.transform.position + new Vector3(x, y, 0);

                angle += nextAngle;
            }

            _isRound = false;
        }

        private void OutsideCameraSpawn()
        {
            int spawnAmount = CalculateSpawnAmount();
            
            for (int i = 0; i < spawnAmount; i++)
            {
                Vector2 spawnPosition = GenerateSpawnPosition();
                var instance = _factory.GetEnemy(EnemyType.SimpleEnemy, _player);
                instance.transform.position = spawnPosition;

                _activeEnemies.Add(instance);
            }
        }

        private int CalculateSpawnAmount()
        {
            var amountRemains = _enemyLimit - _activeEnemies.Count;
            
            return amountRemains - _spawnRate >= 0 ? _spawnRate : amountRemains;
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
