using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum EScenes_1
{
    House,
    Outside
}
public class GameManager : MonoBehaviour
{
    public List<GameObject> DontDestroyGO;
    public EScenes GameScenes;
    public GameObject Player;

    public Transform HouseSpawnPos;
    public Transform OutsideSpawnPos;
    public static GameManager Instance { get; private set; }
    NavMeshAgent agent;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        agent = Player.GetComponent<NavMeshAgent>();
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        for (int i = 0; i < DontDestroyGO.Count; i++)
        {
            DontDestroyOnLoad(DontDestroyGO[i]);
        }
    }

    private void Update()
    {
        
    }

    private void OnLevelWasLoaded()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        Debug.Log("Loaded");
        
        switch (GameScenes)
        {
            case EScenes.House:
                HouseSpawnPos = GameObject.FindGameObjectWithTag("HousePosition").transform;
                agent.enabled = false;
                Player.transform.position = HouseSpawnPos.position;
                StartCoroutine(DelayActivateAgent());
                break;
            case EScenes.Outside:
                OutsideSpawnPos = GameObject.FindGameObjectWithTag("OutsidePosition").transform;
                agent.enabled = false;
                Player.transform.position = OutsideSpawnPos.position;
                StartCoroutine(DelayActivateAgent());
                break;
            default:
                break;
        }


    }

    IEnumerator DelayActivateAgent()
    {
        yield return new WaitForSeconds(0.5f);
        agent.enabled = true;
    }
}
