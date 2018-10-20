using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileManager : MonoBehaviour {
    public static FileManager instance = null;

    private string saveFolderName = null;
    private int selectedFileIndex = 99;
    private int maxSaveSlot = 3;

    private string saveFolderName1;

    public string SaveFolderName
    {
        get
        {
            return saveFolderName1;
        }

        set
        {
            saveFolderName1 = value;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SelectIndex(int index)
    {
        if (index >= maxSaveSlot)
        {
            Debug.Log("[WARN] : FileManager::SelectIndex(int index) : select save slot out");
            maxSaveSlot = 3;
        }

        selectedFileIndex = index;
        saveFolderName = "SaveFolder_" + selectedFileIndex.ToString();
    }
}
