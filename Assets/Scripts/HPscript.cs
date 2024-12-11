using UnityEngine;

public class HPscript : MonoBehaviour
{
    [SerializeField] protected float MaxHP;
    [SerializeField] protected float CurrentHP;
    [SerializeField] GameObject DeadBody;

    protected virtual void Start()
    {
        CurrentHP = MaxHP;
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
        foreach (var item in FindObjectsOfType<BotScript>())
        {
            item.Targets.RemoveAll(target => target == transform);
        }
        Instantiate(DeadBody, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
