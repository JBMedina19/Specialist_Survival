using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHarvest : MonoBehaviour
{
    public Animator anim;
    public TreeBehaviour targetTree;
    public AnimalBehaviour targetAnimal;
    public float playerWoodCount;
    public PlayerStats playerStats;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        playerStats = GetComponent<PlayerStats>();
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
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Tree")){targetTree = null;}
        if (other.CompareTag("Enemy")){ targetAnimal = null;}
    }
}
