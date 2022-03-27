using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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
        
        // Instantiate tower base
        var tower = Instantiate(prefabManager.GetPrefab(PrefabManager.PrefabIndices.Tower), position, Quaternion.identity).GetComponent<Tower>();


        var weaponToInstantiate = PrefabManager.PrefabIndices.TowerWeapon2;

        // Instantiate starting tower weapon
        Instantiate(prefabManager.GetPrefab(weaponToInstantiate),
            position + prefabManager.GetPrefabPosition(weaponToInstantiate),
            Quaternion.identity, tower.transform);

        return tower;
    }
}
    