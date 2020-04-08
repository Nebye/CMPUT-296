using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChurnPredictor
{
    //TODO; FIX THIS FUNCTION FOR EXTRA CREDIT, TEST WITH "Check Accuracy" BUTTON
    public Datapoint[] AssignPredictedChurn(Datapoint[] verificationDatapoints, Dictionary<Datapoint, List<Datapoint>> clustersByCenters)
    {
        foreach(Datapoint d in verificationDatapoints)
        {
            d.SetPredictedChurn(true);
        }
        
        return verificationDatapoints;         
    }
}
