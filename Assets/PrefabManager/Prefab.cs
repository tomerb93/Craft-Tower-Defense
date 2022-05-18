using UnityEngine;

[System.Serializable]
public class Prefab
{
    public GameObject prefab;
    public Vector3 position;

    public Prefab(GameObject prefab, Vector3 position)
    {
        this.prefab = prefab;
        this.position = position;
    }
}