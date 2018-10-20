using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventorymanager : MonoBehaviour
{
    public static Inventorymanager instance = null;

    private Inventory m_playerInventory;        // player inventory info from file
    public UIInventory m_playerUIInventory;     // UI player inventory

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // init UI inventories
        Init_UIInventory();
    }

    private void Init_UIInventory()
    {
        if(m_playerUIInventory != null)
            m_playerUIInventory.Init_UIInventory();
    }

    public void LoadUserInventory()
    {
        // user inventory
        m_playerInventory.LoadInventory();      // File -> user inventory
        m_playerUIInventory.BindInventory(m_playerInventory);       // user inventory -> UI user inventory
    }
}
