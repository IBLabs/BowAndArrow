using TMPro;
using UnityEngine;

public class ScoreEntry : MonoBehaviour
{
    [SerializeField] private TMP_Text rank;
    [SerializeField] private TMP_Text score;

    public void InitEntry(string initRank, string initScore)
    {
        rank.text = initRank;
        score.text = initScore;
    }

    public void InitEntryWithHighLight(string initRank, string initScore)
    {
        rank.text = $"<color=yellow>{initRank}</color>";
        score.text = $"<color=yellow>{initScore}</color>";
    }
}