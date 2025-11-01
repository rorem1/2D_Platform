using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private float speed = 10.0f;
    public int damage = 1;

    Rigidbody2D rb;
    //총알 앞으로만 나가게
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        //transform.Translate(Vector3.right * speed * Time.deltaTime);
        rb.velocity = transform.right * speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
        //if (collision.CompareTag("Boss"))
        //{
        //    //보스딜추가?
        //    Destroy(gameObject);
        //}
    }
}
