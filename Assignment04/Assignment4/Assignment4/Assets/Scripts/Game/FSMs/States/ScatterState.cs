using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScatterState : State
{
    //State to return to when completed Scatter State
    private State returnState;

    //Various logic for handling scatter behavior
    private Vector3 scatterPosition;
    private Vector3 realScatterPosition, realInnerPosition;
    private int currHeading = 0;
    private const int EDGE=0, INNER1=1;

    //Set up scatter position, return state, and state name
    public ScatterState(Vector3 _scatterPosition, State _returnState): base("Scatter")
    {
        scatterPosition = _scatterPosition;
        returnState = _returnState;
    }

    public override State Update(FSMAgent agent)
    {
        //Determine if we've killed Pacman
        Vector3 pacmanLocation = PacmanInfo.Instance.transform.position;
        if (agent.CloseEnough(pacmanLocation))
        {
            ScoreHandler.Instance.KillPacman();
        }

        //If we're done scattering set up return state
        if (agent.TimerComplete())
        {
            return returnState;
        }

        //Handle Pacman eating power pellet and transitioning to Frightened State
        if (PelletHandler.Instance.JustEatenPowerPellet)
        {
            return new FrightenedState(this);
        }

        //Handle scatter state movement logic
        if (currHeading==EDGE)
        {
            if (agent.CloseEnough(realScatterPosition))
            {
                currHeading = INNER1;

                agent.SetTarget(realInnerPosition);
            }
        }
        else if (currHeading == INNER1)
        {
            if (agent.CloseEnough(realInnerPosition))
            {
                currHeading = EDGE;

                agent.SetTarget(realScatterPosition);
            }
        }

        //Stay in state
        return this;
    }

    //Upon entering state set timer and calculate scatter positions
    public override void EnterState(FSMAgent agent)
    {
        agent.SetTimer(7f);

        GraphNode g = HW3NavigationHandler.Instance.NodeHandler.ClosestNode(scatterPosition);
        realScatterPosition = g.Location;
        agent.SetTarget(realScatterPosition);
        Vector3 innerPosition = Vector3.Lerp(Vector3.zero, realScatterPosition, 0.8f);
        realInnerPosition = HW3NavigationHandler.Instance.NodeHandler.ClosestNode(innerPosition+ Vector3.left*realScatterPosition.x/3+Vector3.down* realScatterPosition.y/6).Location;
    }

    public override void ExitState(FSMAgent agent)
    {
        base.ExitState(agent);
    }
}
