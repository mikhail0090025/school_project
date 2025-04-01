using UnityEngine;
using System.Collections.Generic;
using TMPro;
using NUnit.Framework;

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
        var collection = new List<ScoreCounter>(FindObjectsByType<ScoreCounter>(FindObjectsSortMode.None));
        collection.Sort((a, b) => b.Score().CompareTo(a.Score())); // Сортировка по убыванию
        foreach (var bot in collection)
        {
            if (bot == player) continue;
            result += $"Bot {index}: {bot.Kills}      {bot.FriendKills}        {bot.Deaths}         {bot.Score()}\n";
            index++;
        }

        return result;
    }
}
