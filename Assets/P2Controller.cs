using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class P2Controller : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float MoveSpeed;
    [SerializeField] float turnSpeed = 200f; // Adjust for your desired turn speed
    private float currentRotation;
    [SerializeField] public float P2Health = 100f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.U))
        {
            currentRotation += turnSpeed * Time.deltaTime;

        }

        if (Input.GetKey(KeyCode.O))
        {
            currentRotation -= turnSpeed * Time.deltaTime;
        }

    }

    private void FixedUpdate()
    {
        //rb.velocity = new Vector2(Input.GetAxis("P1Horizontal") * MoveSpeed, Input.GetAxis("P1Vertical") * MoveSpeed);
        /*Vector2 dir = new Vector2(Mathf.Cos(rb.rotation * Mathf.Deg2Rad), Mathf.Sin(rb.rotation * Mathf.Deg2Rad));

        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity = dir.normalized * MoveSpeed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rb.velocity = dir.normalized * -MoveSpeed;
        } */

        Vector2 forwardDir = new Vector2(Mathf.Cos(rb.rotation * Mathf.Deg2Rad), Mathf.Sin(rb.rotation * Mathf.Deg2Rad));
        Vector2 rightDir = new Vector2(-forwardDir.y, forwardDir.x); // 90 degrees rotation for right

        Vector2 desiredVelocity = Vector2.zero;

        if (Input.GetKey(KeyCode.I))
        {
            desiredVelocity += forwardDir.normalized * MoveSpeed; // Move forward
        }
        if (Input.GetKey(KeyCode.K))
        {
            desiredVelocity += -forwardDir.normalized * MoveSpeed; // Move backward
        }
        if (Input.GetKey(KeyCode.J))
        {
            desiredVelocity += rightDir.normalized * MoveSpeed; // Strafe left
        }
        if (Input.GetKey(KeyCode.L))
        {
            desiredVelocity += -rightDir.normalized * MoveSpeed; // Strafe right
        }

        rb.velocity = desiredVelocity;


        // Rotate counterclockwise with Q

        //transform.rotation = Quaternion.Euler(0, 0, currentRotation);
        rb.rotation = currentRotation;
    }

    public void TakeDamage(float damage)
    {
        P2Health -= damage;
        //Damage effects & multipliers & stuff
    }

}