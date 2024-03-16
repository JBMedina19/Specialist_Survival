using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBehaviour : MonoBehaviour
{
    public Animator anim;
    public float woodCount;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        TreeMechanics();
    }

    public void TreeMechanics()
    {
        if (woodCount <= 0)
        {
            anim.SetBool("isDead",true);
            anim.SetBool("isHarvesting", false);
            Destroy(gameObject,2);
        }
    }


}
