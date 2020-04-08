public class Datapoint
{
    //actual data
    private string userID;//the user's id
    private int hoursPlayed;//the total hours played
    private int level;//the level of the user
    private int pelletsEaten;//total number of pellets eaten
    private int fruitEaten;//total number of fruit eaten
    private int ghostsEaten;//total number of ghosts eaten
    private float avgScore;//average score per game
    private int maxScore;//max score across all games
    private int totalScore;//total score across all games
    private bool churned;//whether the user churned a week after these values were taken

    //public getters
    public string UserID { get { return userID; } }
    public int HoursPlayed { get { return hoursPlayed; } }
    public int Level { get { return level; } }
    public int PelletsEaten { get { return pelletsEaten; } }
    public int FruitEaten { get { return fruitEaten; } }
    public int GhostsEaten { get { return ghostsEaten; } }
    public float AvgScore { get { return avgScore; } }
    public int MaxScore { get { return maxScore; } }
    public int TotalScore { get { return totalScore; } }
    public bool Churned { get { return churned; } }

    //Test data without churned information (churned defaults to False)
    public Datapoint(string _userID, int _hoursPlayed, int _level, int _pelletsEaten, int _fruitEaten, int _ghostsEaten, float _avgScore, int _maxScore, int _totalScore)
    {
        userID = _userID;
        hoursPlayed = _hoursPlayed;
        level = _level;
        pelletsEaten = _pelletsEaten;
        fruitEaten = _fruitEaten;
        ghostsEaten = _ghostsEaten;
        avgScore = _avgScore;
        maxScore = _maxScore;
        totalScore = _totalScore;
    }

    //Train data with churned information
    public Datapoint(string _userID, int _hoursPlayed, int _level, int _pelletsEaten, int _fruitEaten, int _ghostsEaten, float _avgScore, int _maxScore, int _totalScore, bool _churned)
    {
        userID = _userID;
        hoursPlayed = _hoursPlayed;
        level = _level;
        pelletsEaten = _pelletsEaten;
        fruitEaten = _fruitEaten;
        ghostsEaten = _ghostsEaten;
        avgScore = _avgScore;
        maxScore = _maxScore;
        totalScore = _totalScore;
        churned = _churned;
    }

    //Used in ChurnPredictor.cs to set the predicted churned value
    public void SetPredictedChurn(bool _churned)
    {
        churned = _churned;
    }

    //Test if equivalent
    public override bool Equals(object obj)
    {
        if (obj == null) return false;

        Datapoint otherPnt = obj as Datapoint;
        if (userID.Equals(otherPnt.UserID) && hoursPlayed == otherPnt.hoursPlayed && level == otherPnt.Level && pelletsEaten == otherPnt.PelletsEaten && fruitEaten == otherPnt.FruitEaten && ghostsEaten == otherPnt.GhostsEaten && avgScore == otherPnt.AvgScore && maxScore == otherPnt.MaxScore && totalScore == otherPnt.TotalScore)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //Get hash code for dictionaries
    public override int GetHashCode()
    {
        return userID.GetHashCode() + hoursPlayed.GetHashCode() + level.GetHashCode() + pelletsEaten.GetHashCode() + fruitEaten.GetHashCode() + ghostsEaten.GetHashCode() + avgScore.GetHashCode() + maxScore.GetHashCode() + totalScore.GetHashCode();
    }

    //Get a string representation (useful for debugging)
    public override string ToString()
    {
        return "Datapoint: " + userID + " HoursPlayed: " + hoursPlayed + " Level: " + level + " Pellets Eaten: " + pelletsEaten + " Fruit Eaten: "+fruitEaten+" Ghosts Eaten: "+ghostsEaten+" Average Score: "+avgScore+" Max Score: "+maxScore+" Total Score: "+totalScore;
    }

    //takes string variable names and returns associated value, used for visualization
    public float GetValueByString(string variableName)
    {
        variableName = variableName.ToLower();

        if (variableName.Equals("hoursplayed"))
        {
            return hoursPlayed;
        }
        else if (variableName.Equals("level"))
        {
            return level;
        }
        else if (variableName.Equals("pelletseaten"))
        {
            return pelletsEaten;
        }
        else if (variableName.Equals("fruiteaten"))
        {
            return fruitEaten;
        }
        else if (variableName.Equals("ghostseaten"))
        {
            return ghostsEaten;
        }
        else if (variableName.Equals("avgscore"))
        {
            return avgScore;
        }
        else if (variableName.Equals("maxscore"))
        {
            return maxScore;
        }
        else if (variableName.Equals("totalscore"))
        {
            return totalScore;
        }

        return 0;

    }
}
