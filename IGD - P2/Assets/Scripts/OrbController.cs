using UnityEngine;

public class OrbController : MonoBehaviour
{
    public GameObject OrbVFX;
    SpriteRenderer sr;
    AudioSource audio;
    bool collected = false;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collected) return;
        collected = true;
        audio.Play();
        sr.gameObject.SetActive(false);
        GameObject vfx = Instantiate(OrbVFX);
        vfx.transform.localPosition = transform.position;
        GameManager.OrbCollected += 1;
    }
}
