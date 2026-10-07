using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UI;

public class PaddleController : MonoBehaviour
{
    [SerializeField]SpriteRenderer paddleRenderer;
    public float angleA = 0;
    public float angleB = 90f;
    public float clampThreshold = 0.01f;
    public Image angleRangeImg;
    public float time = 0.15f;
    private float speed;

    private float targetAngle;
    private bool isRotating = false;
    private bool playerOnPaddleWhenFlip = false;
    private bool playerOnPaddle = false;

    private Rigidbody2D rb;

    AudioSource audio;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audio = GetComponent<AudioSource>();
        SetAngleRange();
        rb = GetComponent<Rigidbody2D>();
        rb.rotation = angleA;
        targetAngle = angleB;
        speed = Mathf.Abs(angleB - angleA) / time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.J))
        {
            isRotating = true;
            if (playerOnPaddle)
            {
                playerOnPaddleWhenFlip = true;
            }
        }

        if (isRotating)
        {

            float next = Mathf.MoveTowards(rb.rotation, targetAngle, speed * Time.fixedDeltaTime);

            rb.MoveRotation(next);

            if (Mathf.Abs(rb.rotation - targetAngle) <= clampThreshold)
            {
                rb.rotation = targetAngle;
                isRotating = false;
                playerOnPaddleWhenFlip = false;
                targetAngle = targetAngle == angleA ? angleB : angleA;
            }
        }
    }

    /*private void OnMouseEnter()
    {
        paddleRenderer.color = Color.red;
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnClicked();
        }
    }

    private void OnClicked()
    {
        isRotating = true;
    }

    private void OnMouseExit()
    {
        paddleRenderer.color = Color.white;
    }*/

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOnPaddle = true;
            if (isRotating && playerOnPaddleWhenFlip == false && audio.isPlaying == false)
            {
                audio.Play();
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOnPaddle = false;
        }
    }

    private void SetAngleRange()
    {
        float diff = angleB - angleA;
        if (diff == 0)
        {
            angleRangeImg.fillAmount = 0;
            return;
        }
        float percentage = 1 / (360f / Mathf.Abs(diff));
        float offset = angleA - 90f;
        angleRangeImg.fillClockwise = diff < 0 ? true : false;
        angleRangeImg.fillAmount = percentage;
        RectTransform rect = angleRangeImg.GetComponent<RectTransform>();
        Vector3 angle = rect.localEulerAngles;
        angle.z = offset;
        rect.localEulerAngles = angle;
    }
}
