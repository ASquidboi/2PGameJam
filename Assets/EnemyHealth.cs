using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int health = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(damage);
        //Damage effects and whatnot
        if (health <= 0)
        {
            //Die
            Die();
        }
    }

    void Die()
    {
        //Die
        //Death effects
        Destroy(gameObject);
    }
}
