using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    public GameManagerState currentState { get; private set; } = GameManagerState.PreWave;
    public static Action WaveDidStart;
    public static Action WaveDidEnd;

    [SerializeField] private PreWaveBallon preWaveBallon; 
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        // Initialize the game, show the PreWaveBallon.
        ShowPreWaveBalloon();
    }
    
    public void StartWave()
    {
        WaveDidStart?.Invoke();
        ChangeState(GameManagerState.Wave);
    }
    public void EndWave()
    {
        WaveDidEnd?.Invoke();
        ChangeState(GameManagerState.PreWave);
    }

    private void ShowPreWaveBalloon()
    {
        preWaveBallon.Show();
    }
    
    private void OnEnable()
    {
        Subscribe();
    }

    private void OnDisable()
    {
        Unsubscribe();   
    }
    
    private void Subscribe()
    {
        // BAAIWaveSpawner.WaveDidEnd += OnWaveDidEnd;
    }

    public void OnWaveDidEnd()
    {
        ChangeState(GameManagerState.PreWave);
    }

    private void Unsubscribe()
    {
    }

    private bool ChangeState(GameManagerState newState)
    {
        bool didChange = false;
        switch (currentState)
        {
            case GameManagerState.PreWave:
                if (newState != GameManagerState.Wave &&
                    newState != GameManagerState.Win) break;
                currentState = newState;
                didChange = true;
                break;
            
            case GameManagerState.Wave:
                if (newState != GameManagerState.PreWave &&
                    newState != GameManagerState.Lose) break;
                currentState = newState;
                didChange = true;
                break;
        }
        
        if (didChange) OnStateChange();
        
        return didChange;
    }
    
    private void OnStateChange()
    {
        switch (currentState)
        {
            case GameManagerState.PreWave:
                ShowPreWaveBalloon();
                break;
            case GameManagerState.Wave:
                break;
            case GameManagerState.Win:
                break;
            case GameManagerState.Lose:
                break;
        }
    }
    
    public enum GameManagerState 
    {
        PreWave,
        Wave,
        Win,
        Lose
    }
}