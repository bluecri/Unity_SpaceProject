using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public ShipInfo_FLS m_shipInfo_FLS;
    public Inventory m_inventory;
    public Sprite m_shipSprite;
    public GameObject m_prefab_ship;

    public bool m_bStartShip;               // player can start with this ship
}
