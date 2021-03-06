﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipManager : MonoBehaviour {
    public static ShipManager instance = null;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static int m_maxUserShipOwnCount = 5;       // how many ships user can own

    public List<Ship> m_shipRawList;         // premade ships
    public List<Ship> m_userOwnShip_List;     // user own ship list

    void Init_ShipManager()
    {
        // init m_userOwnShip_List with nulls
        m_userOwnShip_List = new List<Ship>();
        for(int i=0; i< m_maxUserShipOwnCount; i++)
        {
            m_userOwnShip_List.Add(null);
        }
    }

}
