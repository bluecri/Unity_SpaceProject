using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventoryOneBox : MonoBehaviour {
    private InventoryOneBox m_oneBox;
    private Image m_itemImage;
    private Text m_itemNumText;

    public void Init_UIInventoryOneBox()
    {
        m_itemImage = transform.GetChild(0).Find("ItemImage").GetComponent<Image>();
        m_itemNumText = transform.GetChild(0).Find("ItemNumLabel").GetComponent<Text>();
    }

    public void LoadInventoryOneBox(InventoryOneBox oneBox)
    {
        m_oneBox = oneBox;

        if(m_oneBox == null)
        {
            m_itemImage.sprite = null;
            m_itemNumText.text = "0";
            return;
        }

        m_itemImage.sprite = m_oneBox.GetItemSprite();
        m_itemNumText.text = m_oneBox.GetItemNumStr();

    }
}
