using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    float xMovement, zMovement;
    Rigidbody rb;

    [SerializeField] float cameraMoveSpeed = 1;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        xMovement = Input.GetAxisRaw("Horizontal");
        zMovement = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        rb.AddForce(new Vector3(-xMovement, 0, -zMovement) * cameraMoveSpeed, ForceMode.VelocityChange);
    }
}
