using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShipInfo_FLS
{
    [HideInInspector]
    private int         m_shipCode;

    public float        m_shipWeight;
    public int          m_weaponStorageCount;
    public ShipData m_charaShipData;
    
}
