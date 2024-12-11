using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TeamsSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> BotPrefabs;
    [SerializeField] List<Team> Teams;
    [SerializeField] int PlayersTeamIndex;
    [SerializeField] Transform Player;
    void Start()
    {
        foreach (var team in Teams)
        {
            for (global::System.Int32 i = 0; i < team.Size; i++)
            {
                var spawned = Instantiate(BotPrefabs[UnityEngine.Random.Range(0, BotPrefabs.Count)], team.Spawnpoint.position + new Vector3(0,0,i), Quaternion.identity);
                var bot_script = spawned.GetComponent<BotScript>();
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
                        bot1.GetComponent<BotScript>().AddTarget(bot2.transform);
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
}