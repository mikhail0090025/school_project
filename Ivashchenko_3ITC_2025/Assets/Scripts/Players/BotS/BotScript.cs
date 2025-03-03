using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;
[RequireComponent(typeof(HPscript))]
[RequireComponent(typeof(ScoreCounter))]
[RequireComponent(typeof(AudioSource))]
public class BotScript : MonoBehaviour
{
    [SerializeField] Animator MyAnimator;
    public List<Transform> Targets;
    NavMeshAgent agent;
    [SerializeField] Transform origin;
    [SerializeField] Transform dir;
    [SerializeField] float Damage;
    [SerializeField] float ShootsPerSecond;
    [SerializeField] Transform CurrentTarget;
    private AudioSource audioSource;
    public float ShootSpread = 10f;
    public Color BotsColor;
    float TimeSinceLastShot;
    float TimeBetweenShots;
    HPscript myHPS;
    public ScoreCounter myScoreCounter { get; private set; }
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (Targets.Count == 0) Debug.LogError("There are no targets in list");
        DefineTarget();
        TimeBetweenShots = 1f / ShootsPerSecond;
        myHPS = GetComponent<HPscript>();
        myScoreCounter = GetComponent<ScoreCounter>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        TimeSinceLastShot += Time.deltaTime;
        if(CurrentTarget == null) DefineTarget();
        if(CurrentTarget != null)
        {
            if (CanHitTarget() && Vector3.Distance(CurrentTarget.position, transform.position) < 15f)
            {
                if(Vector3.Distance(CurrentTarget.position, transform.position) < 10f)
                {
                    Vector3 retreatDirection = (transform.position - CurrentTarget.position).normalized;
                    Vector3 newDestination = transform.position + retreatDirection * 3f;
                    agent.SetDestination(newDestination);
                }
                else
                {
                    agent.SetDestination(transform.position);
                }
                transform.LookAt(CurrentTarget.position);
                SetAnimation(false, false, true);
                Shoot();
            }
            else
            //if (Vector3.Distance(CurrentTarget.position, transform.position) < 20f)
            {
                Vector3 randomOffset = new Vector3(Random.Range(-2f, 2f), 0, Random.Range(-2f, 2f));
                agent.SetDestination(CurrentTarget.position + randomOffset);
                SetAnimation(true, true, false);
                if (CanHitTarget() && Random.Range(0f, (3f / Time.deltaTime)) < 1f)
                {
                    Shoot();
                }
            }/*
            else
            {
                if (agent.destination == null || Vector3.Distance(agent.destination, transform.position) < 1f) {
                    var points = FindObjectOfType<MapPoints>().Points_;
                    agent.SetDestination(points[Random.Range(0, points.Count)].transform.position);
                }
                SetAnimation(true, true, false);
                if (CanHitTarget() && Random.Range(0f, (3f / Time.deltaTime)) < 1f)
                {
                    Shoot();
                }
            }*/
        }
        var clr = transform.Find("PlayerPoint").gameObject.GetComponent<Renderer>().material.color;
        transform.Find("PlayerPoint").gameObject.GetComponent<Renderer>().material.color = new Color(clr.r, clr.g, clr.b, myHPS.PercentHP * 255f);
    }
    void SetAnimation(bool walk, bool run, bool crouch)
    {
        MyAnimator.SetBool("Walk", walk);
        MyAnimator.SetBool("Run", run);
        MyAnimator.SetBool("Crouch", crouch);
    }
    void DefineTarget()
    {
        float minDist = 99999f;
        Transform current = transform;
        Targets.RemoveAll(target => target == null);
        if (Targets.Count == 0)
        {
            //Debug.LogError("No valid targets in the list!");
            CurrentTarget = null;
            return;
        }
        foreach (var target in Targets)
        {
            if(Vector3.Distance(target.position, transform.position) < minDist)
            {
                if (target.GetComponent<HpScriptPlayer>())
                {
                    if (target.GetComponent<HpScriptPlayer>().GetCurrentHP <= 0) continue;
                }
                current = target;
                minDist = Vector3.Distance(target.position, transform.position);
            }
        }
        CurrentTarget = current;
    }

    void Shoot()
    {
        if (TimeSinceLastShot > TimeBetweenShots) TimeSinceLastShot = 0;
        else return;
        MyAnimator.SetTrigger("Shot");
        audioSource.Play();

        Vector3 direction = (CurrentTarget.position - origin.position).normalized;

        float spreadAngle = ShootSpread;
        Quaternion spread = Quaternion.Euler(
            Random.Range(-spreadAngle, spreadAngle),
            Random.Range(-spreadAngle, spreadAngle),
            0
        );
        direction = spread * direction;

        Ray ray = new Ray(origin.position, direction);
        Debug.DrawRay(origin.position, direction, BotsColor, 1);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.GetComponent<HPscript>())
            {
                var scr = hit.collider.gameObject.GetComponent<HPscript>();
                scr.Damage(this.Damage * (Random.Range(50, 150) / 100f));
                if (scr.GetCurrentHP <= 0f) myScoreCounter.NewKill();
            }
            else if(hit.collider.name == gameObject.name) Debug.Log("I hit myself");
        }
    }

    public void DeleteTargetIfPlayer()
    {
        if (CurrentTarget.name == "Player")
        {
            CurrentTarget = null;
        }
    }

    bool CanHitTarget()
    {
        Vector3 direction = (CurrentTarget.position - origin.position).normalized;
        RaycastHit hit;
        if (Physics.Raycast(origin.position, direction, out hit))
        {
            if (hit.collider.gameObject != CurrentTarget.gameObject)
            {
                return false;
            }
        }
        return true;
    }

    public void AddTarget(Transform new_target) => Targets.Add(new_target);

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.collider.tag == "Player")
    //    {
    //        var diff = collision.transform.position - transform.position;
    //        var point = transform.position - (diff * 2f);
    //        agent.SetDestination(point);
    //        Debug.Log((collision.transform.position, transform.position, point, diff));
    //    }
    //}
}

public enum BotsDifficulty { Easy, Medium, Hard, VeryHard}