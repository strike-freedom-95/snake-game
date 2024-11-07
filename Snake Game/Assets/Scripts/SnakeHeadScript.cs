using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SnakeHeadScript : MonoBehaviour
{
    [SerializeField] ParticleSystem deathParticle;
    [SerializeField] Sprite deadSnake;
    [SerializeField] AudioClip eatSFX;
    [SerializeField] AudioClip deadSFX;
    [SerializeField] ParticleSystem eatFX;

    private void Start()
    {
        CheckFruit();
        CheckWalls();
        CheckSnakeBodyPosition();
        CheckSnakeHeadPosition();
    }

    private void CheckSnakeHeadPosition()
    {
        foreach (GameObject segment in GameObject.FindGameObjectsWithTag("AI Snake Head"))
        {
            if ((int)transform.position.x == (int)segment.transform.position.x &&
                (int)transform.position.y == (int)segment.transform.position.y)
            {
                SnakeDeathSequence();
            }
        }
    }

    private void CheckSnakeBodyPosition()
    {
        foreach (GameObject segment in GameObject.FindGameObjectsWithTag("Snake Body"))
        {
            if ((int)transform.position.x == (int)segment.transform.position.x &&
                (int)transform.position.y == (int)segment.transform.position.y)
            {
                SnakeDeathSequence();
            }
        }
    }

    private void CheckWalls()
    {
        foreach (GameObject wall in GameObject.FindGameObjectsWithTag("Wall"))
        {
            if ((int)transform.position.x == (int)wall.transform.position.x &&
                (int)transform.position.y == (int)wall.transform.position.y)
            {
                SnakeDeathSequence();
            }
        }
    }

    private void CheckFruit()
    {
        if (GameObject.FindGameObjectWithTag("Fruit") != null)
        {
            GameObject fruit = GameObject.FindGameObjectWithTag("Fruit");
            int fruitPosX = (int)fruit.transform.position.x;
            int fruitPosY = (int)fruit.transform.position.y;

            if ((int)transform.position.x == fruitPosX && (int)transform.position.y == fruitPosY)
            {
                FruitSequence(fruit);
            }
        }
    }

    private void FruitSequence(GameObject fruit)
    {
        FindObjectOfType<ScoreCounterScript>().IncreaseScore();
        Instantiate(eatFX, transform.position, Quaternion.identity);
        Destroy(fruit);
        AudioSource.PlayClipAtPoint(eatSFX, Camera.main.transform.position);
        FindObjectOfType<FruitSpawner>().SpawnNewFruit();
        FindObjectOfType<SnakeMovement>().FruitEaten();
    }

    private void SnakeDeathSequence()
    {        
        AudioSource.PlayClipAtPoint(deadSFX, Camera.main.transform.position);
        GetComponent<CinemachineImpulseSource>().GenerateImpulse();
        Instantiate(deathParticle, transform.position, Quaternion.identity);
        FindObjectOfType<SnakeMovement>().SetSnakeStatus(false);
        FindObjectOfType<AISnakeMovement>().SetSnakeStatus(true);
        GetComponent<SpriteRenderer>().sprite = deadSnake;
    }
}
