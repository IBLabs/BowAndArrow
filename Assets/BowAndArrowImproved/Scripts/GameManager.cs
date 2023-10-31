using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public enum State
    {
        PreWave,
        Wave,
        Win,
        Lose
    }

    public static GameManager instance;

    public UnityEvent waveDidStart;
    public UnityEvent waveDidEnd;

    public UnityEvent<State> stateChanged;
    public State CurrentState { get; private set; } = State.PreWave;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void OnEnable()
    {
        stateChanged.Invoke(CurrentState);
    }

    public void Lose()
    {
        ChangeState(State.Lose);
        waveDidEnd?.Invoke();
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
        var didChange = false;

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
                break;
        }

        if (didChange) stateChanged.Invoke(CurrentState);
    }
}