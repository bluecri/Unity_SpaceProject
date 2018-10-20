using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemManager : MonoBehaviour
{
    public List<GameObject> m_prefab_itemInfo_list; // GUI manipulate

    private List<ItemInfo> m_itemInfo_list;


    private void Start()
    {
        m_itemInfo_list = new List<ItemInfo>(m_prefab_itemInfo_list.Count);

        for (int i=0; i<m_prefab_itemInfo_list.Count; i++)
        {
            if (m_prefab_itemInfo_list[i] == null)
            {
                i++;
                continue;
            }

            ItemInfo inInfo = m_prefab_itemInfo_list[i].GetComponent<ItemInfo>();
            inInfo.ItemCode = i;
            m_itemInfo_list.Add(inInfo);
        }
    }
}
