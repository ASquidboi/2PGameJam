using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    GameObject self;
    Rigidbody2D rb;
    [Header("Settings")]
    [Tooltip("Movement Speed. Multiplier.")][SerializeField] float MoveSpeed;
    [Tooltip("Jump Height. Not a multiplier.")][SerializeField] float JumpHeight;
    [Tooltip("Health. Set value.")][SerializeField] private float health;
    [Tooltip("Health. Set value.")][SerializeField] private float maxHealth;
    [Tooltip("Health. Set value.")][SerializeField] private GameObject particles;
    [Tooltip("text")][SerializeField] TMP_Text text;
    public float Health
    {
        get { return health; }
        set { 
            health = value;
            //update ui
            //check if aliven't
            //math n stuff
            print("player health thingymajig");
            if (health <= 0)
            {
                Die();
            }
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        self = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        text.SetText(health + "/" + maxHealth);
    }

    void FixedUpdate()
    {
        // horizontal
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * MoveSpeed, rb.velocity.y);
        //jumping
        if (Input.GetAxisRaw("Jump") == 1)
        {
            //ground check
            bool onGround = false;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(rb.position + new Vector2(0,-1), 0.01f);
            foreach (Collider2D col in colliders)
            {
                if (col.tag == "Ground") { onGround = true; break; }  
            }
            if (onGround)
            {
                rb.velocity = new Vector2(rb.velocity.x, JumpHeight);
            }
            
        }
    }
    void Die()
    {
        //Whatever visual effects
        //Destroy player
        //GUI for restarting and whatnot
        Instantiate(particles, transform.position, transform.rotation);
        Destroy(self);
        
      
        //respawn code
    }
}
