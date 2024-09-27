using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1Controller : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float MoveSpeed;
    [SerializeField] float turnSpeed = 200f; // Adjust for your desired turn speed
    private float currentRotation;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(Input.GetAxis("P1Horizontal") * MoveSpeed, Input.GetAxis("P1Vertical") * MoveSpeed);
        // Rotate counterclockwise with Q
        if (Input.GetKey(KeyCode.Q))
        {
            currentRotation -= turnSpeed * Time.deltaTime;
        }

        // Optionally, rotate clockwise with E
        if (Input.GetKey(KeyCode.E))
        {
            currentRotation += turnSpeed * Time.deltaTime;
        }
        transform.rotation = Quaternion.Euler(0, 0, currentRotation);
    }
}
