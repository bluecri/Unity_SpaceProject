using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGameShips : MonoBehaviour
{
    [System.Serializable]
    public class NewGameShipInfo
    {
        public GameObject   m_ShipPrefabs;  // prefab of ship (nodel, anim...)
        public ShipInfo_FLS m_shipInfos;  // ship info (hp, shield ..)
        public Inventory    m_inventory;     // inventory (weight, size...)
    }

    public NewGameShipInfo[] m_NewGameCharaInfo;
}
