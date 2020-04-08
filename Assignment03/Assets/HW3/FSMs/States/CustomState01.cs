using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomState01 : State
{
    //Set name of this state
    public CustomState01() : base("Cus01") { }
    int state_switch = 0; // switches between timers as well states performed when those timers end, very tricky to work against but not entirely outside of a potential ghost for pacman

    public override State Update(FSMAgent agent)
    {
        //Handle Following Pacman
        Vector3 pacmanLocation = PacmanInfo.Instance.transform.position;

        if (agent.CloseEnough(pacmanLocation))
        {
            ScoreHandler.Instance.KillPacman();
        }

        //If timer complete, go to Scatter State
        if (agent.TimerComplete() && state_switch%2 == 0)
        {
            return new ScatterState(new Vector3(ObstacleHandler.Instance.Width/2, ObstacleHandler.Instance.Height/2), this);
            state_switch += 1; 
        }
        if (agent.TimerComplete() && state_switch % 2 != 0)
        {
            return new CustomState03(this); 
            state_switch += 1;
        }

        //If Pacman ate a power pellet, go to CustomState02 State
        if (PelletHandler.Instance.JustEatenPowerPellet)
        {
            return new CustomState02(this);
        }

        // targets pacman directly
        agent.SetTarget(pacmanLocation);

        //Stay in this state
        return this;
    }

    //Upon entering state, set timer to enter Scatter State
    public override void EnterState(FSMAgent agent)
    {
        if (state_switch % 2 == 0)
            {
                agent.SetTimer(20f);
            }
        else
            {
                agent.SetTimer(10f);
            }
        
    }

    public override void ExitState(FSMAgent agent) { }
}
