using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    [SerializeField] GameObject fruit;
    [SerializeField] int maxPosX, maxPosY;

    private void Start()
    {
        SpawnNewFruit();
    }

    public void SpawnNewFruit()
    {
        int posX = (int)Random.Range(0, maxPosX);
        int posY = (int)Random.Range(0, maxPosY);

        if(ClearedToSpawn(posX, posY))
        {
            Instantiate(fruit, new Vector2(posX, posY), Quaternion.identity);
        }
        else
        {
            SpawnNewFruit();
        }        
    }

    bool ClearedToSpawn(int posX, int posY)
    {
        bool isCleared = true;
        foreach (GameObject segment in GameObject.FindGameObjectsWithTag("Snake Body"))
        {
            if(segment != null)
            {
                if (posX == (int)segment.transform.position.x &&
                posY == (int)segment.transform.position.y)
                {
                    isCleared = false;
                    break;
                }
                isCleared = true;
            }          
        }
        return isCleared;
    }
}
