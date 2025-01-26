using UnityEngine;

public class HPscript : MonoBehaviour
{
    [SerializeField] protected float MaxHP;
    [SerializeField] protected float CurrentHP;
    [SerializeField] public float PercentHP => CurrentHP / (float)MaxHP;
    public float GetCurrentHP => CurrentHP;
    [SerializeField] GameObject DeadBody;
    ScoreCounter scoreCounter;


    protected virtual void Start()
    {
        CurrentHP = MaxHP;
        scoreCounter = GetComponent<ScoreCounter>();
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
        foreach (var item in FindObjectsOfType<BotScript>())
        {
            item.Targets.RemoveAll(target => target == transform);
        }
        var deadbody = Instantiate(DeadBody, transform.position, transform.rotation);
        var sbc = GetComponent<SetBodyColor>();
        if (sbc)
        {
            deadbody.GetComponent<DeadbodyScript>()?.PaintMe(sbc.BotsColor);
        }
        Destroy(gameObject);
    }
}
