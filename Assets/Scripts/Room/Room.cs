using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;

    private Vector3[] initialPosition;
    private int numEnemies;

    private void Awake()
    {
        numEnemies = enemies.Length;
        initialPosition = new Vector3[numEnemies];

        for (int i = 0; i < numEnemies; i++)
        {
            if (enemies[i] != null)
            {
                initialPosition[i] = enemies[i].transform.position;
            }
        }
    }

    public void ActivateEnemiesRoom(bool _status)
    {
        for (int i = 0; i < numEnemies; i++)
        {
            if (enemies[i] != null)
            {
                enemies[i].SetActive(_status);
                enemies[i].transform.position = initialPosition[i];
            }
        }
    }
}
