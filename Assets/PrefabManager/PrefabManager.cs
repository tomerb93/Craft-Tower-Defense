using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    public enum PrefabIndices
    {
        Tower,
        Obstacle,
        TowerWeapon1,
        TowerWeapon2,
    }

    [SerializeField] Prefab[] prefabs;

    public GameObject GetPrefab(PrefabIndices index)
    {
        return prefabs[(int)index].prefab;
    }

    public Vector3 GetPrefabPosition(PrefabIndices index)
    {
        return prefabs[(int) index].position;
    }
}
