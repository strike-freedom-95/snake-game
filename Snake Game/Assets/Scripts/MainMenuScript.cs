using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void OnSinglePlayButtonClicked()
    {
        StartCoroutine(AddSceneTransistionDelay());
    }

    public void OnPlayWithAIButtonClicked()
    {

    }

    public void OnQuitButtonClicked()
    {
        Application.Quit();
    }

    IEnumerator AddSceneTransistionDelay()
    {
        FindObjectOfType<FadeScript>().StartFadeEffect();
        yield return new WaitForSeconds(1);
        if (GameObject.FindGameObjectWithTag("Game Music") != null)
        {
            Destroy(GameObject.FindGameObjectWithTag("Game Music"));
        }
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndex + 1);
    }
}
