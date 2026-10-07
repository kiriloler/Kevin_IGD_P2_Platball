using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndpointController : MonoBehaviour
{
    [SerializeField] SpriteRenderer sr;
    [SerializeField] Sprite lightenSprite;
    [SerializeField] Image angleRange;
    [SerializeField] float timeToProcess = 1;
    [SerializeField] GameObject EndVFX;
    AudioSource audio;

    bool isCompleted = false;

    private Coroutine angleRangeCoroutine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        angleRange.fillAmount = 0;
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isCompleted)
        {
            audio.Play();
            if(angleRangeCoroutine == null)
            angleRangeCoroutine = StartCoroutine(DoAngleRangeAccumulation());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (angleRangeCoroutine != null && !isCompleted)
            {
                audio.Stop();
                StopCoroutine(angleRangeCoroutine);
                angleRangeCoroutine = null;
                angleRange.fillAmount = 0;
            }
        }
    }

    private IEnumerator DoAngleRangeAccumulation()
    {
        float speed = 1 / timeToProcess;
        while (angleRange.fillAmount < 1)
        {
            angleRange.fillAmount += speed * Time.deltaTime;
            yield return null;
        }

        sr.sprite = lightenSprite;
        isCompleted = true;
        //vfx
        GameObject vfx = Instantiate(EndVFX, transform.parent);
        vfx.transform.localPosition = new Vector3(0.5f, -0.5f);

        yield return new WaitForSeconds(1.5f);

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Calculate the next scene index
        int nextSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;

        SceneManager.LoadScene(nextSceneIndex);

        angleRangeCoroutine = null;
    }
}
