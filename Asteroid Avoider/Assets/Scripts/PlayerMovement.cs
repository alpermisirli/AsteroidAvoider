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

    private Vector3 movementDirection;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();

        KeepPlayerOnScreen();
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

    private void ProcessInput()
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

    private void KeepPlayerOnScreen()
    {
        Vector3 newPos = transform.position;
        Vector3 viewportPos = mainCamera.WorldToViewportPoint(transform.position);

        if (viewportPos.x > 1)
        {
            newPos.x = -newPos.x + 0.1f;
        }
        else if (viewportPos.x < 0)
        {
            newPos.x = -newPos.x - 0.1f;
        }
        if (viewportPos.y > 1)
        {
            newPos.y = -newPos.y + 0.1f;
        }
        else if (viewportPos.y < 0)
        {
            newPos.y = -newPos.y - 0.1f;
        }


        transform.position = newPos;
    }
}