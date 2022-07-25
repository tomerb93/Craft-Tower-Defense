using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class Tower : MonoBehaviour
{
    [SerializeField] private int cost = 50;

    public Tower CreateTower(Vector3 position)
    {
        var bank = FindObjectOfType<Bank>();

        if (bank == null)
        {
            return null;
        }

        return bank.WithdrawBalance(cost) ? BuildTower(position) : null;
    }

    private Tower BuildTower(Vector3 position)
    {
        var prefabs = FindObjectOfType<PrefabManager>();
        // Instantiate tower base
        var tower = Instantiate(prefabs.GetPrefab(PrefabManager.PrefabIndices.Tower),
                position,
                Quaternion.identity)
            .GetComponent<Tower>();

        // Instantiate starting tower weapon
        SetTowerWeapon(PrefabManager.PrefabIndices.TowerWeapon1, tower, position);

        return tower;
    }

    public void SetTowerWeapon(PrefabManager.PrefabIndices weaponIndex, Tower tower, Vector3 position)
    {
        var prefabs = FindObjectOfType<PrefabManager>();

        var weapon = GetComponentInChildren<Weapon>();
        if (weapon != null)
        {
            Destroy(weapon.gameObject);
        }

        Instantiate(prefabs.GetPrefab(weaponIndex),
            position + prefabs.GetPrefabPosition(weaponIndex),
            Quaternion.identity, tower.transform).GetComponent<Weapon>();
    }
}