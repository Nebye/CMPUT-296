using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomState03 : State
{
    ///Makes ghost aim for one cell behind pacman

    //State to return to once complete
    private State returnState;

    //Constructor, set up name of state
    public CustomState03(State _returnState) : base("OneStepAhead")
    {
        returnState = _returnState;
    }

    public override State Update(FSMAgent agent)
    {
        //Handle Following Pacman
        Vector3 pacmanLocation = PacmanInfo.Instance.transform.position;
        pacmanLocation = pacmanLocation + PacmanInfo.Instance.Facing * -0.02f; // should make the agent aim for one cell behind pacman
        if (agent.CloseEnough(pacmanLocation))
        {
            ScoreHandler.Instance.KillPacman();
        }


        agent.SetTarget(pacmanLocation); // still targets pacman

        if (agent.TimerComplete())
        {
            return returnState;
        }

        agent.SetSpeedModifierHalf();
        return this;
    }

    //Upon entering state, set timer to enter Scatter State
    public override void EnterState(FSMAgent agent)
    {
        agent.SetTimer(20f);
    }

    public override void ExitState(FSMAgent agent) { }
}
