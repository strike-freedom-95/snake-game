using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISnakeHeadScript : MonoBehaviour
{
    [SerializeField] ParticleSystem deathParticle;
    [SerializeField] Sprite deadSnake;
    [SerializeField] AudioClip eatSFX;
    [SerializeField] AudioClip deadSFX;
    [SerializeField] Canvas deathScreen;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Fruit")
        {
            Destroy(collision.gameObject);
            AudioSource.PlayClipAtPoint(eatSFX, Camera.main.transform.position);
            FindObjectOfType<FruitSpawner>().SpawnNewFruit();
            FindObjectOfType<SnakeMovement>().FruitEaten();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Snake Body")
        {
            SnakeDeathSequence();
        }

        if (collision.gameObject.tag == "Wall")
        {

            SnakeDeathSequence();
        }
    }

    private void SnakeDeathSequence()
    {
        Instantiate(deathScreen, Vector2.zero, Quaternion.identity);
        AudioSource.PlayClipAtPoint(deadSFX, Camera.main.transform.position);
        GetComponent<CinemachineImpulseSource>().GenerateImpulse();
        Instantiate(deathParticle, transform.position, Quaternion.identity);
        FindObjectOfType<AISnakeMovement>().SetSnakeStatus(false);
        GetComponent<SpriteRenderer>().sprite = deadSnake;
        // Destroy(gameObject);
    }
}
