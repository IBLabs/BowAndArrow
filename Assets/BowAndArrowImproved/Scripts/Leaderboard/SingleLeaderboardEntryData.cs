[System.Serializable]
public struct SingleLeaderboardEntry
{
    public int score;
    
    public SingleLeaderboardEntry(int initScore)
    {
        score = initScore;
    }
}