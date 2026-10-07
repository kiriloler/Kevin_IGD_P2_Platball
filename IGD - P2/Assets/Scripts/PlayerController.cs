using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private TrailRenderer trail;
    [SerializeField] float accerlerationRate;
    [SerializeField] float deccerlerationRate;
    [SerializeField] float maxSpeed;

    AudioSource audio;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        trail = GetComponent<TrailRenderer>();
        audio = GetComponent<AudioSource>();
        SpawnManager.Instance.SetSpawnPoint(gameObject);
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");

        if(h != 0f)
        {
            rb.linearVelocityX += h * accerlerationRate;
            rb.linearVelocityX = Mathf.Clamp(rb.linearVelocityX, -maxSpeed, maxSpeed);
        }
        /*else if(rb.linearVelocityX != 0)
        {
            float sign = Mathf.Sign(rb.linearVelocityX);
            rb.linearVelocityX -= sign * deccerlerationRate;

            if(sign == 1)
            {
                rb.linearVelocityX = Mathf.Clamp(rb.linearVelocityX, 0f, sign * maxSpeed);
            }
            else
            {
                rb.linearVelocityX = Mathf.Clamp(rb.linearVelocityX, sign * maxSpeed, 0f);
            }
        }*/
    }

    public void Restart()
    {
        rb.linearVelocityX = 0f;
        rb.linearVelocityY = 0f;
        rb.angularVelocity = 0f;
        transform.position = SpawnManager.Instance.SpawnPoint;
        trail.Clear();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Spike"))
        {
            audio.Play();
            Restart();
        }
        else if (collision.gameObject.CompareTag("HurtCollider"))
        {
            Restart();
        }
    }
}
