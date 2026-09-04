using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Interview.Task3
{
    public class HpBar : MonoBehaviour
    {
        [SerializeField] private PlayerHealth _health;
        [SerializeField] private Image _fill;
        [SerializeField] private Image _damageFill;

        private void Start()
        {
            _health.OnDamage += OnDamage;
            _health.OnHeal += OnHeal;

            transform.forward = Camera.main.transform.forward;
        }

        private void OnDamage(float damage)
        {
            Refresh();
        }

        private void OnHeal(float heal)
        {
            Refresh();
        }

        private void Refresh()
        {
            float targetFill = _health.Hp / _health.MaxHp;

            _fill.DOFillAmount(targetFill, 0.15f).SetEase(Ease.OutQuad);

            if (_damageFill != null)
            {
                _damageFill.DOFillAmount(targetFill, 0.5f)
                           .SetDelay(0.1f)
                           .SetEase(Ease.OutQuad);
            }
        }
    }
}