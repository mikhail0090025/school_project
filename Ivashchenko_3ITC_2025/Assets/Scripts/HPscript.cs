using System.Collections.Generic;
using UnityEngine;

public class HPscript : MonoBehaviour
{
    [SerializeField] protected float MaxHP;
    [SerializeField] protected float CurrentHP;
    [SerializeField] public float PercentHP => CurrentHP / (float)MaxHP;
    public float GetCurrentHP => CurrentHP;
    [SerializeField] GameObject DeadBody;
    ScoreCounter scoreCounter;
    TeamsSpawner teamsSpawner;


    protected virtual void Start()
    {
        CurrentHP = MaxHP;
        scoreCounter = GetComponent<ScoreCounter>();
        teamsSpawner = FindAnyObjectByType<TeamsSpawner>();
    }

    protected virtual void Update()
    {
        if(CurrentHP <= 0)
        {
            Dead();
        }
    }
    public virtual void Damage(float damage)
    {
        CurrentHP -= damage;
    }
    public virtual void Dead()
    {
        scoreCounter.NewDeath();


        if (teamsSpawner.CurrentGameMode == GameMode.TwoTeams)
        {
            foreach (var item in FindObjectsOfType<BotScript>())
            {
                item.Targets.RemoveAll(target => target == transform);
            }
            Destroy(gameObject);
        } else if (teamsSpawner.CurrentGameMode == GameMode.FreeForAll)
        {
            var my_team = teamsSpawner.DefineTeam(gameObject);
            var Spawnpoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("Spawner"));
            transform.position = Spawnpoints.GetRandomItem().transform.position;
        }


        var deadbody = Instantiate(DeadBody, transform.position, transform.rotation);
        var sbc = GetComponent<SetBodyColor>();
        if (sbc)
        {
            deadbody.GetComponent<DeadbodyScript>()?.PaintMe(sbc.BotsColor);
        }
    }
}
