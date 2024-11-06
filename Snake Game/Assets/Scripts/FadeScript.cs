using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScript : MonoBehaviour
{
    public void StartFadeEffect()
    {
        GetComponent<Animator>().SetTrigger("isFading");
    }
}
