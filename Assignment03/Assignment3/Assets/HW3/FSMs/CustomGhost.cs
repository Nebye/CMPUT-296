using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGhost : FSMAgent
{

    void Start()
    {
        Initialize();//remove, this is testing
    }

    public override void Initialize()
    {
        currState = new CustomState01(); // To make it outperform the other ghosts just increase its speed by alot
        currState.EnterState(this);
    }
}
