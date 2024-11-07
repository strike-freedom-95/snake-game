using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour
{
    [SerializeField] Sprite[] wallSprites;
    [SerializeField] GameObject tileObject;

    private void Start()
    {
        tileObject.GetComponent<SpriteRenderer>().sprite = wallSprites[Random.Range(0, wallSprites.Length)];
    }
}
