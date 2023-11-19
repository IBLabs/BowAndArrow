using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    private const string JSON_LEADERBOARD_FILE_NAME = "JsonLeaderboardHolder.json";
    private LeaderboardEntries _leaderboardEntries;

    private IDataHandler _dataHandler = DataHandlerFactory.get();

    [SerializeField] private Scoreboard scoreboard;
    [SerializeField] private GameObject scoreEntriesGrid;
    [SerializeField] private ScoreEntry scoreEntryPrefab;
    [SerializeField] private int numOfEntries = 10;

    private List<ScoreEntry> _scoresEntries = new List<ScoreEntry>();

    private int _myScoreEntryPosition = -1;
    private int _myScore = 0;
    
    public void OnGameManagerStateChanged(GameManager.State newState)
    {
        if (newState == GameManager.State.Lose)
        {
            HandleLoseGameState();
        }
    }

    private void HandleLoseGameState()
    {
        string dataFilePath = Path.Combine(Application.persistentDataPath, JSON_LEADERBOARD_FILE_NAME);
       
        LoadLeaderboardData(dataFilePath);

        _myScore = scoreboard._score;
        SubmitScore(dataFilePath);

        GenerateLeaderboardEntries();
        gameObject.SetActive(true);
    }

    private void LoadLeaderboardData(string dataFilePath)
    {
        if (!File.Exists(dataFilePath))
        {
            CreateLeaderboardDataFile(dataFilePath);
        }

        string dataToRead = File.ReadAllText(dataFilePath);
        _leaderboardEntries = _dataHandler.LoadData<LeaderboardEntries>(dataToRead);
    }

    private void CreateLeaderboardDataFile(string dataFilePath)
    {
        LeaderboardEntries leaderboardData = new LeaderboardEntries
        {
            leaderboard = new List<SingleLeaderboardEntry>()
        };
        
        _dataHandler.SaveData(dataFilePath, leaderboardData);
    }

    private void SubmitScore(string dataFilePath)
    {
        _myScoreEntryPosition = GetMyScoreEntryPosition();
        _leaderboardEntries.leaderboard.Insert(_myScoreEntryPosition, new SingleLeaderboardEntry(_myScore));

        _dataHandler.SaveData(dataFilePath, _leaderboardEntries);
    }

    private int GetMyScoreEntryPosition()
    {
        int position;

        for (position = 0; position < _leaderboardEntries.leaderboard.Count; position++)
        {
            if (_myScore > _leaderboardEntries.leaderboard[position].score) return position;
        }

        return position;
    }

    private void GenerateLeaderboardEntries()
    {
        for (int i = 0; i < numOfEntries && i < _leaderboardEntries.leaderboard.Count; i++)
        {
            string rank = (i + 1).ToString();
            string score = _leaderboardEntries.leaderboard[i].score.ToString();

            ScoreEntry newEntry = Instantiate(scoreEntryPrefab, scoreEntriesGrid.transform);

            if (_myScoreEntryPosition == i)
            {
                newEntry.InitEntryWithHighLight(rank, score);
            }
            else
            {
                newEntry.InitEntry(rank, score);
            }

            _scoresEntries.Add(newEntry);
        }
    }
}