﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ItemRaw {
    [HideInInspector]
    private int     m_itemCode;
    
    public float    m_itemWeight;               // item weight
    public int      m_maxItemNumInOneBox;       // max item num in 1 inventory box
    public bool     m_canUseItem;               // player can consume item

    public Sprite           m_itemSprite;
    public GameObject       m_prefab_item;

    public int m_ItemCode
    {
        get
        {
            return m_itemCode;
        }

        set
        {
            m_itemCode = value;
        }
    }
}
