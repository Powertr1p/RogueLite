using DG.Tweening;
using UnityEngine;

namespace DefaultNamespace
{
    public class BloodAnimation : MonoBehaviour
    {
        public void Start()
        {
            var euler = transform. eulerAngles;
            euler.z = Random. Range(0.0f, 360.0f);
            transform. eulerAngles = euler;
            
            var targetValue = Random.Range(0.5f, 0.8f);
            transform.DOScale(new Vector3(targetValue, targetValue, targetValue), 0.25f).SetEase(Ease.InOutQuad)
                .OnComplete(
                    () =>
                    {
                        gameObject.isStatic = true;
                    });
        }
    }
}