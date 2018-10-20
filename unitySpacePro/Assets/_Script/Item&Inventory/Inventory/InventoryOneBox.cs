using UnityEngine;
using UnityEditor;

/*
 * Represent Inventory One box in inventory.
 * Contains ItemInfo class, item num
 */
public class InventoryOneBox
{
    public ItemInfo m_itemInfo;
    public int      m_itemNum;

    public InventoryOneBox(ItemInfo m_itemInfo, int m_itemNum)
    {
        this.m_itemInfo = m_itemInfo;
        this.m_itemNum = m_itemNum;
    }

    public Sprite GetItemSprite()
    {
        return m_itemInfo.m_itemSprite;
    }

    public string GetItemNumStr()
    {
        return m_itemNum.ToString();
    }

    public int GetItemNum()
    {
        return m_itemNum;
    }
}