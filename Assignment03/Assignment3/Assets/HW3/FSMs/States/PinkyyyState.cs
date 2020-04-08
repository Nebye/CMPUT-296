using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkyyyState : State
{
    //Set name of this state
    public PinkyyyState() : base("Pinkyyy") { }


    public override State Update(FSMAgent agent)
    {
        //Handle Following Pacman
        Vector3 pacmanLocation = PacmanInfo.Instance.transform.position; // pacman current position

        pacmanLocation = pacmanLocation + PacmanInfo.Instance.Facing * 0.8f; // calculates 4 spaces ahead of pacman


        if (agent.CloseEnough(pacmanLocation))
        {
            ScoreHandler.Instance.KillPacman();
        }

        //If timer complete, go to Scatter State
        if (agent.TimerComplete())
        {
            return new ScatterState(new Vector3(-1*ObstacleHandler.Instance.Width, ObstacleHandler.Instance.Height), this);
        }

        //If Pacman ate a power pellet, go to Frightened State
        if (PelletHandler.Instance.JustEatenPowerPellet)
        {
            return new FrightenedState(this);
        }
        //If we didn't return follow Pacman
        agent.SetTarget(pacmanLocation); // will target 4 spaces ahead of pacman

        //Stay in this state
        return this;
    }

    //Upon entering state, set timer to enter Scatter State
    public override void EnterState(FSMAgent agent)
    {
        agent.SetTimer(20f);
    }

    public override void ExitState(FSMAgent agent) { }
}
