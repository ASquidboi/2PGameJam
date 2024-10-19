using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class P1Controller : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float MoveSpeed;
    [SerializeField] float turnSpeed = 200f; // Adjust for your desired turn speed
    private float currentRotation;
    [SerializeField] public float P1Health = 100f;
    [SerializeField] GameObject P1Spawn;
    public bool P1IsDead = false;
    [SerializeField] GameObject Skull;
    [Tooltip("McDonalds sprite, specifically.")][SerializeField] GameObject Sprite;
    //pow! you are dead!

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (P1IsDead == false && Input.GetKey(KeyCode.Q))
        {
            currentRotation += turnSpeed * Time.deltaTime;

        }

        // Optionally, rotate clockwise with E
        if (P1IsDead == false && Input.GetKey(KeyCode.E))
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

        if (P1IsDead == false && Input.GetKey(KeyCode.W))
        {
            desiredVelocity += forwardDir.normalized * MoveSpeed; // Move forward
        }
        if (P1IsDead == false && Input.GetKey(KeyCode.S))
        {
            desiredVelocity += -forwardDir.normalized * MoveSpeed; // Move backward
        }
        if (P1IsDead == false && Input.GetKey(KeyCode.A))
        {
            desiredVelocity += rightDir.normalized * MoveSpeed; // Strafe left
        }
        if (P1IsDead == false && Input.GetKey(KeyCode.D))
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
        P1Health -= damage;
        //Damage effects & multipliers & stuff
        if (P1Health <= 0)
        {
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        //PREPARE THYSELF
        //effect code
        P1IsDead = true;
        Sprite.SetActive(false);
        GameObject skull = Instantiate(Skull, transform.position, Quaternion.Euler(0, 0, 0));
        yield return new WaitForSeconds(3);
        transform.position = P1Spawn.transform.position;
        P1Health = 100;
        P1IsDead = false;
        Sprite.SetActive(true);
        Destroy(skull);
    }

}
