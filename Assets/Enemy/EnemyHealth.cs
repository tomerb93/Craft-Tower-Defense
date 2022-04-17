using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float startingHitpoints = 3;
    [SerializeField] HealthBar healthBar;

    [SerializeField] ParticleSystem deathVFX;

    float currentHitPoints;
    bool isDotted = false;

    void Start()
    {
        currentHitPoints = startingHitpoints;
        healthBar.SetMaxHealth(startingHitpoints);
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit(other.GetComponentInParent<Weapon>());
    }

    void ProcessHit(Weapon weapon)
    {
        if (weapon.HasDOT)
        {
            StartCoroutine(ProcessDotDamage(weapon));
        }
        else
        {
            TakeDamage(weapon.Damage);
        }
    }

    void TakeDamage(float weaponDamage)
    {
        currentHitPoints -= weaponDamage;
        healthBar.SetHealth(currentHitPoints);
        healthBar.SetColor(isDotted ? Color.green : Color.red);
        if (currentHitPoints <= 0)
        {
            ProcessDeath();
        }
    }

    IEnumerator ProcessDotDamage(Weapon weapon)
    {
        isDotted = true;

        for (int i = 0; i < 500; i++)
        {
            TakeDamage(weapon.DamageOverTime);

            yield return new WaitForSeconds(weapon.DamageOverTimer);
        }
        
        isDotted = false;
    }

    private void ProcessDeath()
    {
        var enemy = GetComponent<Enemy>();

        if (!deathVFX.isPlaying)
        {
            deathVFX.Play();
        }
        enemy.RewardBalance();
        StopAllCoroutines();
        GetComponentInChildren<MeshRenderer>().enabled = false;
        GetComponentInChildren<Canvas>().enabled = false;
        enemy.IsDead = true;
        Invoke("DestroyGameObject", deathVFX.main.duration);
    }

    void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
