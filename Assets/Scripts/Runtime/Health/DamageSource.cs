using UnityEngine;

namespace Runtime.Health
{
    public class DamageSource : MonoBehaviour
    {
        [SerializeField] private int damage = 1;

        private void OnTriggerEnter(Collider other)
        {
            IHealth health = other.GetComponent<IHealth>();

            if (health == null)
                health = other.GetComponentInParent<IHealth>();

            health?.Damage(damage);
        }
    }
}