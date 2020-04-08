using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhimsicalState : State
{
    //Private reference to the blinky ghost, if it exists
    private Blinky blinky;

    //Set up name of state
    public WhimsicalState() : base("Whimsical") { }
    
    public override State Update(FSMAgent agent)
    {

        //Check if you killed pacman
        Vector3 pacmanLocation = PacmanInfo.Instance.transform.position;
        if (agent.CloseEnough(pacmanLocation))
        {
            ScoreHandler.Instance.KillPacman();
        }

        //Check if timer completes and should transition to scatter state
        if (agent.TimerComplete())
        {
            return new ScatterState(new Vector3(ObstacleHandler.Instance.Width, -1*ObstacleHandler.Instance.Height), this);
        }

        //Check if Pacman ate a power pellet and we should become frightened
        if (PelletHandler.Instance.JustEatenPowerPellet)
        {
            return new FrightenedState(this);
        }

        //Handle movement logic
        pacmanLocation += PacmanInfo.Instance.Facing * 0.4f;

        if (blinky != null)
        {
            Vector3 relativeVector = pacmanLocation - blinky.GetPosition();
            pacmanLocation = blinky.GetPosition() + relativeVector * 2;
        }

        agent.SetTarget(pacmanLocation);

        //Stay in state
        return this;
    }

    //EnterState Start timer for scatter state and find the blinky ghost if it exists
    public override void EnterState(FSMAgent agent)
    {
        agent.SetTimer(20f);
        blinky = GameObject.FindObjectOfType<Blinky>();
    }

    public override void ExitState(FSMAgent agent) { }
}
