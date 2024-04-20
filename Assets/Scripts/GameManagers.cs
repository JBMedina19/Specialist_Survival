using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum EScenes
{
    Outside,
    House
}
public class GameManagers : MonoBehaviour
{
    public static GameManagers instance = null;

    public EScenes GameScenes;
    public  List<GameObject> DontDestroyGO;
    public Transform Player;
    public Transform HouseSpawnPos,OutsideSpawnPos;
    NavMeshAgent agent;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(base.gameObject);
        }
        else
        {
            Destroy(base.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //starts from 0 to DontDestroyGO count, 0-6
       
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = Player.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnLevelWasLoaded(int level)
    {
        switch (GameScenes)
        {
            case EScenes.Outside:
                OutsideSpawnPos = GameObject.FindGameObjectWithTag("OutsidePosition").transform;
                Player.position = OutsideSpawnPos.position;
                agent.enabled = false;
                GameScenes = EScenes.House;
                StartCoroutine(DelayActivationAgent());
                break;
            case EScenes.House:
                HouseSpawnPos = GameObject.FindGameObjectWithTag("HousePosition").transform;
                Player.position = HouseSpawnPos.position;
                agent.enabled = false;
                GameScenes = EScenes.Outside;
                StartCoroutine(DelayActivationAgent());
                break;
            default:
                break;
        }
    }

    IEnumerator DelayActivationAgent()
    {
        yield return new WaitForSeconds(2f);
        agent.enabled = true;
    }
}
