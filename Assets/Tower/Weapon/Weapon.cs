using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float Damage { get { return damage; } }
    public float Speed { get { return speed; } }
    public float Range { get { return range; } }
    public string Name { get { return weaponName; } }
    public float Slow { get { return slow; } }
    public float SlowDuration { get { return slowDuration; } }
    public bool HasSlow { get { return slow > 0; } }

    [SerializeField] float damage = 0.5f;
    [SerializeField] float speed = 4f;
    [SerializeField] float range = 15f;
    [SerializeField] string weaponName = "Weapon";
    [SerializeField] float slow = 0.1f;
    [SerializeField] float slowDuration = 1f;


    ParticleSystem bulletParticleSystem;

    void Awake()
    {
        bulletParticleSystem = GetComponentInChildren<ParticleSystem>();
    }
     
    void Start()
    {
        //SetParticleSystemProperties();
    }

    void SetParticleSystemProperties()
    {
        var emission = bulletParticleSystem.emission;
        emission.rateOverTime = speed;
    }

    public void UpgradeDamage(float amount)
    {
        damage += amount;
    }

    public void UpgradeSpeed(float amount)
    {
        //speed += amount;
        //SetParticleSystemProperties();

    }

    public void UpgradeSlow(float amount)
    {
        slow += amount;

        if (slow > 0.8f)
        {
            slow = 0.8f;
        }
    }
}