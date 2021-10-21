using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.Events; 
using UnityEngine.EventSystems; 

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame updateù
    public Color titleColor;
    public Color subtitleColor;
    public Color gameOverColor;
    public Text title;
    public Text subtitle;
    public Text gameOver;

    void Start()
    {
        title = GameObject.Find("Title").GetComponent<Text>();
        subtitle = GameObject.Find("SubTitle").GetComponent<Text>();
        gameOver = GameObject.Find("GameOver").GetComponent<Text>();

        titleColor = title.color;
        subtitleColor = subtitle.color;
        gameOverColor = gameOver.color;

        title.color = new Color(titleColor.r, titleColor.g, titleColor.b, 0);
        subtitle.color = new Color(subtitleColor.r, subtitleColor.g, subtitleColor.b, 0);
        gameOver.color = new Color(gameOverColor.r, gameOverColor.g, gameOverColor.b, 0);

        StartCoroutine(FadeInText(title, 1f));
        StartCoroutine(FadeInText(subtitle, 1f));
        StartCoroutine(FadeOutText(title, 0.9f));
        StartCoroutine(FadeOutText(subtitle, 0.9f));
    }

    void Update()
    {
        if (DetectCollision.dead)
        {
            StartCoroutine(FadeInText(gameOver, 1f));        
        }
    }

    IEnumerator FadeOutText(Text text, float fadeSpeed)
    {
        while (text.color.a > 0)
        {
            Color objectColor = text.color;
            float fadeAmount = text.color.a - (fadeSpeed * Time.deltaTime);

            text.color = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            yield return null;
        }
    }

    IEnumerator FadeInText(Text text, float fadeSpeed)
    {
        while (text.color.a < 1)
        {
            Color objectColor = text.color;
            float fadeAmount = text.color.a + (fadeSpeed * Time.deltaTime);

            text.color = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            yield return null;
        }
    }
}
