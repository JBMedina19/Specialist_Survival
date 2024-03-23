using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerStats : MonoBehaviour
{
    //Singleton
    public static PlayerStats Instance { get; private set; }

    public int playerHealth;
    public int playerDamage;
    public ParticleSystem bloodFX;

    public TextMeshProUGUI HealthText;

    private void Awake()
    {
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
        
    }

    // Update is called once per frame
    void Update()
    {
        HealthText.text = playerHealth.ToString();
    }
}
