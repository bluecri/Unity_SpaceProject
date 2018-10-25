using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Reflection;
/*
 * FileManager manages folder & folder root name & current data slot Index.
 * Also, Load & Save Global Data (ex : user inventory, player data..)
 * Data relate with Scene(Stage) should be loaded or saved by that Scene & SceneManager
 */

public class FileManager : MonoBehaviour {

    [HideInInspector]
    public static FileManager instance = null;

    private List<FileLoadSaveElem> m_targetFileNoRelatedToScene_list;  // list of load&save not related to scene

    private string m_saveOriginFolderName = null;
    private string m_saveTempFolderName = null;

    private int m_selectedFileIndex = 99;
    private int maxSaveSlot = 3;

    private static string topFolderName = "Save_Data";
    private static string slotFolderName = "SlotFolder_";
    private static string originForlderName = "origin";
    private static string tempForlderName = "temp";

    public void Init_FileManager()
    {
        // Create target of global save & load

        // user inventory(Global)
        Inventorymanager invenInstance = Inventorymanager.instance;

        m_targetFileNoRelatedToScene_list = new List<FileLoadSaveElem>();
        m_targetFileNoRelatedToScene_list.Add(invenInstance.m_PlayerInventory);
    }

    public List<FileLoadSaveElem> m_TargetFileNoRelatedToScene_list
    {
        get
        {
            return m_targetFileNoRelatedToScene_list;
        }

        set
        {
            m_targetFileNoRelatedToScene_list = value;
        }
    }

    // select save & load slot index
    public void SelectIndex(int index)
    {
        if (index >= maxSaveSlot)
        {
            Debug.Log("[WARN] : FileManager::SelectIndex(int index) : select save slot out");
            maxSaveSlot = 3;
        }

        m_selectedFileIndex = index;

        // Generate folder root string
        GenerateString();
    }

    public void LoadGlobal()
    {
        foreach(var elem in m_TargetFileNoRelatedToScene_list)
        {
            LoadClassData(elem, false);
        }
    }

    public void SaveGlobal()
    {
        foreach (var elem in m_TargetFileNoRelatedToScene_list)
        {
            SaveClassData(elem, false);
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    // Remove selected slot file & create new slot
    private void RemoveAndMakeFolder()
    {
        // create root folder : projDatas
        System.IO.Directory.CreateDirectory(Application.persistentDataPath + topFolderName);

        // Remove number folder : projDatas/SaveFolder_1
        try
        {
            System.IO.Directory.Delete(Application.persistentDataPath + topFolderName + "/" + slotFolderName + m_selectedFileIndex.ToString(), true);
        }

        catch (System.IO.IOException e)
        {
            Debug.Log("[EXCPETION] FileManager:void RemoveAndMakeFolder(): " + e.Message);
        }

        // create number folder : projDatas/SaveFolder_1
        System.IO.Directory.CreateDirectory(Application.persistentDataPath + topFolderName + "/" + slotFolderName + m_selectedFileIndex.ToString());

        // create temp & origin folder : projDatas/SaveFolder_1/temp && projDatas/SaveFolder_1/origin
        System.IO.Directory.CreateDirectory(m_saveOriginFolderName);
        System.IO.Directory.CreateDirectory(m_saveTempFolderName);
    }

    private void MakeFolderOnly()
    {
        // create root folder : projDatas
        System.IO.Directory.CreateDirectory(Application.persistentDataPath + topFolderName);
        
        // create number folder : projDatas/SaveFolder_1
        System.IO.Directory.CreateDirectory(Application.persistentDataPath + topFolderName + "/" + slotFolderName + m_selectedFileIndex.ToString());

        // create temp & origin folder : projDatas/SaveFolder_1/temp && projDatas/SaveFolder_1/origin
        System.IO.Directory.CreateDirectory(m_saveOriginFolderName);
        System.IO.Directory.CreateDirectory(m_saveTempFolderName);
    }

    private void GenerateString()
    {
        m_saveOriginFolderName = Application.persistentDataPath + topFolderName + "/" + slotFolderName + m_selectedFileIndex.ToString() + "/" + originForlderName;
        m_saveTempFolderName = Application.persistentDataPath + topFolderName + "/" + slotFolderName + m_selectedFileIndex.ToString() + "/" + tempForlderName;
    }

    private string GetTypeFileName(FileLoadSaveElem data)
    {
        return data.GetType().ToString() + "_" + data.GetStrID();
    }

    private string GenTargetFileName(FileLoadSaveElem data, bool bTemp)
    {
        if (bTemp)
        {
            return m_saveTempFolderName + "/" + GetTypeFileName(data) + ".dat";
        }
        else
        {
            return m_saveOriginFolderName + "/" + GetTypeFileName(data) + ".dat";
        }
    }

    private void SaveClassData(FileLoadSaveElem data, bool bTemp)
    {
        string targetFileName = GenTargetFileName(data, bTemp);
        
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(targetFileName, FileMode.Create);

            bf.Serialize(file, data);

            file.Close();
        }
        catch (IOException e)
        {
            Debug.Log(e.ToString() + "FileManager::SaveClassData save error");
        }
    }

    private void LoadClassData(FileLoadSaveElem data, bool bTemp)
    {
        string targetFileName = GenTargetFileName(data, bTemp);

        // check file exist
        if (File.Exists(targetFileName))
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(targetFileName, FileMode.Open);

                Object loadedData = (Object)bf.Deserialize(file);
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
    private void CopyPropertiesToThis(Object obj)
    {
        // ref : http://telegraphrepaircompany.com/using-reflection-loop-properties-object-c/
        PropertyInfo[] properties = obj.GetType().GetProperties();
        foreach (var property in properties)
        {
            //if (property.GetValue(this, null) != null)
            property.SetValue(this, property.GetValue(obj, null), null);
        }
    }

}
