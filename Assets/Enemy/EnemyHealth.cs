using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float startingHitpoints = 3;

    float currentHitPoints;
    Enemy enemy;

    void OnEnable()
    {
        currentHitPoints = startingHitpoints;
    }

    void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit(other.GetComponentInParent<Weapon>());
    }

    void ProcessHit(Weapon weapon)
    {
        currentHitPoints -= weapon.Damage;

        if (currentHitPoints <= 0)
        {
            ProcessDeath();
        }
    }

    private void ProcessDeath()
    {
        Destroy(gameObject);
        enemy.RewardBalance();
        StopAllCoroutines();
    }
}
