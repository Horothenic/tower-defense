using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using Zenject;
using DG.Tweening;
using Utilities.Cameras;

namespace TowerDefense.Towers
{
    [RequireComponent(typeof(TowerEnemyDetector))]
    public class MegaTowerShooter : MonoBehaviour
    {
        #region FIELDS

        [Inject] private CameraShake cameraShake = null;

        [Header("CONFIGURATIONS")]
        [SerializeField] private Image cooldownBar = null;

        [Header("CONFIGURATIONS")]
        [SerializeField] private LaserDamager megaLaser = null;
        [SerializeField] private Collider megaLaserCollider = null;
        [SerializeField] private float duration = 5;
        [SerializeField] private float interval = 0.5f;
        [SerializeField] private float damage = 10;
        [SerializeField] private float cooldown = 10;

        [Header("TWEENING")]
        [SerializeField] private float preShootSizeFactor = 1.2f;
        [SerializeField] private float preShootSizeDuration = 0.3f;

        private TowerEnemyDetector towerEnemyDetector = null;
        private bool shotEnable = false;
        private Coroutine shotCoroutine = null;
        private Sequence tweenSequence = null;

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            towerEnemyDetector = GetComponent<TowerEnemyDetector>();
        }

        private void OnDestroy()
        {
            tweenSequence?.Kill();
        }

        private void Start()
        {
            megaLaser.SetDamage(damage);
            StartCooldown();
        }

        private void Update()
        {
            if (shotCoroutine == null || towerEnemyDetector.HasEnemies)
                return;

            StopCoroutine(shotCoroutine);
            Stop();
        }

        public void Shoot()
        {
            if (!shotEnable)
                return;

            ShootTween();
        }

        private void ShootTween()
        {
            tweenSequence = DOTween.Sequence();
            var originalScale = transform.localScale;
            tweenSequence.Append(transform.DOScale(originalScale * preShootSizeFactor, preShootSizeDuration));
            tweenSequence.AppendCallback(OnShootTweenEnd);
            tweenSequence.Append(transform.DOScale(originalScale, preShootSizeDuration));
        }

        private void OnShootTweenEnd()
        {
            cameraShake.MediumShake();
            shotEnable = false;
            shotCoroutine = StartCoroutine(ShootCoroutine());
        }

        private IEnumerator ShootCoroutine()
        {
            megaLaser.gameObject.SetActive(true);

            var attacks = duration / interval;
            var wait = new WaitForSeconds(interval);

            for (int i = 0; i < attacks; i++)
            {
                megaLaserCollider.enabled = true;
                yield return wait;
                megaLaserCollider.enabled = false;
                yield return null;
            }

            Stop();
        }

        private void Stop()
        {
            megaLaser.gameObject.SetActive(false);
            StartCooldown();
        }

        private void EnableShot()
        {
            shotEnable = true;
        }

        private void StartCooldown()
        {
            Invoke(nameof(EnableShot), cooldown);
            TweenCooldownBar();
        }

        private void TweenCooldownBar()
        {
            DOTween.To(() => cooldownBar.fillAmount, x => cooldownBar.fillAmount = x, 1, cooldown).From(0).SetEase(Ease.Linear);
        }

        #endregion
    }
}
