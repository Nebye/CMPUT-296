using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleLoverEvaluator : Evaluator
{
    //This evaluator wants a level to be as covered in obstacles as possible while not covering pacman, it will give 0 for a totally open level and 1 for a totally closed level (besides Pacman's position)
    public override float EvaluateLevel(Level level)
    {
        float totalSize = 12f;

        float totalArea = 0;
        foreach (ProtoObstacle p in level.obstacles)
        {
            if (p.worldLocation == level.pacmanStartPos)
            {
                return 0;
            }

            float thisObstacleArea = 0;
            for (int i = 0; i < p.shape.Length; i++)
            {
                if (i == p.shape.Length - 1)
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
        totalArea += 0.4f;//Must not have covered pacman
        float coveredRatio = totalArea / totalSize;
        coveredRatio = Mathf.Clamp(coveredRatio, 0f, 1f);
        return coveredRatio;
    }
}
