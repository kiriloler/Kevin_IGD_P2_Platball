using UnityEngine;
using TMPro;

public class EndText : MonoBehaviour
{
    public TextMeshPro tmp_text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tmp_text.text = $"Thanks for playing! Orb collected: {GameManager.OrbCollected}/5";
        GameManager.OrbCollected = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
