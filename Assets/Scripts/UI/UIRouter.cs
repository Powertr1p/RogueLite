using UnityEngine;

namespace PowerTrip
{
    public static class UIRouter
    {
        #region Fields
        private static Camera _camera;
        #endregion

        // Damage Text
        // ——————————————————————————————————————————————————

        public static void SetDamageTextState(bool state)
        {
            if (GetInstance() is null) return;

            UIRoot.Instance.DamageText.SetState(state);
        }

        public static void ShowDamageText(Vector3 position, float value)
        {
            if (GetInstance() is null) return;

            Vector3 screenPosition = GetScreenPosition(position);

            UIRoot.Instance.DamageText.ShowDamageText(screenPosition, value);
        }

        // Helpers
        // ——————————————————————————————————————————————————

        private static UIRoot GetInstance()
        {
            return UIRoot.Instance;
        }

        private static Vector3 GetScreenPosition(Vector3 worldPosition)
        {
            _camera = GetInstance().Camera;

            return _camera.WorldToScreenPoint(worldPosition);
        }
    }
}
