using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerScript : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            Click();
        }
    }

    void Click()
    {
        // Debug.Log("Clicked");
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();

        if(Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.collider.gameObject.name);
        }
    }    
}