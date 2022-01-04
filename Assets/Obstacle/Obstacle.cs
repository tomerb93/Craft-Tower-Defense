using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    
    public bool CreateObstacle(Obstacle obstacle, Vector3 position)
    {
        Bank bank = FindObjectOfType<Bank>();

        if (bank == null)
        {
            return false;
        }

        if (bank.CurrentObstacleCount > 0)
        {
            bank.DecreaseObstacleCount(1);
            Instantiate(obstacle, position, Quaternion.identity);
            return true;
        }

        return false;
    }
}
