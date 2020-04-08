using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyesState : State
{
    //State to return to
    private State returnState;

    //Set name of state and returnState value
    public EyesState(State _returnState) : base("Eyes")
    {
        returnState = _returnState;
    }

    //Check if we are close enough to start position, if so return to returnState
    public override State Update(FSMAgent agent)
    {
        if (agent.CloseEnough(AgentConstants.GHOST_START_POS))
        {
            return returnState;
        }

        //Stay in this state
        return this;
    }

    //Upon entering state set up modification, speed change, and set our target
    public override void EnterState(FSMAgent agent)
    {
        agent.SetAnimationStateEyes();
        agent.SetSpeedModifierDouble();
        agent.SetTarget(AgentConstants.GHOST_START_POS);
    }

    //Upon exiting state reset speed and animation state
    public override void ExitState(FSMAgent agent)
    {
        agent.SetAnimationStateNormal();
        agent.SetSpeedModifierNormal();
    }
}
