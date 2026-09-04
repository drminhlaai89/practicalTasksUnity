using Lean.Pool;
using UnityEngine;

namespace Interview.Task2
{
    public class NumbersSpawner : MonoBehaviour
    {
        [SerializeField] private Number _prefab;
        [SerializeField] private Transform _spawnPoint;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                SpawnNumber();
        }

        private void SpawnNumber()
        {
            var number = LeanPool.Spawn(_prefab);
            number.transform.position = _spawnPoint.position;
            number.Animate();
        }
    }
}