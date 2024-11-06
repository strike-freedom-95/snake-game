using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHeadScript : MonoBehaviour
{
    [SerializeField] ParticleSystem deathParticle;
    [SerializeField] Sprite deadSnake;
    [SerializeField] AudioClip eatSFX;
    [SerializeField] AudioClip deadSFX;
    [SerializeField] Canvas deathScreen;
    [SerializeField] ParticleSystem eatFX;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Fruit")
        {
            FruitSequence(collision);
        }
    }

    private void FruitSequence(Collider2D collision)
    {
        FindObjectOfType<ScoreCounterScript>().IncreaseScore();
        Instantiate(eatFX, transform.position, Quaternion.identity);
        Destroy(collision.gameObject);
        AudioSource.PlayClipAtPoint(eatSFX, Camera.main.transform.position);
        FindObjectOfType<FruitSpawner>().SpawnNewFruit();
        FindObjectOfType<SnakeMovement>().FruitEaten();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Snake Body")
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
        AudioSource.PlayClipAtPoint(deadSFX, Camera.main.transform.position);
        GetComponent<CinemachineImpulseSource>().GenerateImpulse();
        Instantiate(deathParticle, transform.position, Quaternion.identity);
        FindObjectOfType<SnakeMovement>().SetSnakeStatus(false);
        GetComponent<SpriteRenderer>().sprite = deadSnake;
        StartCoroutine(ShowDeathScreenAfterDelay(1));
    }

    IEnumerator ShowDeathScreenAfterDelay(float duration)
    {
        yield return new WaitForSeconds(duration);
        Instantiate(deathScreen, Vector2.zero, Quaternion.identity);
    }
}
