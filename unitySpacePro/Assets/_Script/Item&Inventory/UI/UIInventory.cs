using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour {
    public GameObject   m_prefab_uiOneBox;
    public GameObject   m_prefab_uiRow;
    public GameObject   m_inst_scrollContent;

    private int         m_totalBoxNum;
    private  float      m_heightOneBox, m_widthOneBox;
    private  float      m_heightRow;

    private List<UIInventoryOneBox> m_uiInventoryOneBox_List;
    private Inventory m_bindedInventory;

    public void Init_UIInventory()
    {
        GetPrefabInfo();

        this.m_totalBoxNum = 0;   // default value
    }

    private void GetPrefabInfo()
    {
        this.m_heightOneBox = m_prefab_uiOneBox.GetComponent<RectTransform>().rect.height;
        this.m_widthOneBox = m_prefab_uiOneBox.GetComponent<RectTransform>().rect.width;

        this.m_heightRow = m_prefab_uiRow.GetComponent<RectTransform>().rect.height;
    }

    public void BindInventory(Inventory newBindedInventory)
    {
        if (newBindedInventory == null)
        {
            Debug.Log("[ERR] : UIInventory::BindInventory(Inventory newBindedInventory) newBindedInventory is null!");
            return;
        }

        if (newBindedInventory == m_bindedInventory)
        {
            Debug.Log("UIInventory::BindInventory(Inventory newBindedInventory) newBindedInventory is already binded");
            return;
        }

        m_bindedInventory = newBindedInventory;             // Set binded inventory
        m_totalBoxNum = m_bindedInventory.GetInventorySize();     // Get total box num from binded inventory

        // Make m_uiInventoryOneBox_List space
        m_uiInventoryOneBox_List = new List<UIInventoryOneBox>(m_totalBoxNum);

        // Create UI oneBoxes and bind class UIInventoryOneBox to m_uiInventoryOneBox_List
        CreateBoxUIInScroll();

        // Load Item
        LoadBoxUIInfoInScroll();
    }

    public void UnbindInventory()
    {
        m_bindedInventory = null;

        List<ItemBox> tempInventoryOneBoxList = m_bindedInventory.InventoryOneBox_List;
        for (int i = 0; i < tempInventoryOneBoxList.Count; i++)
        {
            m_uiInventoryOneBox_List[i].LoadInventoryOneBox(null);
        }
        m_uiInventoryOneBox_List = null;

        // destroy content
        DestroyBoxUIInScroll();
    }

    // Should call after m_bindedInventory is not null.
    public void CreateBoxUIInScroll()
    {
        if (m_bindedInventory == null)
            return;

        float curScrollWidth = gameObject.GetComponent<RectTransform>().rect.width;     // inventory scroll width size
        int targetBoxNum = m_bindedInventory.GetInventorySize();                              // create box with this num

        // Calculate boxes Row & Column Num
        int boxColNum = Mathf.FloorToInt(curScrollWidth / m_widthOneBox);
        int boxRowNum = m_totalBoxNum / boxColNum;
        
        float rowStartY = -(m_heightRow / 2);
        float rowYGap = m_heightRow + 10;

        // for constant gap between boxes in row
        float rowXGap = (curScrollWidth - boxColNum * m_widthOneBox) / (float)(boxColNum + 1);    // GapNum = boxNum + 1, GapSize = emptyWidth / GapNum
        float rowStartX = rowXGap; // for align row in middle

        // set scroll size
        m_inst_scrollContent.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (float)boxRowNum * rowYGap);

        // create row and push boxes in each row gameobject
        //m_uiInventoryOneBox_List <- Bind to this()
        for(int row = 0; row < boxRowNum; row++)
        {
            GameObject rowGO = Instantiate(m_prefab_uiRow, m_inst_scrollContent.transform);
            rowGO.transform.SetAsLastSibling();     // make last sibiling
            rowGO.GetComponent<RectTransform>().anchoredPosition = new Vector2(rowStartX, rowStartY - row * rowYGap);

            for (int col = 0; col < boxColNum; col++)
            {
                GameObject boxGO = Instantiate(m_prefab_uiOneBox, rowGO.transform);
                boxGO.transform.SetAsLastSibling();     // make last sibiling
                boxGO.GetComponent<RectTransform>().anchoredPosition = new Vector2(rowStartX + (rowXGap + m_widthOneBox) * col, 0);

                // bind UIInventoryOneBox from boxes
                UIInventoryOneBox uiInventoryOneBox = boxGO.GetComponent<UIInventoryOneBox>();
                m_uiInventoryOneBox_List.Add(uiInventoryOneBox);
            }
        }
        
        // create last line
        if (m_totalBoxNum % boxColNum != 0)
        {
            GameObject rowGO = Instantiate(m_prefab_uiRow, m_inst_scrollContent.transform);
            rowGO.transform.SetAsLastSibling();     // make last sibiling
            rowGO.GetComponent<RectTransform>().anchoredPosition = new Vector2(rowStartX, rowStartY - boxRowNum * rowYGap);
            boxRowNum++;

            for (int col = 0; col < boxColNum; col++)
            {
                GameObject boxGO = Instantiate(m_prefab_uiOneBox, rowGO.transform);
                boxGO.transform.SetAsLastSibling();     // make last sibiling
                boxGO.GetComponent<RectTransform>().anchoredPosition = new Vector2(rowStartX + rowXGap * col, 0);

                // bind UIInventoryOneBox from boxes
                UIInventoryOneBox uiInventoryOneBox = boxGO.GetComponent<UIInventoryOneBox>();
                m_uiInventoryOneBox_List.Add(uiInventoryOneBox);
            }
        }

    }

    public void DestroyBoxUIInScroll()
    {
        int childs = m_inst_scrollContent.transform.childCount;
        for (int i = childs - 1; i > 0; i--)
        {
            GameObject.Destroy(m_inst_scrollContent.transform.GetChild(i).gameObject);
        }
    }

    // Load item infos from inventory
    public void LoadBoxUIInfoInScroll()
    {
        List<ItemBox> tempInventoryOneBoxList = m_bindedInventory.InventoryOneBox_List;
        for (int i = 0; i < tempInventoryOneBoxList.Count; i++)
        {
            m_uiInventoryOneBox_List[i].LoadInventoryOneBox(tempInventoryOneBoxList[i]);
        }
    }

    // Clear item infos
    public void ClearBoxUIInfoInScroll()
    {

    }
}
