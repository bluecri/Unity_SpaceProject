using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class ShipData
{

    /*
     * shield += [ - InDmg * (1.0 - shieldArmorPercent) - fixedDmg ]
     * hp     += [ - (remainInDmg - shieldArmor) - fixedDmg ]
     */

    public PropInt m_hp;
    public PropInt m_hpArmor;
    public PropInt m_shield;
    public PropFloat m_shieldArmorPercent;

    public PropFloat m_speed;
    public PropFloat m_speedAccel;
    public PropFloat m_speedRotate;
}
