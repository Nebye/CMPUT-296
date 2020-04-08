using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evaluator : MonoBehaviour
{
    //To be overwritten
    public virtual float EvaluateLevel(Level level)
    {
        return 0;
    }
}
