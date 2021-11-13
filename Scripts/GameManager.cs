using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.Events; 
using UnityEngine.EventSystems;
using System.IO;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame updateù
    public Color titleColor;
    public Color subtitleColor;
    public Color gameOverColor;
    public Color finalScoreColor;
    public GameObject title;
    public GameObject subtitle;
    public GameObject gameOver;
    public GameObject finalScore;
    public Text moreHammo;
    public Text bestScoreUI;

    public GameObject newHighScoreUI;

    public int bestScore;

    void Start()
    {
        //title = title.GetComponent<Text>();
        //subtitle = subtitle.GetComponent<Text>();
        //gameOver = gameOver.GetComponent<Text>();
        //score = GameObject.Find("Player").GetComponent<ControllerPlayer>().score       
        Load();

        bestScoreUI.text = "    High: " + bestScore.ToString();

        titleColor = title.GetComponent<Text>().color;
        subtitleColor = subtitle.GetComponent<Text>().color;
        gameOverColor = gameOver.GetComponent<Text>().color;
        finalScoreColor = finalScore.GetComponent<Text>().color;

        title.GetComponent<Text>().color = new Color(titleColor.r, titleColor.g, titleColor.b, 0);
        subtitle.GetComponent<Text>().color = new Color(subtitleColor.r, subtitleColor.g, subtitleColor.b, 0);
        gameOver.GetComponent<Text>().color = new Color(gameOverColor.r, gameOverColor.g, gameOverColor.b, 0);
        finalScore.GetComponent<Text>().color = new Color(finalScoreColor.r, finalScoreColor.g, finalScoreColor.b, 0);

        StartCoroutine(FadeInText(title.GetComponent<Text>(), 1f));
        StartCoroutine(FadeInText(subtitle.GetComponent<Text>(), 1f));
        StartCoroutine(FadeOutText(title.GetComponent<Text>(), 0.9f));
        StartCoroutine(FadeOutText(subtitle.GetComponent<Text>(), 0.9f));

    }

    void Update()
    {
        if (/*DetectCollision*/PlayerCollisions.dead)
        {
            StartCoroutine(FadeInText(gameOver.GetComponent<Text>(), 1f));
            StartCoroutine(FadeInText(finalScore.GetComponent<Text>(), 1f));
            if (PauseMenu.GameIsPaused)
            {
                gameOver.SetActive(false);
            }
        }
        //bestScoreUI.text = "    Best: " + bestScore.ToString();
    }

    public void moreHammoUI(string hammo)
    {
        moreHammo.text = hammo;
        StartCoroutine(FadeInText(moreHammo, 0.5f));
        StartCoroutine(FadeOutText(moreHammo, 0.9f));
    }

    IEnumerator FadeOutText(Text text, float fadeSpeed)
    {
        while (text.color.a > 0)
        {
            Color objectColor = text.color;
            float fadeAmount = text.color.a - (fadeSpeed * Time.deltaTime);

            text.color = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            yield return null;
            if (PauseMenu.GameIsPaused)
            {
                text.gameObject.SetActive(false);
                break;
            }
        }
        text.gameObject.SetActive(false);
    }

    IEnumerator FadeInText(Text text, float fadeSpeed)
    {
        text.gameObject.SetActive(true);
        while (text.color.a < 1)
        {
            Color objectColor = text.color;
            float fadeAmount = text.color.a + (fadeSpeed * Time.deltaTime);
        
            text.color = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            yield return null;
        
            if (PauseMenu.GameIsPaused)
            {
                text.gameObject.SetActive(false);
                break;
                
            }
        }
    }
    /*
    private bool ActiveText()
    {
        if (PauseMenu.GameIsPaused)
        {
            GameObject.Find("Title").SetActive(false);
            GameObject.Find("SubTitle").SetActive(false);
        }
        else
        {
            if (!GameObject.Find("Title").activeSelf && !GameObject.Find("SubTitle").activeSelf)
            {

            }
        }
    }
    */
    [System.Serializable]
    class SaveData
    {
        public int bestScore;
    }

    public void Save()
    {
        SaveData data = new SaveData();
        if (bestScore < ControllerPlayer.score)
        {
            //GameObject.Find("NewHighScore").SetActive(true);
            newHighScoreUI.SetActive(true);
            data.bestScore = ControllerPlayer.score;          
        }
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void Load()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);          
            bestScore = data.bestScore;
        }
    }
}
