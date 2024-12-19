using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard_PlayerUI : MonoBehaviour
{
    [SerializeField] TMP_Text playerName;
    [SerializeField] TMP_Text playerScore;
    [SerializeField] TMP_Text playerID;
    public void SetPlayerUI(string name, string score, string id)
    {
        playerName.text = name;
        playerScore.text = score;
        playerID.text = $"ID : {id}";
    }

}
