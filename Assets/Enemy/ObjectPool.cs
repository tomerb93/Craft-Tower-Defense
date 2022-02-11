using System;
using System.Collections;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public enum SpawnState {  SPAWNING, WAITING, COUNTING };

    [SerializeField] float waveCountdown;
    [SerializeField] float timeBetweenWaves = 5f;
    [SerializeField] Wave[] waves;

    int nextWave;
    SpawnState state = SpawnState.COUNTING;
    float searchCountdown = 1f;

    AlertController alert;

    void Awake()
    {
        alert = FindObjectOfType<AlertController>();
    }

    void Start()
    {
        alert.Alert("Next wave spawning in 5 seconds", 20, true);
        waveCountdown = timeBetweenWaves;
    }

    void Update()
    {
        if (state == SpawnState.WAITING)
        {
            if (!EnemyIsAlive())
            {
                // Begin a new round
                WaveCompleted();
            }
            else
            {
                return;
            }
        }

        if (waveCountdown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        } else
        {

            waveCountdown -= Time.deltaTime;
        }
    }

    void WaveCompleted()
    {
        alert.Alert("Wave completed", 20, true);
        if (nextWave + 1 > waves.Length - 1)
        {
            alert.Alert("All waves complete, increasing difficulty...", 20, true);
            foreach (Wave wave in waves)
            {
                wave.count *= 2;
                wave.rate *= 1.2f;
            }
            nextWave = 0;
        }
        else
        {
            nextWave++;
        }

        alert.Alert("Next wave spawning in 5 seconds", 20, true);
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;
    }

    IEnumerator SpawnWave(Wave wave)
    {
        alert.Alert("Spawning wave!", 20, true);

        state = SpawnState.SPAWNING;

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);

            yield return new WaitForSeconds(1f / wave.rate);
        }

        state = SpawnState.WAITING;
    }

    void SpawnEnemy(Enemy enemy)
    {
        Instantiate(enemy, transform);
    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;

        // Search every 1 s, FindObjectOfType is heavy function
        // (iterates all objects)
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (FindObjectOfType<Enemy>() == null)
            {
                return false;
            }
        }
        
        return true;
    }
}
