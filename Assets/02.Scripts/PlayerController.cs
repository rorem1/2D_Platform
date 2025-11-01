using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    /*
     아이템
    1 
    2 체력회복
    3 기본공격력증가
    4 이동속도 증가
    5 뒤에 조그만 드론 무기발사(느린직선 ,빠른직선 , 유도, )
    6 
        
     */


    //이동 점프 체력 공격력 애니메이션 땅체크

    [Header("Setting")]
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float jumpForce = 5.0f;
    [SerializeField] private float maxHp = 15f;
    [SerializeField] private float curHp = 15f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float checkRadius = 0.15f;
    [SerializeField] LayerMask groundLayer;

    //내려가기
    //https://www.youtube.com/watch?v=7rCUt6mqqE8
    private GameObject downPlatform;
    [SerializeField] private Collider2D playerCollider;


    //dash
    //https://www.youtube.com/watch?v=2kFGmuPHiA0
    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 10f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;
    [SerializeField] private TrailRenderer tr;

    public Bullet bulletprefab;
    public Transform firePoint;
    [SerializeField] private float fireLate = 0.3f;
    [SerializeField] private float fireTime;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;

    private float inputX;
    private float inputY;
    private bool isGround;

    public GameObject hpBack;
    public Image hpBarFill;
    
    
    
    
    void Start()

    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        playerCollider = GetComponent<Collider2D>();
        
    }

    void Update()
    {
        if (isDashing)
        {
            return;
        }
        //AD로만 이동
        inputX = 0f;
        if (Input.GetKey(KeyCode.A))
        {
            inputX = -1f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            inputX = 1f;
        }
        
        //쭈구리기
        if (Input.GetKey(KeyCode.S))
        {
            inputX = 0f;
            anim.SetBool("Duck", true);
        }
        else
        {
            anim.SetBool("Duck", false);
        }
        
        //점프
        if(Input.GetKeyDown(KeyCode.Space)&&isGround)
        {
            Jump();
            anim.SetBool("Jump", true);
            
        }
        if (isGround && rb.velocity.y <= 0.05f)
        {
            anim.SetBool("Jump", false);
            
        }

        //좌우 이동
        if (rb.velocity.x == 0)
        {
            anim.SetBool("isWalking", false);
        }
        else
        {
            anim.SetBool("isWalking", true);
        }        
        
        //대시기능
        //if (Input.GetKeyDown(KeyCode.Mouse1))
        //{
        //    anim.SetBool("Dash", true);
        //}
        if (Input.GetKeyDown(KeyCode.Mouse1) && canDash)
        {
            anim.SetTrigger("Dash2");
            StartCoroutine(Dash());
        }
        //공격꾹 누르기
        //이동공격 제자리공격 가능하게만들기
        if (Input.GetKey(KeyCode.Mouse0)&&Time.time>=fireTime)
        {
            fireTime = Time.time + fireLate;
            Fire();
            anim.SetBool("Atk",true);
        }
        else
        {
            anim.SetBool("Atk", false);
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            anim.SetBool("Atk2", true);
        }
        else
        {
            anim.SetBool("Atk2", false);
        }
        //s랑 스페이스 같이눌렀을때 밑으로내려가기
        
        if (Input.GetKey(KeyCode.S) && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("s누름");
            if(downPlatform != null)
            {
                
            }
        }
    }
    
    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
        Move();
        CheckGround();
        Flip();
    }
    private void TakeDamage(int damage)
    {
        Debug.Log("체력닳음");
        curHp -= damage;

        hpBarFill.fillAmount = curHp / maxHp;
        if(curHp == 0)
        {
            Destroy(gameObject);
            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyBullet"))
        {
            Debug.Log("총알맞음");
            EnemyBullet bullet =collision.GetComponent<EnemyBullet>();

            TakeDamage(bullet.damage);
            Destroy(bullet);
        }
    }
    private void Move()
    {
        rb.velocity = new Vector2(inputX*moveSpeed,rb.velocity.y);
        
    }
    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
    private void CheckGround()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
    }
    private void Flip()
    {
        if(inputX != 0f)
        {
            if(inputX < 0f)
            {
                sr.flipX = true;
            }
            else
            {
                sr.flipX = false;
                
            }
        }
    }
    //내려가기
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
            
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("OneWayPlatform"))
    //    {
    //        downPlatform = collision.gameObject;
    //    }
    //}
    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("OneWayPlatform"))
    //    {
    //        downPlatform = null;
    //    }
    //}
    //private IEnumerator DisableCollision(GameObject platform)
    //{
    //    var platformCol = platform.GetComponent<Collider2D>();
    //    if (platformCol == null)
    //        platformCol = platform.GetComponent<Collider2D>();

    //    if (platformCol == null)
    //        yield break;

    //    Physics2D.IgnoreCollision(playerCollider, platformCol, true);
    //    yield return new WaitForSeconds(0.5f);
    //    Physics2D.IgnoreCollision(playerCollider, platformCol, false);

    //}
    //private IEnumerator DisableCollision(GameObject platform)
    //{
    //    var platformcol = platform.GetComponent<Collider2D>();
    //    if (platformcol == null) yield break;
    //    Physics2D.IgnoreCollision(playerCollider, platformcol, true);
    //    yield return new WaitForSeconds(0.5f);
    //    Physics2D.IgnoreCollision(playerCollider, platformcol, false);
    //}
    //private IEnumerator DisableCollision()
    //{
    //    Collider2D platformCollider = downPlatform.GetComponent<Collider2D>();

    //    Physics2D.IgnoreCollision(playerCollider, platformCollider);
    //    yield return new WaitForSeconds(1f);
    //    Physics2D.IgnoreCollision(playerCollider, platformCollider, false);
    //}
    private void Fire()
    {
        Bullet bullet = Instantiate(bulletprefab, firePoint.position, firePoint.rotation);
    }

    //대시코루틴
    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        float originalGravity = rb.gravityScale;
        
        rb.gravityScale = 0f;
        float dashDir = sr.flipX ? -1f : 1f;

        rb.velocity = new Vector2(dashDir * dashingPower, 0f);
        
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);        
        tr.emitting = false;
        
        rb.gravityScale = originalGravity;
        isDashing = false;
        
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
    }

}
