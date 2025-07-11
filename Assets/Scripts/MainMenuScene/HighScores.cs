using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using Dan.Main;

public class HighScores : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> Names;
    [SerializeField] private List<TextMeshProUGUI> Scores;
    private string publicKey = "188a1985629b8990859fd1efbb17be6fbd2858dfa40baa5fdaa5dbb8ff08e1e9";

    public void GetLeaderboard()
    {
        LeaderboardCreator.GetLeaderboard(publicKey, ((msg) =>
        {
            int loopLength = (msg.Length < Names.Count) ? msg.Length : Names.Count; 
            for(int i = 0; i < loopLength ; i++)
            {
                Names[i].text = msg[i].Username;
                Scores[i].text = msg[i].Score.ToString() ;
            }
        }));
    }
    public void SetLeaderBoard(string name , int score)
    {
        LeaderboardCreator.UploadNewEntry(publicKey, name, score, ((msg) => { GetLeaderboard();  }));
        
    }
    private void Start()
    {
        GetLeaderboard();
    }

}
