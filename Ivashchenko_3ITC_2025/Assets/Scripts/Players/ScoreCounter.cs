using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] int kills;
    [SerializeField] int friendKills;
    [SerializeField] int deaths;
    public int Kills => kills;
    public int FriendKills => friendKills;
    public int Deaths => deaths;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        kills = 0;
        deaths = 0;
    }
    public void NewKill() => kills++;
    public void NewFriendKill() => friendKills++;
    public void NewDeath() => deaths++;

    // Update is called once per frame
    void Update()
    {
        
    }

    public int Score()
    {
        int killsFactor = 10;
        int friendlyKillsFactor = -15;
        int deathsFactor = -5;

        return Mathf.Max(0, (kills * killsFactor) + (friendKills * friendlyKillsFactor) + (deaths * deathsFactor));
    }
}
