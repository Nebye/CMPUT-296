using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataVisualizer : MonoBehaviour
{
    //Visualization stuff
    public TextAsset trainingData, verificationData;
    public GameObject trueDatapoint, falseDatapoint;
    //Current visualization
    private List<DatapointInfo> currDatapointVisualization;
    //This round's clusters
    private Dictionary<Datapoint, List<Datapoint>> clustersByCenters;
    //Training, and verification data
    private Datapoint[] trainingPoints, verificationPointsTest, verificationPointsTestWithChurn;

    //Clusters used for the visualization
    public Color32[] clusterColors = new Color32[] { new Color32(250, 230, 190, 200), new Color32(160, 250, 130, 200), new Color32(240, 240, 50, 200), new Color32(250, 120, 80, 200), new Color32(170, 10, 60, 200), new Color32(20, 210, 220, 200), new Color32(20, 210, 220, 200), new Color32(250, 120, 250, 200), new Color32(0, 90, 200, 200), new Color32(130, 20, 160, 200) };
    public string variableX, variableY;

    // Start is called before the first frame update
    void Start()
    {
        trainingPoints = GetDatapointsFromCSV(trainingData, true);
        verificationPointsTest = GetDatapointsFromCSV(verificationData, false);
        verificationPointsTestWithChurn = GetDatapointsFromCSV(verificationData, true);

        //Cluster everything
        KClusterer kClusterer = new KClusterer();
        clustersByCenters = kClusterer.Cluster(trainingPoints);

    }

    //Runs if "Check Accuracy" is hit. 
    public void RunChurnPrediction()
    {
        //Predict on verification data
        ChurnPredictor churn = new ChurnPredictor();
        Datapoint[] predictedPoints = churn.AssignPredictedChurn(verificationPointsTest, clustersByCenters);

        //Calculate Validation Accuracy
        float score = 0;
        for (int i = 0; i < predictedPoints.Length; i++)
        {
            if (predictedPoints[i].Churned == verificationPointsTestWithChurn[i].Churned)
            {
                score += 1;
            }
        }
        score /= ((float)predictedPoints.Length);

        Debug.Log("Accuracy: " + score);
    }

    //Public visualize call
    public void Visualize()
    {
        if (variableX.Length > 0 && variableY.Length > 0)
        {
            VisualizeDatapoints(variableX, variableY);
        }
    }

    //Visualize datapoints in terms of clusters and the associated variables
    private void VisualizeDatapoints(string variable1, string variable2)
    {
        //Find the maximum values
        float maxVal1 = 0;
        float maxVal2 = 0;

        foreach(Datapoint d in trainingPoints)
        {
            if (d.GetValueByString(variable1) > maxVal1)
            {
                maxVal1 = d.GetValueByString(variable1);
            }
            if (d.GetValueByString(variable2) > maxVal2)
            {
                maxVal2 = d.GetValueByString(variable2);
            }
        }

        //Remove existing visualized datapoints
        if (currDatapointVisualization != null)
        {
            foreach (DatapointInfo d in currDatapointVisualization)
            {
                Destroy(d.gameObject);
            }
        }
        currDatapointVisualization = new List<DatapointInfo>();

        //Visualizing everything
        int currCluster = 0;
        foreach(KeyValuePair<Datapoint, List<Datapoint>> kvp in clustersByCenters)
        {
            foreach(Datapoint d in kvp.Value)
            {
                GameObject datapointObj = null;
                if (d.Churned)
                {
                    datapointObj = GameObject.Instantiate(trueDatapoint);
                }
                else
                {
                    datapointObj = GameObject.Instantiate(falseDatapoint);
                }

                DatapointInfo pntInfo = datapointObj.GetComponent<DatapointInfo>();
                pntInfo.Initialize(d, clusterColors[currCluster]);
                currDatapointVisualization.Add(pntInfo);

                float normalizedVal1 = d.GetValueByString(variable1) / maxVal1;
                float normalizedVal2 = d.GetValueByString(variable2) / maxVal2;

                datapointObj.transform.position = new Vector3(-3 + normalizedVal1 * 6, -1.5f + normalizedVal2 * 6);
                datapointObj.name = d.UserID;
            }
            currCluster += 1;
        }
    }

    //Calculates out the distortion
    public void CalculateDistortion()
    {
        float distortion = 0;
        foreach (KeyValuePair<Datapoint, List<Datapoint>> kvp in clustersByCenters)
        {
            foreach(Datapoint d in kvp.Value)
            {
                distortion += KClusterer.Distance(kvp.Key, d);
            }
        }
        distortion /= ((float)clustersByCenters.Keys.Count);
        Debug.Log("Total Distortion: " + distortion);
    }

    //Load in Datapoints
    private Datapoint[] GetDatapointsFromCSV(TextAsset file, bool readChurn)
    {
        string[] lines = file.text.Split('\n');
        List<Datapoint> datapoints = new List<Datapoint>();
        foreach(string line in lines)
        {
            string[] splitRow = line.Split(',');

            if (!splitRow[0].Contains("userID") && splitRow.Length>1)
            {
                
                string userID = splitRow[0];
                int hoursPlayed = int.Parse(splitRow[1]);
                int level = int.Parse(splitRow[2]);
                int pelletsEaten = int.Parse(splitRow[3]);
                int fruitEaten = int.Parse(splitRow[4]);
                int ghostsEaten = int.Parse(splitRow[5]);
                float avgScore = float.Parse(splitRow[6]);
                int maxScore = int.Parse(splitRow[7]);
                int totalScore = int.Parse(splitRow[8]);
                bool churn = false;
                if (splitRow[9].Contains("T"))
                {
                    churn = true;
                }

                Datapoint d = null;
                if (readChurn)
                {
                    d = new Datapoint(userID, hoursPlayed, level, pelletsEaten, fruitEaten, ghostsEaten, avgScore, maxScore, totalScore, churn);
                }
                else
                {
                    d = new Datapoint(userID, hoursPlayed, level, pelletsEaten, fruitEaten, ghostsEaten, avgScore, maxScore, totalScore);
                }

                datapoints.Add(d);
            }
        }

        return datapoints.ToArray();
    }
}
