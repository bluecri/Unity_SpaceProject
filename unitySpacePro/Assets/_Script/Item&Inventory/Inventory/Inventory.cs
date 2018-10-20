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
public class Inventory {
    private string          m_name;
    private int             m_sizeCapacity;
    private float           m_weightCapacity;
    private List<InventoryOneBox>   m_InventoryOneBox_List;

    public Inventory(string name, int size = 12, float weight = 20.0f)
    {
        m_name = name;
        SetProperties(size, weight);
    }

    public void SetProperties(int size, float weight)
    {
        SizeCapacity = size;
        WeightCapacity = weight;
        InventoryOneBox_List = new List<InventoryOneBox>(SizeCapacity);
        for(int i=0; i<SizeCapacity; i++)
        {
            InventoryOneBox_List.Add(new InventoryOneBox(null, 0));
        }
    }
    
    public void SaveInventory()
    {
        string targetFileName = Application.persistentDataPath + FileManager.instance.SaveFolderName + "/" + m_name + ".dat";
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(targetFileName, FileMode.Create);

            Inventory data = this;
            bf.Serialize(file, data);

            file.Close();
        }
        catch (IOException e)
        {
            Debug.Log(e.ToString() + "SavePlayerInfo save error");
        }
    }
    
    public void LoadInventory()
    {
        string targetFileName = Application.persistentDataPath + FileManager.instance.SaveFolderName + "/" + m_name + ".dat";
        // check file exist
        if (File.Exists(targetFileName))
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(targetFileName, FileMode.Open);

                Inventory loadedData = (Inventory)bf.Deserialize(file);
                CopyPropertiesToThis(loadedData);

                file.Close();
            }
            catch (IOException e)
            {
                Debug.Log(e.ToString() + "LoadPlayerInfo Load error");
                
            }
        }
        else
        {
            Debug.Log("[WARN] : Inventory::LoadPlayerInfo() : No file exist");
            // Use current inventory (with properties with creation)
        }
    }

    // copy all properties to this
    public void CopyPropertiesToThis(Inventory data)
    {
        // ref : http://telegraphrepaircompany.com/using-reflection-loop-properties-object-c/
        PropertyInfo[] properties = this.GetType().GetProperties();
        foreach (var property in properties)
        {
            //if (property.GetValue(this, null) != null)
            property.SetValue(this, property.GetValue(data, null), null);
        }
    }

    public int SizeCapacity
    {
        get
        {
            return m_sizeCapacity;
        }

        set
        {
            m_sizeCapacity = value;
        }
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

    public List<InventoryOneBox> InventoryOneBox_List
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
}
