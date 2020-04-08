using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inky : FSMAgent
{

    void Start()
    {
        Initialize();//remove, this is testing
    }

    public override void Initialize()
    {
        currState = new WhimsicalState();
        currState.EnterState(this);
    }
}