using System;
using Components;
using DG.Tweening;
using JetBrains.Annotations;
using Loot;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(EnemyMover))]
    public class EnemyBase : MonoBehaviour
    {
        [SerializeField] private EnemyMover _mover;
        [SerializeField] private Health _health;
        [SerializeField] private SpriteRenderer _sprite;
        [SerializeField] private EnemyType _type;
        
        [Header("Лут")]
        [SerializeField] private LootType _loot;
        [SerializeField] private int _dropChance;

        public event Action<EnemyBase> Died;
        
        public new EnemyType GetType => _type;
        public LootType GetLootType => _loot;
        public int DropChance => _dropChance;
        
        private Transform _lootTransform;
        private Tween _tweenColor;

        private void OnEnable()
        {
            _health.Died += OnDied;
            _health.DamageTaken += OnTakeDamage;
        }

        private void OnDisable()
        {
            _health.Died -= OnDied;
            _health.DamageTaken -= OnTakeDamage;
        }

        private void OnDied()
        {
            _tweenColor.Kill();

            Died?.Invoke(this);

            if (!ReferenceEquals(_lootTransform, null))
            {
                _lootTransform.SetParent(null);
                _lootTransform.gameObject.SetActive(true);
            }
            
            Destroy(gameObject);
        }

        public void Initialize(Transform player, LootBase loot)
        {
            _mover.Initialize(player);
            _lootTransform = loot.transform;
            
            BindLoot(loot);
        }

        public void Initialize(Transform player)
        {
            _mover.Initialize(player);
        }

        private void BindLoot(LootBase loot)
        {
            _lootTransform.SetParent(transform);
            _lootTransform.localPosition = Vector3.zero;
            _lootTransform.gameObject.SetActive(false);
        }

        private void OnTakeDamage()
        {
            if (_sprite.color != Color.white)
                _sprite.color = Color.white;
            
            _tweenColor = _sprite.DOColor(Color.red, 0.1f).OnComplete(() =>
            {
                _sprite.DOColor(Color.white, 0.1f);
            });
        }
    }
}