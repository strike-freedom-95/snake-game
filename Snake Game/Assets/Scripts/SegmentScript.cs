using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentScript : MonoBehaviour
{
    [SerializeField] Sprite tailEnd;

    public void SetDuration(float time, float snakeSpeed)
    {
        StartCoroutine(ChangeToTail(time - snakeSpeed));
        // Destroy(gameObject, time);
        StartCoroutine(DelayedDestruction(time));
    }

    IEnumerator ChangeToTail(float interval)
    {
        yield return new WaitForSeconds(interval);        
        if (FindObjectOfType<SnakeMovement>().GetSnakeStatus())
        {
            GetComponent<SpriteRenderer>().sprite = tailEnd;
        }
    }

    IEnumerator DelayedDestruction(float interval)
    {
        yield return new WaitForSeconds (interval);
        if (FindObjectOfType<SnakeMovement>().GetSnakeStatus())
        {
            Destroy(gameObject);
        }
        
    }
}
