using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShipRaw
{
    [HideInInspector]
    private int m_shipCode;

    public float        m_shipWeight;
    public int          m_weaponStorageCount;
    public Inventory    m_inventory;
    public CharaShipData m_charaShipData;


    public Sprite       m_shipSprite;
    public GameObject   m_prefab_ship;

    public bool         m_bStartShip;               // player can start with this ship



    public int m_ShipCode
    {
        get
        {
            return m_shipCode;
        }

        set
        {
            m_shipCode = value;
        }
    }
}
