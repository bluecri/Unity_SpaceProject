using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventorymanager : MonoBehaviour
{
    public static Inventorymanager instance = null;

    private Inventory m_playerInventory;        // player inventory info from file
    public UIInventory m_playerUIInventory;     // UI player inventory

    public Inventory m_PlayerInventory
    {
        get
        {
            return m_playerInventory;
        }

        set
        {
            m_playerInventory = value;
        }
    }

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

    public void Init_Inventorymanager()
    {
        if(m_playerUIInventory != null)
            m_playerUIInventory.Init_UIInventory();

        // need out user inventory init
        m_playerInventory = new Inventory("userInventory");
    }

    public void LoadInventoryToUI()
    {
        m_playerUIInventory.BindInventory(m_playerInventory);       // user inventory -> UI user inventory
    }
}
