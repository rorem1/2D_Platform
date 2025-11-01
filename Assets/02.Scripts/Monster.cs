using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    //https://www.youtube.com/watch?v=7MYUOzgZTf8&list=PLO-mt5Iu5TeZGR_y6mHmTWyo0RyGgO0N_&index=6
    [Header("Setting")]
    [SerializeField] private float MaxHealth = 10f;
    [SerializeField] private float currentHealth = 10f;
    [SerializeField] private float moveSpeed = 5f;

    
    public int nextMove;

    //체력바
    //https://www.youtube.com/watch?v=mwuVOsCgoPY
    public GameObject healthBarBackground;
    public Image healthBarFill;

    Rigidbody2D rb;
    SpriteRenderer sr;



    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        
        Invoke("MovePattern", 0.5f);

        healthBarFill.fillAmount = 1f;
    }

    private void FixedUpdate()
    {
        //이동
        rb.velocity = new Vector2(rb.velocity.x, nextMove*moveSpeed);
        
        
        //지형확인
        Raycast();

        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Bullet bullet = collision.GetComponent<Bullet>();
            
            TakeDamage(bullet.damage);
            Destroy(collision.gameObject);
        }
        
    }
    private void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBarFill.fillAmount = currentHealth / MaxHealth;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Destroy(gameObject);
    }
    
    private void MovePattern()
    {
        nextMove = Random.Range(-1, 2);

        Invoke("MovePattern", 0.5f);
    }
    private void Raycast()
    {
        


        Debug.DrawRay(transform.position, Vector3.up*3f, Color.red);
        Debug.DrawRay(transform.position, Vector3.down*2.5f, Color.red);

        Vector2 top = new Vector2(transform.position.x, transform.position.y + 1);
        Vector2 down = new Vector2(transform.position.x, transform.position.y - 1);
        
        RaycastHit2D rayHitUp = Physics2D.Raycast(top, Vector3.up, 2f, LayerMask.GetMask("Ground"));
        RaycastHit2D rayHitDown = Physics2D.Raycast(down, Vector3.down, 1.5f, LayerMask.GetMask("Ground"));

        //만약 레이가 레이어그라운드에 닿으면 >> 반대로 움직이기
        if(rayHitUp.collider != null && nextMove > 0)
        {
            Debug.Log("위에 닿음");
            nextMove = -1;
        }
        if(rayHitDown.collider != null && nextMove < 0)
        {
            Debug.Log("아래에 닿음");
            nextMove = 1;
        }
        
        
        




    }

}
