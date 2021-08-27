using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float forceMagnitude;
    [SerializeField] private float maxVelocity;

    [SerializeField] [Tooltip("Movetowards or moveaway from touch position")]
    private bool invertForce;

    private Camera mainCamera;

    private Rigidbody rb;

    private Vector3 movementDirection;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Touchscreen.current.primaryTouch.press.isPressed)
        {
            Vector2 touchPos = Touchscreen.current.primaryTouch.position.ReadValue();

            Vector3 worldPos = mainCamera.ScreenToWorldPoint(touchPos);

            movementDirection = transform.position - worldPos;
            if (invertForce)
            {
                movementDirection = -movementDirection;
            }

            movementDirection.z = 0f;
            movementDirection.Normalize();
        }
        else
        {
            movementDirection = Vector3.zero;
        }
    }

    //For physics calculations
    private void FixedUpdate()
    {
        if (movementDirection == Vector3.zero)
        {
            return;
        }

        rb.AddForce(movementDirection * forceMagnitude * Time.deltaTime,
            ForceMode.Force); //Time.deltaTime actually is not needed inside fixed update

        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity); //Limiting maximum velocity
    }
}