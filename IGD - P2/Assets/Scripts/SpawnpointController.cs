using UnityEngine;

public class SpawnpointController : MonoBehaviour
{
    [SerializeField] SpriteRenderer sr;
    [SerializeField] Sprite lightenSprite;
    [SerializeField] GameObject lightenVFX;
    AudioSource audio;
    bool lighten = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && lighten == false)
        {
            audio.Play();
            lighten = true;
            sr.sprite = lightenSprite;
            GameObject vfx = Instantiate(lightenVFX, transform.parent);
            vfx.transform.localPosition = new Vector3(0.5f, -0.5f);
            SpawnManager.Instance.SetSpawnPoint(gameObject);
        }
    }
}
