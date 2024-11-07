using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentScript : MonoBehaviour
{
    [SerializeField] Sprite tailEnd;
    [SerializeField] bool isAI = false;

    public void SetDuration(float time, float snakeSpeed)
    {
       
        // Destroy(gameObject, time);
       
        if(isAI)
        {
            if (!FindObjectOfType<AISnakeMovement>().GetMatchStatus())
            {
                StartCoroutine(ChangeToTail(time - snakeSpeed));
                StartCoroutine(DelayedDestruction(time));
            }
        }
        else
        {
            if (!FindObjectOfType<SnakeMovement>().GetMatchStatus())
            {
                StartCoroutine(ChangeToTail(time - snakeSpeed));
                StartCoroutine(DelayedDestruction(time));
            }
        }
    }

    IEnumerator ChangeToTail(float interval)
    {
        yield return new WaitForSeconds(interval); 
        if (!isAI)
        {
            if (FindObjectOfType<SnakeMovement>().GetSnakeStatus())
            {
                GetComponent<SpriteRenderer>().sprite = tailEnd;
            }
        }
        else
        {
            if (FindObjectOfType<AISnakeMovement>().GetSnakeStatus())
            {
                GetComponent<SpriteRenderer>().sprite = tailEnd;
            }
        }
        
    }

    IEnumerator DelayedDestruction(float interval)
    {
        yield return new WaitForSeconds (interval);
        if (!isAI)
        {
            if (FindObjectOfType<SnakeMovement>().GetSnakeStatus())
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if (FindObjectOfType<AISnakeMovement>().GetSnakeStatus())
            {
                Destroy(gameObject);
            }
        }
        
        
    }
}
