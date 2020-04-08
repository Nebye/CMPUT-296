using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClaustrophobicEvaluator : Evaluator
{
    //This evaluator wants a level to be as open as possible, it will give 0 for a totally covered level and 1 for a totally open level
    public override float EvaluateLevel(Level level)
    {
        float totalSize = 10f;

        float totalArea = 0;
        foreach(ProtoObstacle p in level.obstacles)
        {
            float thisObstacleArea = 0;
            for (int i = 0; i<p.shape.Length; i++)
            {
                if(i== p.shape.Length - 1)
                {
                    thisObstacleArea += p.shape[i].x * p.shape[0].y - p.shape[i].y * p.shape[0].x;
                }
                else
                {
                    thisObstacleArea += p.shape[i].x * p.shape[i + 1].y - p.shape[i].y * p.shape[i + 1].x;
                }
            }
            thisObstacleArea = Mathf.Abs(thisObstacleArea / 2f);
            totalArea += thisObstacleArea;
        }

        float coveredRatio = totalArea / totalSize;
        coveredRatio = Mathf.Clamp(coveredRatio, 0f, 1f);
        return 1.0f-coveredRatio;
    }
}
