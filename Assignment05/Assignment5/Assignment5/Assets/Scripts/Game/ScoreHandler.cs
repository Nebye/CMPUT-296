using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreHandler : MonoBehaviour
{
    public static ScoreHandler Instance;

    public Text scoreText;
    private int score = 0;

    private const string BASIC_SCORE_TEXT = "Pellets: ";

    void Start()
    {
        Instance = this;
    }

    public void UpdateScore()
    {
        score += 1;
        scoreText.text = BASIC_SCORE_TEXT+score;
    }

    public void KillPacman()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
