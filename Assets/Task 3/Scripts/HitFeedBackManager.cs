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

        [Header("VFX Feedback")]
        [SerializeField] private ParticleSystem _impactVFX; // RoundHitBlue
        [SerializeField] private ParticleSystem[] _bloodParticles; // BloodSplat 1, 2, 3

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

            // 3. Impact VFX (RoundHitBlue)
            if (_impactVFX != null)
            {
                _impactVFX.Play();
            }

            // 4. Random Blood Splat VFX
            PlayRandomBlood();

            // 5. Screen Hit Flash (Red Overlay)
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

        private void PlayRandomBlood()
        {
            if (_bloodParticles != null && _bloodParticles.Length > 0)
            {
                int randomIndex = Random.Range(0, _bloodParticles.Length);
                if (_bloodParticles[randomIndex] != null)
                {
                    _bloodParticles[randomIndex].Play();
                }
            }
        }
    }
}