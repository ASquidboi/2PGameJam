using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using TMPro;
//dummy comment
public class P2Controller : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float MoveSpeed;
    [SerializeField] float turnSpeed = 200f; // Adjust for your desired turn speed
    private float currentRotation;
    [SerializeField] public float P2Health = 100f;
    [SerializeField] GameObject P2Spawn;
    public bool P2IsDead = false;
    [SerializeField] GameObject Skull;
    [Tooltip("McDonalds sprite, specifically.")][SerializeField] GameObject Sprite;
    //pow! you are dead!
    [SerializeField] GameObject P2DeathScreen;
    [Tooltip("text 2: text harder")][SerializeField] TMP_Text respawnText;
    [SerializeField] ScoreManager scoremanager;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (P2IsDead == false && Input.GetKey(KeyCode.U))
        {
            currentRotation += turnSpeed * Time.deltaTime;

        }

        // Optionally, rotate clockwise with E
        if (P2IsDead == false && Input.GetKey(KeyCode.O))
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

        if (P2IsDead == false && Input.GetKey(KeyCode.I))
        {
            desiredVelocity += -forwardDir.normalized * MoveSpeed; // Move forward
        }
        if (P2IsDead == false && Input.GetKey(KeyCode.K))
        {
            desiredVelocity += forwardDir.normalized * MoveSpeed; // Move backward
        }
        if (P2IsDead == false && Input.GetKey(KeyCode.J))
        {
            desiredVelocity += -rightDir.normalized * MoveSpeed; // Strafe left
        }
        if (P2IsDead == false && Input.GetKey(KeyCode.L))
        {
            desiredVelocity += rightDir.normalized * MoveSpeed; // Strafe right
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
        if (P2Health <= 0)
        {
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        //PREPARE THYSELF
        //effect code
	scoremanager.P1Score += 1;
        P2IsDead = true;
        Sprite.SetActive(false);
	P2DeathScreen.SetActive(true);
        respawnText.SetText("Respawning in 3...");
        GameObject skull = Instantiate(Skull, transform.position, Quaternion.Euler(0, 0, 0));
        yield return new WaitForSeconds(1);
	respawnText.SetText("Respawning in 2...");
	yield return new WaitForSeconds(1);
	respawnText.SetText("Respawning in 1...");
	yield return new WaitForSeconds(1);
	respawnText.SetText("Respawning...");
	yield return new WaitForSeconds(1);
        transform.position = P2Spawn.transform.position;
        P2Health = 100;
        P2IsDead = false;
        Sprite.SetActive(true);
	P2DeathScreen.SetActive(false);
        Destroy(skull);
    }

}
