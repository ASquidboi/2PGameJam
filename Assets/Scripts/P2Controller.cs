using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class P2Controller : MonoBehaviour
{
    Rigidbody2D rb;

    //Values
    [SerializeField] float MoveSpeed;
    [SerializeField] float turnSpeed = 200f;
    [SerializeField] public float P2Health = 100f;

    //Internal values
    private float currentRotation;
    public bool P2IsDead = false; //Variable for checking whether the player is dead (so as to stop them from doing anything)

    //Prefabs
    [SerializeField] GameObject P2DeathScreen;
    [Tooltip("text 2: text harder")][SerializeField] TMP_Text respawnText;
    [SerializeField] ScoreManager scoremanager;
    [SerializeField] GameObject Skull;
    [SerializeField] GameObject Sprite;
    [SerializeField] GameObject P2Spawn;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Turn using U & O
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
        
        //Get "forward" and "right"
        Vector2 forwardDir = new Vector2(Mathf.Cos(rb.rotation * Mathf.Deg2Rad), Mathf.Sin(rb.rotation * Mathf.Deg2Rad));
        Vector2 rightDir = new Vector2(-forwardDir.y, forwardDir.x); // 90 degrees rotation for right

        Vector2 desiredVelocity = Vector2.zero;

        //Get player input for movement
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
        //Take damage... obviously... 
        P2Health -= damage;
        //Damage effects & multipliers & stuff
        if (P2Health <= 0)
        {
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        
        //Update scoreboard
        scoremanager.P1Score += 1;
        //Disable sprite and enable death FX
        P2IsDead = true;
        Sprite.SetActive(false);
        P2DeathScreen.SetActive(true);
        //Death timers
        respawnText.SetText("Respawning in 3...");
        GameObject skull = Instantiate(Skull, transform.position, Quaternion.Euler(0, 0, 0)); //Instantiate skull

        yield return new WaitForSeconds(1);
        respawnText.SetText("Respawning in 2...");

        yield return new WaitForSeconds(1);
        respawnText.SetText("Respawning in 1...");

        yield return new WaitForSeconds(1);
        respawnText.SetText("Respawning...");

        yield return new WaitForSeconds(1);

        //Respawn player
        transform.position = P2Spawn.transform.position;
        P2Health = 100;
        P2IsDead = false;
        //Reenable sprite and disable death FX
        Sprite.SetActive(true);
        P2DeathScreen.SetActive(false);
        Destroy(skull);
    }

}