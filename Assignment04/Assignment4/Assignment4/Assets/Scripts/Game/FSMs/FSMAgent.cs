using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Basic FSM Agent Setup
public class FSMAgent : MonoBehaviour
{
    public GhostAnimationHandler normal, frightened, eyes;

    //The currently active state
    public State currState;

    //Timer for internal use
    private float timer;

    //Pathing
    private Vector3[] path;
    private int pathIndex;
    private Vector3 target;
    private bool movingTowardTarget;

    //Speed modifier
    private float speedModifer = 1.0f;

    //Set up initial state
    public virtual void Initialize(){ }

    void Update()
    {
        //Timer Logic
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }

        //Pathing Logic
        if (movingTowardTarget)
        {
            if ((target - GetPosition()).sqrMagnitude < AgentConstants.THRESHOLD)
            {
                movingTowardTarget = false;
                SetPosition(target);

                if (path.Length > pathIndex+1)
                {
                    pathIndex += 1;
                    target = path[pathIndex];
                    movingTowardTarget = true;
                }
            }
            else
            {
                Vector3 potentialNewPosition = GetPosition() + (target - GetPosition()).normalized * Time.deltaTime * AgentConstants.GHOST_SPEED* speedModifer;
                if (ObstacleHandler.Instance.AnyIntersect(new Vector2(GetPosition().x, GetPosition().y), new Vector2(potentialNewPosition.x, potentialNewPosition.y)))
                {
                    movingTowardTarget = false;
                }
                else
                {
                    SetPosition(potentialNewPosition);
                }
            }

        }

        //FSM Logic
        State nextState = currState.Update(this);
        if (!nextState.Name.Equals(currState.Name))
        {
            currState.ExitState(this);
            nextState.EnterState(this);

            currState = nextState;
        }
    }

    //POSITION GETTERS AND SETTERS

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    //YOU ARE NOT ALLOWED TO TELEPORT
    private void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    //TIMER GETTERS AND SETTERS

    public void SetTimer(float timerMax)
    {
        timer = timerMax;
    }

    public bool TimerComplete()
    {
        return timer <= 0;
    }

    //Set target location and begin pathing towards the target
    public void SetTarget(Vector3 _target)
    {
        GraphNode closestStart = HW3NavigationHandler.Instance.NodeHandler.ClosestNode(GetPosition());
        GraphNode closestGoal = HW3NavigationHandler.Instance.NodeHandler.ClosestNode(_target);
        path = HW3NavigationHandler.Instance.PathFinder.CalculatePath(closestStart, closestGoal);

        if (path == null || path.Length <= 1)
        {
            target = _target;
            movingTowardTarget = true;
        }
        else
        {
            pathIndex = 0;
            target = path[0];
            movingTowardTarget = true;
        }

    }

    //Check if this agent is close enough to the target position to interact with it
    public bool CloseEnough(Vector3 position){
        float dist = (GetPosition() - position).sqrMagnitude;
        return dist < 0.01f;
    }

    //ANIMATION STATE SETTERS

    public void SetAnimationStateNormal()
    {
        normal.enabled = true;
        frightened.enabled = false;
        eyes.enabled = false;
    }

    public void SetAnimationStateFrightened()
    {
        normal.enabled = false;
        frightened.enabled = true;
        eyes.enabled = false;
    }

    public void SetAnimationStateEyes()
    {
        normal.enabled = false;
        frightened.enabled = false;
        eyes.enabled = true;
    }

    //SPEED SETTERS

    public void SetSpeedModifierHalf()
    {
        speedModifer = 0.5f;
    }

    public void SetSpeedModifierNormal()
    {
        speedModifer = 1f;
    }

    public void SetSpeedModifierDouble()
    {
        speedModifer = 1.5f;
    }

}
