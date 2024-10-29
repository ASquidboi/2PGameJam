using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1Bullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 40;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }
    //void OnColliderEnter2D(Collider2D hitInfo)
    //{
    //    print("AAAA");
    //    P2Controller enemy = hitInfo.GetComponent<P2Controller>();
    //    enemy.TakeDamage(damage);
    //    if (enemy != null)
    //    {

    //    }
    //    Destroy(gameObject);
    //}
    void OnCollisionEnter2D(Collision2D collision)
    {
        P1Controller enemy = collision.gameObject.GetComponent<P1Controller>();
        P2Controller enemy2 = collision.gameObject.GetComponent<P2Controller>();

        if (enemy != null)
        {
            print("AAAA");
            enemy.TakeDamage(damage);
        }
        if (enemy2 != null)
        {
            print("1");
            enemy2.TakeDamage(damage);
        }
        Destroy(gameObject);
    }


}