using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Interview.Task3
{
    public class HitFeedbackManager : MonoBehaviour
    {
        [SerializeField] private PlayerHealth _health;
        [SerializeField] private Animator _animator;

        [Header("UI Feedback")]
        [SerializeField] private NumbersSpawner _numbersSpawner;
        [SerializeField] private Image _hitFlashPanel;

        [Header("VFX Prefabs (Project Folder)")]
        [SerializeField] private ParticleSystem _impactVFXPrefab;
        [SerializeField] private ParticleSystem[] _bloodParticlePrefabs;

        private Camera _mainCamera;

        private void Awake()
        {
            _mainCamera = Camera.main;
        }

        private void OnEnable()
        {
            _health.OnDamage += HandleHitFeedback;
        }

        private void OnDisable()
        {
            _health.OnDamage -= HandleHitFeedback;
        }

        private void HandleHitFeedback(float damage)
        {
            // 1. Camera Shake
            _mainCamera.DOComplete();
            _mainCamera.DOShakePosition(0.2f, strength: 0.3f, vibrato: 10);

            // 2. Character GetHit Animation
            if (_animator != null)
            {
                _animator.SetTrigger("GetHit");
            }

            // 3. Spawn Impact VFX Prefab tại vị trí ngực nhân vật
            Vector3 spawnPos = _health.transform.position + Vector3.up * 1.0f;
            if (_impactVFXPrefab != null)
            {
                ParticleSystem vfx = Instantiate(_impactVFXPrefab, spawnPos, Quaternion.identity);
                Destroy(vfx.gameObject, vfx.main.duration + vfx.main.startLifetime.constantMax);
            }

            // 4. Spawn ngẫu nhiên 1 Blood Splat Prefab
            SpawnRandomBloodPrefab(spawnPos);

            // 5. Screen Hit Flash
            if (_hitFlashPanel != null)
            {
                _hitFlashPanel.DOKill();
                _hitFlashPanel.color = new Color(1f, 0f, 0f, 0.35f);
                _hitFlashPanel.DOFade(0f, 0.2f);
            }

            // 6. Floating Damage Numbers
            if (_numbersSpawner != null)
            {
                _numbersSpawner.SpawnDamageText(damage, _health.transform.position);
            }
        }

        private void SpawnRandomBloodPrefab(Vector3 spawnPos)
        {
            if (_bloodParticlePrefabs != null && _bloodParticlePrefabs.Length > 0)
            {
                int randomIndex = Random.Range(0, _bloodParticlePrefabs.Length);
                ParticleSystem prefab = _bloodParticlePrefabs[randomIndex];

                if (prefab != null)
                {
                    ParticleSystem blood = Instantiate(prefab, spawnPos, Quaternion.identity);
                    Destroy(blood.gameObject, blood.main.duration + blood.main.startLifetime.constantMax);
                }
            }
        }
    }
}