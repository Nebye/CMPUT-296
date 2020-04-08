using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class used for visualizing datapoints
public class DatapointInfo : MonoBehaviour
{
    [SerializeField]
    private string userID;//the user's id
    [SerializeField]
    private int hoursPlayed;//the total hours played
    [SerializeField]
    private int level;//the level of the user
    [SerializeField]
    private int pelletsEaten;//total number of pellets eaten
    [SerializeField]
    private int fruitEaten;//total number of fruit eaten
    [SerializeField]
    private int ghostsEaten;//total number of ghosts eaten
    [SerializeField]
    private float avgScore;//average score per game
    [SerializeField]
    private int maxScore;//max score across all games
    [SerializeField]
    private int totalScore;//total score across all games
    [SerializeField]
    private bool churned;//whether the user churned a week after these values were taken

    public SpriteRenderer spriteRenderer;

    public void Initialize(Datapoint myDatapoint, Color32 color)
    {
        userID = myDatapoint.UserID;
        hoursPlayed = myDatapoint.HoursPlayed;
        level = myDatapoint.Level;
        pelletsEaten = myDatapoint.PelletsEaten;
        fruitEaten = myDatapoint.FruitEaten;
        ghostsEaten = myDatapoint.GhostsEaten;
        avgScore = myDatapoint.AvgScore;
        maxScore = myDatapoint.MaxScore;
        totalScore = myDatapoint.TotalScore;
        churned = myDatapoint.Churned;

        spriteRenderer.color = color;
    }
}
