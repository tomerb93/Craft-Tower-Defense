using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float Damage { get { return damage; } }
    public float Speed { get { return speed; } }
    public float Range { get { return range; } }
    public float Pierce { get { return pierce; } }
    public string Name { get { return weaponName; } }
    public float Slow { get { return slow; } }
    public float SlowDuration { get { return slowDuration; } }
    public bool HasSlow { get { return slow > 0; } }
    public float DamageOverTime { get { return damageOverTime; } }
    public bool HasDOT { get { return damageOverTime > 0; } }
    public float DamageOverTimer { get { return damageOverTimer; } }
    public int DamageOverTimeDuration { get { return damageOverTimeDuration; } }
    public string Info { get { return info; } }

    public string Stats { get { return $"Damage: {damage}, DoT: {damageOverTime}, Speed: {speed}, Slow: {slow}"; } }


    [SerializeField] float damage = 0.5f;
    [SerializeField] float damageOverTime = 0f;
    [SerializeField] float damageOverTimer = 0.5f;
    [SerializeField] int damageOverTimeDuration = 2;
    [SerializeField] float pierce = 0f;
    [SerializeField] float speed = 4f;
    [SerializeField] float range = 15f;
    [SerializeField] string weaponName = "Weapon";
    [SerializeField] string info = "";
    [SerializeField] float slow = 0.1f;
    [SerializeField] float slowDuration = 1f;


    ParticleSystem bulletParticleSystem;

    void Awake()
    {
        bulletParticleSystem = GetComponentInChildren<ParticleSystem>();
    }
     
    void Start()
    {
        SetParticleSystemProperties();
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
        speed += amount;
        SetParticleSystemProperties();

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
