using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public enum States 
    {
        Scouting,
        Firing
    }
    public class State
    {
        public States id;
        public System.Action onFrame;
        public System.Action onEnter;
        public System.Action onExit;

        public override string ToString()
        {
            return id.ToString();
        }
    }

    Dictionary<States, State> states = new Dictionary<States, State>();

    public State currentState { get; private set; }

    public State initialState;

    public State CreateState(States id)
    {
        var newState = new State();
        newState.id = id;

        if (states.Count == 0)
        {
            initialState = newState;
        }

        states[id] = newState;
        return newState;
    }

    public void Update()
    {
        if (states.Count == 0 || initialState == null)
        {
            Debug.LogError($"State machine has no states!");
            return;
        }

        if (currentState == null)
        {
            TransitionTo(initialState);
        }

        if (currentState.onFrame != null)
        {
            currentState.onFrame();
        }
    }

    public void TransitionTo(State newState)
    {
        if (newState == null)
        {
            Debug.LogError($"Cannot transition to a null state!");
            return;
        }

        if (currentState != null && currentState.onExit != null)
        {
            currentState.onExit();
        }

        // Debug.Log($"Transitioning from '{currentState}' to '{newState}'");
        currentState = newState;

        if (newState.onEnter != null)
        {
            newState.onEnter();
        }
    }

    public void TransitionTo(States id)
    {
        if (states.ContainsKey(id) == false)
        {
            Debug.LogError($"State machine doesn't contain a state named {id}!");
            return;
        }

        var newState = states[id];
        TransitionTo(newState);
    }

}
