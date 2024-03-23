using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalBehaviour : MonoBehaviour
{
    public Animator anim;
    public int healthPoints;
    public int damage;
    public AIBehaviour aiBehaviour;
    public ParticleSystem deathFX;

    public GameObject meatDrops;

    public float yOffset;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        aiBehaviour = GetComponent<AIBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        if (healthPoints<=0)
        {
            anim.SetTrigger("isDead");
            aiBehaviour.agent.speed = 0;
            deathFX.transform.SetParent(null);
            deathFX.Play();
            Destroy(gameObject,2);
            
          
        }
    }
    private void OnDestroy()
    {
        //Instantiate = to spawn and object
        //Instantiate (object to spawn,where to spawn,spawn rotation)
        Instantiate(meatDrops, new Vector3 (transform.position.x, transform.position.y + yOffset, transform.position.z), Quaternion.identity);
    }
}
