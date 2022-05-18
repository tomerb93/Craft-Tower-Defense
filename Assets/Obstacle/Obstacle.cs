using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public bool CreateObstacle(Vector3 position)
    {
        var bank = FindObjectOfType<Bank>();
        var prefabManager = FindObjectOfType<PrefabManager>();

        if (bank == null || prefabManager == null)
        {
            return false;
        }

        if (bank.DecreaseObstacleCount(1))
        {
            Instantiate(prefabManager.GetPrefab(PrefabManager.PrefabIndices.Obstacle), position, Quaternion.identity);
            return true;
        }

        return false;
    }
}