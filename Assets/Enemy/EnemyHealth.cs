using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float startingHitpoints = 3f;
    [SerializeField] float armor;
    [SerializeField] HealthBar healthBar;

    [SerializeField] ParticleSystem deathVFX;

    float currentHitPoints;
    bool isDotted;
    float currentDotDamage;

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
            if (!isDotted)
            {
                StartCoroutine(ProcessDotDamage(weapon));
            }
            else if (isDotted && weapon.DamageOverTime > currentDotDamage)
            {
                StopAllCoroutines();
                StartCoroutine(ProcessDotDamage(weapon));
            }
                
        }
        else
        {
            TakeDamage(weapon.Damage, weapon.Pierce);
        }
    }

    void TakeDamage(float weaponDamage, float weaponPierce, bool isDot = false)
    {
        float damageTaken = isDot ? weaponDamage : weaponDamage - armor * (1f - weaponPierce);
        currentHitPoints -= damageTaken > 0f ? damageTaken : 0f;
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

        currentDotDamage = weapon.DamageOverTime;

        for (int i = 0; i < weapon.DamageOverTimeDuration; i++)
        {
            TakeDamage(weapon.DamageOverTime, weapon.Pierce, true);

            yield return new WaitForSeconds(weapon.DamageOverTimer);
        }

        healthBar.SetColor(Color.red);
        isDotted = false;
    }

    void ProcessDeath()
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