using UnityEngine;
using UnityEditor;

/*
 * Represent Inventory One box in inventory.
 * Contains ItemInfo class, item num
 */
[System.Serializable]
public class ItemBox
{
    public int      m_itemCode;
    public int      m_itemNum;

    public ItemBox(ItemRaw itemRaw, int itemNum)
    {
        this.m_itemCode = itemRaw.m_ItemCode;
        this.m_itemNum = itemNum;
    }

    public Sprite GetItemSprite()
    {
        return ItemManager.instance.GetItemRawWithItemCode(m_itemCode).m_itemSprite;
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