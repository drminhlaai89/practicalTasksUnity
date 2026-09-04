using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Interview.Task3
{
    public class DamageText : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private float _duration = 0.8f;
        [SerializeField] private float _moveYAmount = 1.5f;

        private Sequence _sequence;

        public void Setup(float damageAmount, System.Action<DamageText> onComplete)
        {
            _text.text = $"-{damageAmount:F0}";

            if (Camera.main != null)
            {
                transform.forward = Camera.main.transform.forward;
            }

            Color c = _text.color;
            c.a = 1f;
            _text.color = c;
            transform.localScale = Vector3.one * 1.5f;

            _sequence?.Kill();

            _sequence = DOTween.Sequence();
            _sequence.Append(transform.DOScale(2.0f, 0.15f).SetEase(Ease.OutBack))
                     .Join(transform.DOMoveY(transform.position.y + _moveYAmount, _duration).SetEase(Ease.OutQuad))
                     .Insert(_duration * 0.5f, _text.DOFade(0f, _duration * 0.5f))
                     .OnComplete(() => onComplete?.Invoke(this));
        }

        private void OnDestroy()
        {
            _sequence?.Kill();
        }
    }
}
