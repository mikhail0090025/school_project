using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using static UnityEditor.Experimental.GraphView.GraphView;
[RequireComponent(typeof(MapPoints))]
public class TeamsSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> BotPrefabs;
    [SerializeField] List<Team> Teams;
    [SerializeField] int PlayersTeamIndex;
    [SerializeField] Transform Player;
    [SerializeField] bool FromGameSettings;
    [Header("For debug")]
    [SerializeField] int team_size_in_debug = 25;
    [SerializeField] BotsDifficulty bots_difficulty_in_debug = BotsDifficulty.Medium;
    // PRIVATE
    TMPro.TMP_Text TeamsLabel;
    MapPoints mp;
    void Start()
    {
        TeamsLabel = GameObject.Find("TeamsLabel").GetComponent<TMPro.TMP_Text>();
        if (Application.isEditor)
        {
            //FromGameSettings = false;
        }
        mp = GetComponent<MapPoints>();
        if (FromGameSettings)
        {
            if (Teams.Count != 2) Debug.Log("If you get team data from new game settings, you can have only 2 teams (no more nor less)");
            Teams[0].Size = NewGameSettings.Team1Size;
            Teams[1].Size = NewGameSettings.Team2Size;
            Teams[0].botsDifficulty = (BotsDifficulty)NewGameSettings.BotsDifficulty;
            Teams[1].botsDifficulty = (BotsDifficulty)NewGameSettings.BotsDifficulty;
            if(NewGameSettings.Team1Size == 0)
            {
                Teams[0].Size = team_size_in_debug;
                Teams[1].Size = team_size_in_debug;
                Teams[0].botsDifficulty = bots_difficulty_in_debug;
                Teams[1].botsDifficulty = bots_difficulty_in_debug;
            }
        }
        foreach (var team in Teams)
        {
            int sqrt = Mathf.RoundToInt(Mathf.Sqrt(team.Size));
            for (int i = 0; i < team.Size; i++)
            {
                var spawned = Instantiate(BotPrefabs[UnityEngine.Random.Range(0, BotPrefabs.Count)], team.Spawnpoint.position + new Vector3((i / sqrt), 0,i % sqrt), Quaternion.identity);
                var bot_script = spawned.GetComponent<BotScript>();
                spawned.transform.Find("PlayerPoint").GetComponent<Renderer>().material.color = team.TeamColor;
                spawned.transform.Find("Quad").GetComponent<Renderer>().material.color = team.TeamColor;
                //MeshRenderer
                bot_script.BotsColor = team.TeamColor;
                team.Players.Add(spawned);
                spawned.name = "BotTeam" + (Teams.IndexOf(team) + 1).ToString();
            }
            if (Teams.IndexOf(team) == PlayersTeamIndex) team.Players.Add(Player.gameObject);
        }
        foreach (var team1 in Teams)
        {
            foreach (var team2 in Teams)
            {
                if (team1 == team2) continue;
                foreach (var bot1 in team1.Players)
                {
                    foreach (var bot2 in team2.Players)
                    {
                        var bs = bot1.GetComponent<BotScript>();
                        if(bs)
                        {
                            bs.AddTarget(bot2.transform);
                            switch (team1.botsDifficulty)
                            {
                                case BotsDifficulty.Easy:
                                    bs.ShootSpread = 20f;
                                    break;
                                case BotsDifficulty.Medium:
                                    bs.ShootSpread = 15f;
                                    break;
                                case BotsDifficulty.Hard:
                                    bs.ShootSpread = 9f;
                                    break;
                                case BotsDifficulty.VeryHard:
                                    bs.ShootSpread = 3f;
                                    break;
                            }
                        }
                    }
                    //if(Teams.IndexOf(team1) != PlayersTeamIndex) bot1.GetComponent<BotScript>().AddTarget(Player);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        TeamsLabel.text = "";
        foreach (var item in Teams)
        {
            int teamIndex = Teams.IndexOf(item);
            TeamsLabel.text += $"Team {teamIndex + 1}: {item.CurrentSize}/{item.Size} {item.botsDifficulty} {(PlayersTeamIndex == teamIndex ? "(Your team)" : "")}\n";
        }
    }
    public bool SameTeam(GameObject player1,  GameObject player2)
    {
        Debug.Log("Name 1: " + player1.name);
        Debug.Log("Name 2: " + player2.name);
        foreach (var team in Teams)
        {
            if (team.Players.Contains(player1) && team.Players.Contains(player2)) return true;
        }
        return false;
    }

    public void DeletePlayer(GameObject player)
    {
        foreach (var team in Teams)
        {
            team.Players.Remove(player);
        }
    }
}
[Serializable]
public class Team
{
    public List<GameObject> Players;
    public Color TeamColor;
    public Transform Spawnpoint;
    public int Size;
    public BotsDifficulty botsDifficulty;
    public int CurrentSize { get
        {
            int count = 0;
            foreach (var player in Players)
            {
                if (player != null) count++;
            }
            return count;
        } }
}