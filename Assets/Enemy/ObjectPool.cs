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

    void Start()
    {
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
        Debug.Log("Wave Completed...");
        // TODO: Alert player he's won and reward
        if (nextWave + 1 > waves.Length - 1)
        {
            Debug.Log("All waves complete, looping..");
            nextWave = 0;
        }
        else
        {
            nextWave++;
        }

        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;
    }

    IEnumerator SpawnWave(Wave wave)
    {
        Debug.Log("Spawning wave..");
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
        Debug.Log("Spawning enemy..");

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
