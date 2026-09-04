using System.Collections.Generic;
using UnityEngine;

namespace Interview.Task3
{
    public class NumbersSpawner : MonoBehaviour
    {
        [SerializeField] private DamageText _damageTextPrefab;
        [SerializeField] private float _randomOffset = 0.5f;

        private readonly Queue<DamageText> _pool = new Queue<DamageText>();

        public void SpawnDamageText(float damage, Vector3 spawnPosition)
        {
            // Thêm random position offset
            Vector3 randomOffset = Random.insideUnitSphere * _randomOffset;
            randomOffset.z = 0; // Giữ nguyên Z nếu là UI/2D plane
            Vector3 finalPos = spawnPosition + Vector3.up * 1.5f + randomOffset;

            DamageText item = GetFromPool();
            item.transform.position = finalPos;
            item.gameObject.SetActive(true);

            // Setup animation và truyền callback trả về Pool khi hoàn thành
            item.Setup(damage, ReturnToPool);
        }

        private DamageText GetFromPool()
        {
            if (_pool.Count > 0)
                return _pool.Dequeue();

            return Instantiate(_damageTextPrefab, transform);
        }

        private void ReturnToPool(DamageText item)
        {
            item.gameObject.SetActive(false);
            _pool.Enqueue(item);
        }
    }
}
