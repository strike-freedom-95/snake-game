using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UserInputScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI finalScoreText;

    private void Start()
    {
        finalScoreText.text = "Score : " + FindObjectOfType<ScoreCounterScript>().GetScore().ToString();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Y))
        {
            // FindObjectOfType<FadeScript>().StartFadeEffect();
            int currentIndex = SceneManager.GetActiveScene().buildIndex;
            StartCoroutine(AddSceneTransistionDelay(currentIndex, false));
        }
        else if (Input.GetKeyUp(KeyCode.N))
        {
            // FindObjectOfType<FadeScript>().StartFadeEffect();
            StartCoroutine (AddSceneTransistionDelay(0, true));
        }
    }

    IEnumerator AddSceneTransistionDelay(int buildIndex, bool isQuitting)
    {
        FindObjectOfType<FadeScript>().StartFadeEffect();
        yield return new WaitForSeconds(1);
        if (GameObject.FindGameObjectWithTag("Game Music") != null && isQuitting)
        {
            Destroy(GameObject.FindGameObjectWithTag("Game Music"));
        }

        SceneManager.LoadScene(buildIndex);
    }
}
