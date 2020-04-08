using System.Collections;
using System.Collections.Generic;

//Basic State Representation
public class State
{
    private string stateName;//The name of this state, set in the constructor
    public string Name { get { return stateName; } }

    public State(string _stateName)
    {
        this.stateName = _stateName;
    }

    //Handle each tick of behavior while in this state
    public virtual State Update(FSMAgent agent)
    {
        return this;
    }

    //Handle logic on entering this state
    public virtual void EnterState(FSMAgent agent) { }

    //Handle logic on exiting this state
    public virtual void ExitState(FSMAgent agent) { }
}
