using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public State CurrentState { get; private set; } = State.PreWave;
    
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
                if (newState != State.PreWave) break;
                CurrentState = newState;
                didChange = true;
                break;
        }
        
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