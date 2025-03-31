using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
            foreach (var item in FindObjectsByType<BotScript>(FindObjectsSortMode.None))
            {
                item.Targets.RemoveAll(target => target == transform);
            }
            Destroy(GetComponent<BotScript>());
            Destroy(GetComponent<ScoreCounter>());
            Destroy(GetComponent<Animator>());
            Destroy(GetComponent<NavMeshAgent>());
            Destroy(transform.Find("Quad").gameObject);
            Destroy(transform.Find("PlayerPoint").gameObject);
            gameObject.name = gameObject.name + "Dead";
            var rb = GetComponent<Rigidbody>();
            var delin = gameObject.AddComponent<DeleteIn>();
            delin.seconds = 8;
            rb.constraints = RigidbodyConstraints.None;
            Debug.Log("I am killed");
            Destroy(this);
            //Destroy(gameObject);
        } else if (teamsSpawner.CurrentGameMode == GameMode.FreeForAll)
        {
            var my_team = teamsSpawner.DefineTeam(gameObject);
            var Spawnpoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("Spawner"));
            transform.position = Spawnpoints.GetRandomItem().transform.position;
            CurrentHP = MaxHP;
        }
        Debug.Log("I am killed");


        /*var deadbody = Instantiate(DeadBody, transform.position, transform.rotation);
        var sbc = GetComponent<SetBodyColor>();
        if (sbc)
        {
            deadbody.GetComponent<DeadbodyScript>()?.PaintMe(sbc.BotsColor);
        }*/
    }
}
