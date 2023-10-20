using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public State CurrentState { get; private set; } = State.PreWave;
    public UnityEvent waveDidStart;
    public UnityEvent waveDidEnd;
    
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
        waveDidStart?.Invoke();
        ChangeState(State.Wave);
    }
    public void EndWave()
    {
        waveDidEnd?.Invoke();
        ChangeState(State.PreWave);
    }

    private bool ChangeState(State newState)
    {
        bool didChange = false;
        switch (CurrentState)
        {
            case State.PreWave:
                if (newState != State.Wave &&
                    newState != State.Win) break;
                CurrentState = newState;
                didChange = true;
                break;
            
            case State.Wave:
                if (newState != State.PreWave &&
                    newState != State.Lose) break;
                CurrentState = newState;
                didChange = true;
                break;
        }
        
        if (didChange) OnStateChange();
        
        return didChange;
    }
    
    private void OnStateChange()
    {
        switch (CurrentState)
        {
            case State.PreWave:
                break;
            case State.Wave:
                break;
            case State.Win:
                break;
            case State.Lose:
                break;
        }
    }
    
    public enum State 
    {
        PreWave,
        Wave,
        Win,
        Lose
    }
}