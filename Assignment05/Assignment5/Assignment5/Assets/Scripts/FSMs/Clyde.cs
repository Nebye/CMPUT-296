using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clyde : FSMAgent
{

    void Start()
    {
        Initialize();//remove, this is testing
    }

    public override void Initialize()
    {
        currState = new ChaseState();
        currState.EnterState(this);
    }
}
