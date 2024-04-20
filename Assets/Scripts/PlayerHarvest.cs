using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHarvest : MonoBehaviour
{
    public static PlayerHarvest instance = null;

    public Animator anim;
    public TreeBehaviour targetTree;
    public AnimalBehaviour targetAnimal;
    public int playerWoodCount;
    public int playerMeatCount;
    public PlayerStats playerStats;

    public GameObject EnterHouseText;
    public GameObject ExitHouseText;
    public GameObject ChickenFenceText;

    private bool canEnterHouse;
    private bool canExitHouse;
    private bool canCreateChickenFence;

    public GameObject ChickenFence;
    public GameObject ChickenFenceTrigger;
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
        
        anim = GetComponent<Animator>();
        playerStats = GetComponent<PlayerStats>();
        canEnterHouse = false;
        canExitHouse = false;
        canCreateChickenFence = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (targetTree != null)
        {
            WoodChop();
        }
        if (targetAnimal != null)
        {
            AnimalChop();
        }

        if (Input.GetKeyDown(KeyCode.E) && canEnterHouse)
        {
            SceneManager.LoadScene("HouseScene");
        }
        if (Input.GetKeyDown(KeyCode.E) && canExitHouse)
        {
            SceneManager.LoadScene("OutsideScene");
        }
        if (Input.GetKeyDown(KeyCode.E) && InventorySystem.Instance.woodCount >= 20)
        {
            canCreateChickenFence = true;
            Destroy(ChickenFenceTrigger);
            ChickenFence.SetActive(true);
        }
    }
    public void WoodChop()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            anim.SetTrigger("ChoppingWood");
            StartCoroutine(DelayTreeAnimation());
        }
    }
    IEnumerator DelayTreeAnimation()
    {
        yield return new WaitForSeconds(0.75f);
        targetTree.anim.SetTrigger("isHarvesting");
        targetTree.woodCount--;
        playerWoodCount++;
        InventorySystem.Instance.woodCount = playerWoodCount;
    }

    public void AnimalChop()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            anim.SetTrigger("ChoppingWood");
            StartCoroutine(DelayAnimalAnimation());
        }
    }

    IEnumerator DelayAnimalAnimation()
    {
        yield return new WaitForSeconds(0.75f);
        targetAnimal.anim.SetTrigger("isHit");
        targetAnimal.healthPoints -= playerStats.playerDamage;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tree"))
        {
            targetTree = other.GetComponent<TreeBehaviour>();
        }
        if (other.CompareTag("Enemy"))
        {
            targetAnimal = other.GetComponent<AnimalBehaviour>();
        }
        if (other.CompareTag("House"))
        {
            EnterHouseText.SetActive(true);
            canEnterHouse = true;
        }
        if (other.CompareTag("OutsideTrigger"))
        {
            ExitHouseText.SetActive(true);
            canExitHouse = true;
        }
        if (other.CompareTag("ChickenFence"))
        {
            ChickenFenceText.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Tree")){targetTree = null;}
        if (other.CompareTag("Enemy")){ targetAnimal = null;}
        if (other.CompareTag("House"))
        {
            EnterHouseText.SetActive(false);
            canEnterHouse = false;
        }
        if (other.CompareTag("OutsideTrigger"))
        {
            ExitHouseText.SetActive(false);
            canExitHouse = false;
        }
        if (other.CompareTag("ChickenFence"))
        {
            ChickenFenceText.SetActive(false);
        }
    }

    private void OnLevelWasLoaded()
    {
        EnterHouseText.SetActive(false);
        canEnterHouse = false;
        ExitHouseText.SetActive(false);
        canExitHouse = false;

    }

}
