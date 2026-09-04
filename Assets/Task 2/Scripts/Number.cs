using TMPro;
using UnityEngine;
using Lean.Pool;

namespace Interview.Task2
{
    public class Number : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;

        public void Animate()
        {
            // Додайте анімацію вильоту тексту сюди

            LeanPool.Despawn(this, 1f);
        }
    }
}