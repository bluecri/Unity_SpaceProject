using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public FileManager fileManagerInstance;

	// Use this for initialization
	void Start () {

        // Get All Instance
        fileManagerInstance = FileManager.instance;

        // GUI Inspector Init
        ItemManager.instance.Init_ItemManager();    // Init ItemManager

        // Init no dependency
        Inventorymanager.instance.Init_Inventorymanager();  // Init Inventorymanager
        FileManager.instance.Init_FileManager();    // Init FileManager

    }

    void StartNewGame()
    {
        // Scene move to next
    }

    void ContinueGame()
    {
        fileManagerInstance.LoadGlobal();
        Inventorymanager.instance.LoadInventoryToUI();

        // Scene move to next
    }


    void SaveGame()
    {
        fileManagerInstance.SaveGlobal();

        // Scene move to next

    }

}
