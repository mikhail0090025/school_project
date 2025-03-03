using UnityEngine;
using TMPro;

public class ScoreUiScript : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text MainLabel;
    void Start()
    {
        
    }

    void Update()
    {
        MainLabel.text = GetText();
    }
    string GetText()
    {
        string result = "";
        int index = 1;
        var player = GameObject.FindGameObjectWithTag("Player").GetComponent<ScoreCounter>();
        result += $"Player: {player.Kills}      {player.FriendKills}        {player.Deaths}         {player.Score()}\n";
        foreach (var bot in FindObjectsOfType<ScoreCounter>())
        {
            if (bot == player) continue;
            result += $"Bot {index}: {bot.Kills}      {bot.FriendKills}        {bot.Deaths}         {bot.Score()}\n";
            index++;
        }

        return result;
    }
}
