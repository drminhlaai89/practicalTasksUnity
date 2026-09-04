using UnityEngine;
using Random = UnityEngine.Random;

namespace Interview.Task3
{
    public class Stickman : MonoBehaviour
    {
        [SerializeField] private PlayerHealth _health;
        [SerializeField] private float _maxHp = 100f;

        [SerializeField] private float _minDamage;
        [SerializeField] private float _maxDamage;

        private void Start()
        {
            _health.Initialize(_maxHp);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                Interact();
        }

        private void Interact()
        {
            if (!_health.Dead)
                DealDamage();
            else
                Restore();
        }

        private void DealDamage()
        {
            var damage = Random.Range(_minDamage, _maxDamage);
            _health.TakeDamage(damage);
        }

        private void Restore()
        {
            _health.Restore();
        }
    }
}