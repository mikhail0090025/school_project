using UnityEngine;

public class HPscript : MonoBehaviour
{
    [SerializeField] protected float MaxHP;
    [SerializeField] protected float CurrentHP;
    public float GetCurrentHP => CurrentHP;
    [SerializeField] GameObject DeadBody;
    [SerializeField] int kills;
    [SerializeField] int deaths;
    public int Kills => kills;
    public int Deaths => deaths;


    protected virtual void Start()
    {
        CurrentHP = MaxHP;
        kills = 0;
        deaths = 0;
    }
    public void NewKill() => kills++;

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
        deaths++;
        foreach (var item in FindObjectsOfType<BotScript>())
        {
            item.Targets.RemoveAll(target => target == transform);
        }
        Instantiate(DeadBody, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
