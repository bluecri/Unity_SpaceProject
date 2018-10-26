using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

/*
 * Represent Inventory data.
 * Contains capacity(size & weight), list of InventoryOneBox
 */
[Serializable]
public class Inventory : FileLoadSaveElem
{
    [SerializeField]
    private float   m_weightCapacity;           // m_curInventoryWeight can exceed m_weightCapacity!

    [SerializeField]
    private List<ItemBox>   m_InventoryOneBox_List;

    private float m_curInventoryWeight;       // cur Total weight in inventory

    public Inventory(string name, int size = 12, float weight = 20.0f)
    {
        SetProperties(size, weight);
    }

    public bool SetSize(int newSize)
    {
        if (newSize < 0)
        {
            Debug.Log("[WARN] : Inventory::SetSize : cannot size to under 0");
            return false;
        }

        if (m_InventoryOneBox_List.Count <= newSize)
        {
            int addCount = newSize - m_InventoryOneBox_List.Count;
            AddSize(addCount);
            return true;
        }
        else
        {
            return DecSize(newSize);
        }
    }

    public void AddSize(int acc)
    {
        if (acc < 0)
        {
            Debug.Log("[WARN] : Inventory::AddSize : cannot size with negative value");
            return;
        }

        for (int i = 0; i < acc; i++)
            m_InventoryOneBox_List.Add(null);
    }

    public bool CanDecSize(int dec)
    {
        return IsEmpty(dec);
    }

    // inventory has empty slot with emptyCount or not
    public bool IsEmpty(int emptyCount = 1)
    {
        if (emptyCount < 0)
        {
            Debug.Log("[WARN] : Inventory::IsEmpty : cannot IsEmpty(negative value)");
            return false;
        }

        int curEmptyBoxCount = GetEmptySize();

        if (curEmptyBoxCount >= emptyCount)
        {
            return true;
        }

        return false;
    }

    // how many inventory has empty slot?
    public int GetEmptySize()
    {
        int curEmptyBoxCount = 0;
        foreach (ItemBox elem in m_InventoryOneBox_List)
        {
            if (elem == null)
                curEmptyBoxCount++;
        }

        return curEmptyBoxCount;
    }

    // Decrease size of inventory. If empty slot is not enough, retur false AND not decrease size.
    public bool DecSize(int dec)
    {
        if (false == CanDecSize(dec))
            return false;

        // move item back to front & reduce size
        int emptyIndex = 0;
        int delIndex = m_InventoryOneBox_List.Count - 1;

        int decCount = 0;
        while (decCount < dec)
        {
            // TODO : change null to emptyCheckFunc
            // Find empty slot
            while (m_InventoryOneBox_List[emptyIndex] != null)
            {
                emptyIndex++;
            }

            // TODO : change null to emptyCheckFunc
            if (m_InventoryOneBox_List[delIndex] == null)
            {
                m_InventoryOneBox_List.RemoveAt(delIndex);
            }
            else
            {
                // Move item in delindex to front empty slot
                m_InventoryOneBox_List[emptyIndex] = m_InventoryOneBox_List[delIndex];
                m_InventoryOneBox_List.RemoveAt(delIndex);
            }

            delIndex--;
            decCount++;
        }

        return true;
    }

    public bool SetWeight(float newWeight)
    {
        if (newWeight < 0.0)
        {
            Debug.Log("[WARN] : Inventory::SetWeight : cannot weight to under 0");
            return false;
        }
        m_weightCapacity = newWeight;
        return true;
    }

    public void AccWeight(float acc)
    {
        if (acc < 0.0)
        {
            Debug.Log("[WARN] : Inventory::AccWeight : cannot acc weight with negative value");
            return;
        }
        m_weightCapacity += acc;
    }

    public bool DecWeight(float dec)
    {
        if (dec < 0.0)
        {
            Debug.Log("[WARN] : Inventory::DecWeight : cannot dcc weight with negative value");
            return false;
        }

        if (m_weightCapacity < dec)
        {
            Debug.Log("[WARN] : Inventory::DecWeight : cannot decrese weight under 0");
            return false;
        }
        m_weightCapacity -= dec;
        return true;
    }

    public bool IsToHeavy()
    {
        if (m_curInventoryWeight > m_weightCapacity)
            return true;
        return false;
    }

    public int GetInventorySize()
    {
        return m_InventoryOneBox_List.Count;
    }


    public float WeightCapacity
    {
        get
        {
            return m_weightCapacity;
        }

        set
        {
            m_weightCapacity = value;
        }
    }

    public List<ItemBox> InventoryOneBox_List
    {
        get
        {
            return m_InventoryOneBox_List;
        }

        set
        {
            m_InventoryOneBox_List = value;
        }
    }


    private void SetProperties(int size, float weight)
    {
        WeightCapacity = weight;
        InventoryOneBox_List = new List<ItemBox>(size);
        for (int i = 0; i < size; i++)
        {
            InventoryOneBox_List.Add(new ItemBox(null, 0));
        }
    }

}
