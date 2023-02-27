using System;
using UnityEngine;

using DG.Tweening;

namespace PowerTrip
{
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(EnemyMover))]

    public class EnemyBase : MonoBehaviour
    {
        #region Events
        public event Action<EnemyBase> OnDeath;
        #endregion

        #region Fields
        [SerializeField] protected EnemyType _type;

        [SerializeField] protected Health _health;
        [SerializeField] protected EnemyDamager _damager;
        [SerializeField] protected EnemyMover _mover;
        [SerializeField] protected SpriteRenderer _sprite;
        [SerializeField] protected Transform _blood;

        [Header("Лут")]
        [SerializeField] protected LootType _loot;
        [SerializeField] protected int _dropChance;

        private Transform _lootTransform;
        private Tween _tweenColor;
        #endregion

        #region Accessors
        public EnemyType Type => _type;
        public LootType LootType => _loot;

        public int DropChance => _dropChance;
        #endregion

        private void OnEnable()
        {
            _health.OnDeath += HandleDeath;
            _health.OnDamageTaken += HandleDamageTaken;
        }

        private void OnDisable()
        {
            _health.OnDeath -= HandleDeath;
            _health.OnDamageTaken -= HandleDamageTaken;
        }

        public void Initialize(Player player, LootBase loot)
        {
            _mover.Initialize(player.transform);
            _damager.Initialize(player);

            _lootTransform = loot.transform;
            
            BindLoot(loot);
        }

        public void Initialize(Player player)
        {
            _mover.Initialize(player.transform);
            _damager.Initialize(player);
        }

        private void BindLoot(LootBase loot)
        {
            _lootTransform.SetParent(transform);
            _lootTransform.localPosition = Vector3.zero;
            _lootTransform.gameObject.SetActive(false);
        }

        private void HandleDamageTaken()
        {
            if (_sprite.color != Color.white)
            {
                _sprite.color = Color.white;
            }

            _tweenColor?.Kill();
            _tweenColor = _sprite.DOColor(Color.red, 0.1f).OnComplete(() =>
            {
                _tweenColor?.Kill();
                _tweenColor = _sprite.DOColor(Color.white, 0.1f);
            });
        }

        private void HandleDeath()
        {
            _tweenColor?.Kill();

            OnDeath?.Invoke(this);

            if (_lootTransform != null)
            {
                _lootTransform.SetParent(null);
                _lootTransform.gameObject.SetActive(true);
            }
            
            _blood.SetParent(null);
            _blood.gameObject.SetActive(true);

            Destroy(gameObject);
        }
    }
}