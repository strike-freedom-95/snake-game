using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DecisionScript : MonoBehaviour
{
    [SerializeField] Canvas deathScreen;

    bool isResultDisplayed = false;

    private void Update()
    {
        if(!FindObjectOfType<SnakeMovement>().GetSnakeStatus() || !FindObjectOfType<AISnakeMovement>().GetSnakeStatus())
        {
            FindObjectOfType<SnakeMovement>().SetMatchStatus(true);
            FindObjectOfType<AISnakeMovement>().SetMatchStatus(true);
            if (!isResultDisplayed)
            {
                isResultDisplayed = true;
                MatchOver();
            }
        }        
    }

    void MatchOver()
    {
        FindObjectOfType<SnakeMovement>().SetMatchStatus(true);
        FindObjectOfType<AISnakeMovement>().SetMatchStatus(true);

        if (!FindObjectOfType<SnakeMovement>().GetSnakeStatus())
        {
            StartCoroutine(ShowDeathScreenAfterDelay(0.2f, "Computer Wins!"));
        }

        else if (!FindObjectOfType<AISnakeMovement>().GetSnakeStatus())
        {
            StartCoroutine(ShowDeathScreenAfterDelay(0.2f, "Human Wins!"));
        }

        else
        {
            StartCoroutine(ShowDeathScreenAfterDelay(0.2f, "Its a tie!"));
        }

        IEnumerator ShowDeathScreenAfterDelay(float duration, string conclusion)
        {
            yield return new WaitForSeconds(duration);
            var inst = Instantiate(deathScreen, Vector2.zero, Quaternion.identity);
            TextMeshProUGUI[] messages = inst.GetComponentsInChildren<TextMeshProUGUI>();
            foreach (TextMeshProUGUI message in messages)
            {
                if (message.gameObject.tag == "Message")
                {
                    message.text = conclusion;
                }
            }
        }
    }
}
