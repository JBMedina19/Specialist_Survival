using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatDrop : MonoBehaviour
{
    public int meatCount;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHarvest>().playerMeatCount += meatCount;
            InventorySystem.Instance.meatCount += meatCount;
            Destroy(gameObject);
        }
    }
}
