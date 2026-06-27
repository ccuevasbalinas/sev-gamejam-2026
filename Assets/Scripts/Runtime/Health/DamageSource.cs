using UnityEngine;

namespace Runtime.Health
{
    public class DamageSource : MonoBehaviour
    {
        [SerializeField] private int damage = 1;

        private void OnTriggerEnter(Collider other)
        {
            IHealthService health = other.GetComponent<IHealthService>();

            if (health == null)
                health = other.GetComponentInParent<IHealthService>();

            health?.Damage(damage);
        }
    }
}