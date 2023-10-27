using System;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public State CurrentState { get; private set; } = State.PreWave;
    
    public UnityEvent gameDidReset;
    public UnityEvent waveDidStart;
    public UnityEvent waveDidEnd;

    public UnityEvent<State> stateChanged;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        stateChanged.Invoke(CurrentState);
    }

    public void Reset()
    {
        gameDidReset?.Invoke();
        ChangeState(State.PreWave);
    }
    
    public void Lose()
    {
        waveDidEnd?.Invoke();
        ChangeState(State.Lose);
    }
    
    public void StartWave()
    {
        ChangeState(State.Wave);
        waveDidStart?.Invoke();
    }

    public void EndWave()
    {
        ChangeState(State.PreWave);
        waveDidEnd?.Invoke();
    }

    private void ChangeState(State newState)
    {
        bool didChange = false;
        
        switch (CurrentState)
        {
            case State.PreWave:
                if (newState != State.Wave) break;
                CurrentState = newState;
                didChange = true;
                break;
            
            case State.Wave:
                if (newState != State.PreWave && newState != State.Lose) break;
                CurrentState = newState;
                didChange = true;
                break;
            
            case State.Lose:
                if (newState != State.PreWave) break;
                CurrentState = newState;
                didChange = true;
                break;
        }
        Debug.Log(CurrentState);

        if (didChange) stateChanged.Invoke(CurrentState);
    }

    public enum State 
    {
        PreWave,
        Wave,
        Win,
        Lose
    }
}