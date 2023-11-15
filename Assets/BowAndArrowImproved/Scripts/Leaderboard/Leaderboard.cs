using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    private const string JsonLeaderBoardFileName = "JsonLeaderboardHolder.json";
    private LeaderboardEntries _leaderboardEntries;

    private IDataLoader _leaderboardLoader = new JsonDataLoader();
    private IDataSaver _leaderboardSaver = new JsonDataSaver();

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
            LoadLeaderboardData();
            
            _myScore = scoreboard._score;
            SubmitScore();

            GenerateLeaderboardEntries();
            gameObject.SetActive(true);
        }
    }

    private int GetMyScoreEntryPosition()
    {
        int position;

        for (position = 0; position < _leaderboardEntries.leaderboard.Count; position++)
        {
            if (_myScore >= _leaderboardEntries.leaderboard[position].score) break;
        }

        return position;
    }

    private void SubmitScore()
    {
        _myScoreEntryPosition = GetMyScoreEntryPosition();
        _leaderboardEntries.leaderboard.Insert(_myScoreEntryPosition, new SingleLeaderboardEntry(_myScore));
        SaveLeaderboardData();
    }

    private void GenerateLeaderboardEntries()
    {
        for (int i = 0; i < numOfEntries; i++)
        {
            string rank = (i + 1).ToString();
            string score = (i < _leaderboardEntries.leaderboard.Count)
                ? _leaderboardEntries.leaderboard[i].score.ToString()
                : "";

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

    private void LoadLeaderboardData()
    {
        string filePath = Path.Combine(Application.persistentDataPath, JsonLeaderBoardFileName);

        if (!File.Exists(filePath))
        {
            LeaderboardEntries leaderboardData = new LeaderboardEntries
            {
                leaderboard = new List<SingleLeaderboardEntry>()
            };

            string jsonDataToWrite = JsonUtility.ToJson(leaderboardData);

            File.WriteAllText(filePath, jsonDataToWrite);
        }

        string jsonDataToRead = File.ReadAllText(filePath);
        _leaderboardEntries = _leaderboardLoader.LoadData<LeaderboardEntries>(jsonDataToRead);
    }

    private void SaveLeaderboardData()
    {
        _leaderboardSaver.SaveData(JsonLeaderBoardFileName, _leaderboardEntries);
    }
}