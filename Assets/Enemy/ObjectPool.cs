using System.Collections;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] [Range(0, 20)] int poolSize = 5;
    [SerializeField] float spawnTimer = 1f;
    [SerializeField] float startTimer = 5f;

    GameObject[] pool;

    void Awake()
    {
        PopulatePool();
    }


    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    void Update()
    {
        if(startTimer > 0)
        {
            startTimer -= Time.deltaTime;
        }
    }

    void PopulatePool()
    {
        pool = new GameObject[poolSize];

        for (int i = 0; i < pool.Length; i++)
        {
            pool[i] = Instantiate(enemyPrefab, transform);
            pool[i].SetActive(false);
        }
    }

    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(startTimer);
        while (true)
        {
            EnableInactiveObjectsInPool();
            yield return new WaitForSeconds(spawnTimer);
        }
    }

    void EnableInactiveObjectsInPool()
    {
        for (int i = 0; i < pool.Length; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                pool[i].SetActive(true);
                return;
            }
        }
    }
}
