using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool isGameStarted;
    public int score;
    public Text scoreText;
    public Text highScoreText;
    private void Awake()
    {
        highScoreText.text = "Best:" + GetHighScore().ToString();
    }
    public void StartGame()
    {
        isGameStarted = true;
        FindObjectOfType<Road>().StartBuilding();
        Debug.Log("onreo[ing");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            StartGame();
    }
    public void EndGame()
    {
        SceneManager.LoadScene(0);
    }
    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
        if(score > GetHighScore())
        {
            PlayerPrefs.SetInt("Highscore", score);
        }
        highScoreText.text = "Best: " + score.ToString();
    }
    public int GetHighScore()
    {
        int i = PlayerPrefs.GetInt("Highscore");
        return i;
    }
}
