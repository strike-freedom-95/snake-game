using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    [SerializeField] GameObject fruit;

    private void Start()
    {
        SpawnNewFruit();
    }

    public void SpawnNewFruit()
    {
        Instantiate(fruit, 
            new Vector2((int)Random.Range(-7, 7), (int)Random.Range(-5, 5)), 
            Quaternion.identity);
    }
}
