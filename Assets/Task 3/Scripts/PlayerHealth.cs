using System;
using UnityEngine;

namespace Interview.Task3
{
    public class PlayerHealth : MonoBehaviour
    {
        public Action<float> OnDamage;
        public Action<float> OnHeal;

        public float MaxHp { get; private set; }
        public float Hp { get; private set; }

        public bool Dead => Hp <= 0;

        public void Initialize(float maxHp)
        {
            MaxHp = maxHp;
            Hp = MaxHp;
        }

        public void TakeDamage(float amount)
        {
            Hp -= amount;
            OnDamage?.Invoke(amount);
        }

        public void Heal(float amount)
        {
            Hp += amount;
            if (Hp > MaxHp)
                Hp = MaxHp;

            OnHeal?.Invoke(amount);
        }

        public void Restore()
        {
            Heal(MaxHp - Hp);
        }
    }
}