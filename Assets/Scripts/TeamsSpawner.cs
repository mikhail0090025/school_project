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
            int sqrt = Mathf.RoundToInt(Mathf.Sqrt(team.Size));
            for (int i = 0; i < team.Size; i++)
            {
                var spawned = Instantiate(BotPrefabs[UnityEngine.Random.Range(0, BotPrefabs.Count)], team.Spawnpoint.position + new Vector3((i / sqrt), 0,i % sqrt), Quaternion.identity);
                var bot_script = spawned.GetComponent<BotScript>();
                spawned.transform.Find("PlayerPoint").GetComponent<Renderer>().material.color = team.TeamColor;
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