using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int cost = 50;

    public Tower CreateTower(Vector3 position)
    {
        var bank = FindObjectOfType<Bank>();
        
        if (bank == null)
        {
            return null;
        }

        return bank.WithdrawBalance(cost) ? InstantiateTower(position) : null;
    }

    private Tower InstantiateTower(Vector3 position)
    {
        var prefabManager = FindObjectOfType<PrefabManager>();

        if (prefabManager == null)
        {
            return null;
        }

        var offset = new Vector3(0, 5f, 0);
        // Instantiate tower base
        var tower = Instantiate(prefabManager.GetPrefab(PrefabManager.PrefabIndices.Tower), position, Quaternion.identity).GetComponent<Tower>();
        Instantiate(prefabManager.GetPrefab(PrefabManager.PrefabIndices.TowerWeapon),
            position + prefabManager.GetPrefabPosition(PrefabManager.PrefabIndices.TowerWeapon),
            Quaternion.identity, tower.transform);

        return tower;
    }
}
