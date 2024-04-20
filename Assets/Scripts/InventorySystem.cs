using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class InventorySystem : MonoBehaviour
{
    //Singleton
    public static InventorySystem Instance { get; private set; }

    public KeyCode InventoryOpenButton = KeyCode.I;
    public GameObject InventoryWindow;

    public TextMeshProUGUI woodCountText;
    public int woodCount;

    public TextMeshProUGUI meatCountText;
    public int meatCount;

    private bool hasMeat;

    public int meatHealValue;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
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
        
    }

    // Update is called once per frame
    void Update()
    {
        woodCountText.text = woodCount.ToString();
        meatCountText.text = meatCount.ToString();
        if (Input.GetKeyDown(InventoryOpenButton))
        {
            InventoryWindow.SetActive(true);
            Time.timeScale = 0;
        }

        if (meatCount > 0)
        {
            hasMeat = true;
        }
        else
        {
            hasMeat = false;
        }
    }

    public void CloseInventory()
    {
        InventoryWindow.SetActive(false);
        Time.timeScale = 1;
    }

    public void ConsumeMeat()
    {
        if (hasMeat)
        {
            PlayerStats.Instance.playerHealth += meatHealValue;
            meatCount--;
        }
    }

}
