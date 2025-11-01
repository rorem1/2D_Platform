using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] public int damage = 1;
    Rigidbody2D rb;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground")|| collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
    public void Shot(Vector2 dir , float speed)
    {
        
        rb.velocity = dir * speed;
    }
    public void RightShot(Vector2 dir , float speed)
    {
        rb.velocity = Vector2.right * speed;
    }
    public void LeftShot(Vector2 dir, float speed)
    {
        rb.velocity = Vector2.left * speed;
    }
}
