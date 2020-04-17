using System.Collections;
using System.Collections.Generic;

public interface IState
{
    void Enter(GameObject self);
    void Execute();
    void Exit();
}

public class StateMachine
{
    IState currentState;
    string currentState_name;
    Dictionary<string, IState> states;
    List<string> blocked_states = new List<string>();

    public StateMachine()
    {
        states = new Dictionary<string, IState>();
    }
    
    public void ChangeState(string newState_name,GameObject self)
    {
        IState newState = states[newState_name];
        if(newState == null)
        {
            return;
        }
        if (newState == currentState)
            return;
        if (blocked_states.Contains(newState_name))
            return;
        if (currentState != null)
            currentState.Exit();
        
        currentState = newState;
        currentState_name = newState_name;

        currentState.Enter(self);
    }

    public void Update()
    {
        if (currentState != null) currentState.Execute();
    }
    //Extras
    public void addState(string name, IState state)
    {
        if (states.Count > 0)
        {
            if (states.ContainsKey(name))
            {
                return;
            }
        }
        states.Add(name, state);

    }
    public bool hasState(string newState_name)
    {
        return states.ContainsKey(newState_name);
    }
    public string getState()
    {
        return currentState_name;
    }
    public IState getStateClass()
    {
        return currentState;
    }
    public bool isOnState(string state)
    {
        return state == currentState_name;
    }
    public void blockState(string state)
    {
        if(!blocked_states.Contains(state))
            blocked_states.Add(state);
    }
    public void unblockState(string state)
    {
        blocked_states.Remove(state);
    }
}
