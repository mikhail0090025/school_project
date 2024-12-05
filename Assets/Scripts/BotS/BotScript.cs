using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class BotScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] Animator MyAnimator;
    Transform CurrentTarget;
    [SerializeField] List<Transform> Targets;
    NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (Targets.Count == 0) Debug.LogError("There are no targets in list");
        DefineTarget();
    }

    // Update is called once per frame
    void Update()
    {
        if(CurrentTarget != null)
        {
            if(Vector3.Distance(CurrentTarget.position, transform.position) > 5f)
            {
                agent.SetDestination(CurrentTarget.position);
                MyAnimator.SetBool("Walk", true);
                MyAnimator.SetBool("Run", true);
                MyAnimator.SetBool("Aim", false);
            }
            else
            {
                agent.SetDestination(transform.position);
                transform.LookAt(CurrentTarget.position);
                MyAnimator.SetBool("Walk", false);
                MyAnimator.SetBool("Run", false);
                MyAnimator.SetBool("Aim", true);
                MyAnimator.SetTrigger("Shot");
            }
        }
    }
    void DefineTarget()
    {
        float minDist = 99999f;
        Transform current = transform;
        foreach (var target in Targets)
        {
            if(Vector3.Distance(target.position, transform.position) < minDist)
            {
                current = target;
                minDist = Vector3.Distance(target.position, transform.position);
            }
        }
        CurrentTarget = current;
    }
}
