using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//dummy comment
public class CPlayerController : MonoBehaviour
{
    Rigidbody2D rb;

    //Values
    [SerializeField] float MoveSpeed;
    //[SerializeField] float turnSpeed = 200f;
    [SerializeField] public float P1Health = 100f;

    //Internal values
    private float currentRotation;
    public bool P1IsDead = false; //Variable for checking whether the player is dead (so as to stop them from doing anything)

    //Prefabs
    [SerializeField] GameObject P1DeathScreen;
    [Tooltip("text 2: text harder")][SerializeField] TMP_Text respawnText;
    [SerializeField] ScoreManager scoremanager;
    [SerializeField] GameObject Skull;
    [SerializeField] GameObject Sprite;
    [SerializeField] GameObject P1Spawn;




    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 rotation_direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(rotation_direction.y, rotation_direction.x) * Mathf.Rad2Deg;



        Quaternion desiredRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, 0.025f);

    }

    private void FixedUpdate()
    {
        //Initialize directions for movement
        Vector2 forwardDir = new Vector2(Mathf.Cos(rb.rotation * Mathf.Deg2Rad), Mathf.Sin(rb.rotation * Mathf.Deg2Rad));
        Vector2 rightDir = new Vector2(-forwardDir.y, forwardDir.x); // 90 degrees rotation for right


        Vector2 desiredVelocity = Vector2.zero;

        //Check for WASD inputs for movement
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

        rb.rotation = currentRotation;
    }

    public void TakeDamage(float damage)
    {
        //Take damage... obviously...
        P1Health -= damage;
        //Damage effects & multipliers & stuff
        if (P1Health <= 0)
        {
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {

        //Change scoreboard, disable sprite, enable death FX
        scoremanager.P2Score += 1;
        P1IsDead = true;
        Sprite.SetActive(false);
        P1DeathScreen.SetActive(true);

        //Respawn timers
        respawnText.SetText("Respawning in 3...");
        GameObject skull = Instantiate(Skull, transform.position, Quaternion.Euler(0, 0, 0)); //Instantiate skull prefab for death effect
        yield return new WaitForSeconds(1);
        respawnText.SetText("Respawning in 2...");

        yield return new WaitForSeconds(1);
        respawnText.SetText("Respawning in 1...");


        yield return new WaitForSeconds(1);
        respawnText.SetText("Respawning...");

        yield return new WaitForSeconds(1);

        //Respawn player
        transform.position = P1Spawn.transform.position;
        P1Health = 100;
        P1IsDead = false;
        //Reenable sprite and disable death FX
        Sprite.SetActive(true);
        P1DeathScreen.SetActive(false);
        Destroy(skull);
    }

}
