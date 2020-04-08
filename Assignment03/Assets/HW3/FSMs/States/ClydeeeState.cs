using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ClydeeeState : State
{
    //Set name of this state
    public ClydeeeState() : base("Clydeee") { }

    public override State Update(FSMAgent agent)
    {
        //Handle Following Pacman
        Vector3 pacmanLocation = PacmanInfo.Instance.transform.position;
        Vector3 relativeVector = pacmanLocation - agent.GetPosition(); // the distance between pacman and clyde
        var xpos = relativeVector.x;
        var ypos = relativeVector.y;
        double dist_between = Math.Sqrt(xpos * xpos + ypos * ypos); // should be the diagonal space between pacman and agent
        
        

        if (agent.CloseEnough(pacmanLocation))
        {
            ScoreHandler.Instance.KillPacman();
        }

        //If timer complete or within 8 cells (straight line distance) of Pac-Man, go to Scatter State - BOTTOM LEFT
        if (agent.TimerComplete() || dist_between < 0.16f) // 1 cell = 0.02f
        {
            return new ScatterState(new Vector3(-1*ObstacleHandler.Instance.Width, -1*ObstacleHandler.Instance.Height), this);
        }

        //If Pacman ate a power pellet, go to Frightened State
        if (PelletHandler.Instance.JustEatenPowerPellet)
        {
            return new FrightenedState(this);
        }

        //Follow Pacman if 8 or more cells away
        if (dist_between >= 0.16f)
        {
            agent.SetTarget(pacmanLocation);
        }
        

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

