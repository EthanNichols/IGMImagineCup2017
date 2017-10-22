using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public int score = 0;
    public int totalscore = 0;

    public int hits = 0;
    public int maxHits = 3;

    public int difficulty;

    private GameObject health;
    private GameObject scoreIndicator;

    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(gameObject);
        health = transform.Find("HealthBarBase").Find("HealthBar").gameObject;
        scoreIndicator = transform.Find("MoneyText").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        health.GetComponent<Image>().fillAmount = (float)(maxHits - hits) / (float)maxHits;
        scoreIndicator.GetComponent<Text>().text = score.ToString();

        difficulty = (int)(totalscore / 150);

        if (hits >= maxHits)
        {
            SceneManager.LoadScene("GameOver");
            hits = 0;
        }

        GameObject finalScore = GameObject.FindGameObjectWithTag("FinalScore");

        if (finalScore != null)
        {
            finalScore.GetComponent<Text>().text = "Final Score:\n" + totalscore.ToString();
            Destroy(gameObject);
        }

        foreach(Transform child in transform)
        {
            if (child.gameObject.name.Contains("Tut"))
            {
                child.gameObject.GetComponent<Text>().color -= new Color(0, 0, 0, .005f);
            }
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        totalscore += amount;
    }
}
