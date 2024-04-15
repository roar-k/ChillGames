using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public SpriteRenderer sr;
    public float time = 10;
    public TextMeshProUGUI timerText;
    public Sprite redTimer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;

        while (time < 4) {
            sr = gameObject.GetComponent<SpriteRenderer>();
            
            if (time < 1) {
            time = 0;
        }
        timerText.text = ((int)time).ToString();
        }
        
        
    }

    public void TimeUp () {

    }
}
