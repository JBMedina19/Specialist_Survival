using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AIBehaviour : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform pointA;
    public Transform pointB;
    public Transform destination;

    public float chaseRange;

    public Transform playerPosition;
    public float distanceToPlayer;
    public bool isChasing;
    public bool isWalking;

    public Animator animalAnimation;
    // Start is called before the first frame update
    void Start()
    {
        animalAnimation = GetComponent<AnimalBehaviour>().anim;
        isChasing = false;
        isWalking = false;
        agent = GetComponent<NavMeshAgent>();
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        SetDestination(pointA);
        animalAnimation.SetBool("isWalking", true);
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, playerPosition.position);
        if (distanceToPlayer <= chaseRange)
        {
            isChasing = true;
            animalAnimation.SetBool("isWalking", false);
            animalAnimation.SetBool("isAttacking", false);
            animalAnimation.SetBool("isRunning", true);
            agent.speed = 2;
            SetDestination(playerPosition);
        }
        if (distanceToPlayer <= 1.5f)
        {
            agent.speed = 0f;
            animalAnimation.SetBool("isRunning", false);
            animalAnimation.SetBool("isAttacking", true);
        }
        //path pending = if theres no queue on our path.
        //remaining distance = distance between the agent and the destination.
        //&& = and, = equals, || == or.
        if (!agent.pathPending && agent.remainingDistance <0.01f && isChasing == false)
        {
            isWalking = true;
            animalAnimation.SetBool("isWalking", true);
            if (destination == pointA)
            {
                SetDestination(pointB);
            }
            else
            {
                SetDestination(pointA);
            }
        }
    }

    void SetDestination(Transform dest)
    {
        destination = dest;
        agent.SetDestination(dest.position);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }

    public void AttackPlayer()
    {
        PlayerStats health = playerPosition.gameObject.GetComponent<PlayerStats>();
        AnimalBehaviour damage = GetComponent<AnimalBehaviour>();
        health.playerHealth -= damage.damage;
        health.bloodFX.Play();

    }
}
