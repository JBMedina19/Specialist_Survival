using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum ElifeCycle
{
    Egg,
    Chick,
    Adult
}
public class ChickenBehaviour : MonoBehaviour
{
    public ElifeCycle lifeCycle;
    public GameObject[] chickenLife;
    public int activeChicken;
    public NavMeshAgent agent;

    public List<Transform> targetPos;
    public int wayPointNumber;
    public Transform waypointParent;

    public float toLayEggs;
    float toLayEggsStore;

    public GameObject eggToSpawn;

    public bool readyToLayEggs;
    private void Awake()
    {
        //MoveToRandomWayPoint();
    }
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        foreach (Transform tr in waypointParent.GetComponentInChildren<Transform>())
        {
            targetPos.Add(tr.gameObject.transform);
        }
        toLayEggsStore = toLayEggs;
        MoveToRandomWayPoint();
    }

    // Update is called once per frame
    void Update()
    {
        ChickenAppearanceUpdater();
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0)
            {
                MoveToRandomWayPoint();
            }
        }
        
        if (readyToLayEggs)
        {
            toLayEggs -= Time.deltaTime;
            if (toLayEggs <= 0)
            {
                toLayEggs = toLayEggsStore;
                Instantiate(eggToSpawn, transform.position, Quaternion.identity);
            }
        }
        

    }
    public void ChickenAppearanceUpdater()
    {
        for (int i = 0; i < chickenLife.Length; i++)
        {
            chickenLife[i].SetActive(i == activeChicken);
        }
        switch (lifeCycle)
        {
            case ElifeCycle.Egg:
                activeChicken = 0;
                agent.radius = 0.5f;
                agent.speed = 0;
                readyToLayEggs = false;
                break;
            case ElifeCycle.Chick:
                activeChicken = 1;
                agent.radius = 0.5f;
                agent.speed = 1.5f;
                readyToLayEggs = false;
                break;
            case ElifeCycle.Adult:
                activeChicken = 2;
                agent.radius = 1.0f;
                agent.speed = 2f;
                readyToLayEggs = true;
                break;
            default:
                break;
        }
    }

    private void MoveToRandomWayPoint()
    {
        if (targetPos.Count == 0)
        {
            Debug.LogWarning("No Waypoints Available");
            return;
        }
        int newWayPointIndex = GetRandomWayPoint();
        if (newWayPointIndex != wayPointNumber)
        {
            wayPointNumber = newWayPointIndex;
            agent.SetDestination(targetPos[wayPointNumber].position);
        }
        else
        {
            MoveToRandomWayPoint();
        }
    }

    private int GetRandomWayPoint()
    {
        return Random.Range(0,targetPos.Count);
    }
}
