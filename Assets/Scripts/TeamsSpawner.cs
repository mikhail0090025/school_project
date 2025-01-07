using System;
using System.Collections.Generic;
using UnityEngine;

public class TeamsSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> BotPrefabs;
    [SerializeField] List<Team> Teams;
    [SerializeField] int PlayersTeamIndex;
    [SerializeField] Transform Player;
    [SerializeField] bool FromGameSettings;
    void Start()
    {
        if (FromGameSettings)
        {
            if (Teams.Count != 2) Debug.Log("If you get team data from new game settings, you can have only 2 teams (no more nor less)");
            Teams[0].Size = NewGameSettings.Team1Size;
            Teams[1].Size = NewGameSettings.Team2Size;
            Teams[0].botsDifficulty = (BotsDifficulty)NewGameSettings.BotsDifficulty;
            Teams[1].botsDifficulty = (BotsDifficulty)NewGameSettings.BotsDifficulty;
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
            }
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
                    if(Teams.IndexOf(team1) != PlayersTeamIndex) bot1.GetComponent<BotScript>().AddTarget(Player);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
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
}