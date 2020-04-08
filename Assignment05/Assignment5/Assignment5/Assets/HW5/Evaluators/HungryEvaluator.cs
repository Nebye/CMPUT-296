using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungryEvaluator : Evaluator
{
    //Returns 0 if a level has no pellets, returns 1 if a level has 350 pellets or more
    public override float EvaluateLevel(Level level)
    {
        float percentagePellets = ((float)level.pellets.Count) / 350f;

        percentagePellets = Mathf.Clamp(percentagePellets, 0f, 1f);

        return percentagePellets;
    }
}
