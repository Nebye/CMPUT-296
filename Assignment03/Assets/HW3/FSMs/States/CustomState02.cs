using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomState02 : State
{
    //Makes ghost slower

    //State to return to once complete
    private State returnState;

    //Constructor, set up name of state
    public CustomState02(State _returnState) : base("Slow")
    {
        returnState = _returnState;
    }

    public override State Update(FSMAgent agent)
    {
        Vector3 pacmanLocation = PacmanInfo.Instance.transform.position; // can still kill pacman
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

    //Upon entering state, start a timer, set speed modifier to half, and set up animations
    public override void EnterState(FSMAgent agent)
    {
        agent.SetSpeedModifierDouble();
        agent.SetTimer(12);
    }

    //Upon exiting state, set speed and animation state to normal
    public override void ExitState(FSMAgent agent)
    {
        agent.SetAnimationStateNormal();
        agent.SetSpeedModifierNormal();
    }
}
