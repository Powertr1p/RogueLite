using UnityEngine;

namespace PowerTrip
{
    public class UIRoot : Singleton<UIRoot>
    {
        #region Fields
        [Header("Component Links:")]
        [SerializeField] private Camera _camera;
        [SerializeField] private UI_DamageLabels _damageText;
        #endregion

        #region Accessors
        public Camera Camera { get => _camera; }
        public UI_DamageLabels DamageText { get => _damageText; }
        #endregion
    }
}

