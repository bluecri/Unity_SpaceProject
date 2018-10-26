using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGameCharacters : MonoBehaviour
{
    [System.Serializable]
    public class NewGamePlayerInfo
    {
        public GameObject       m_CharacterPrefabs;     // prefab of player (nodel, anim...)
        public PlayerInfo_FLS   m_Infos;            // player info (hp, shield ..)
        public Inventory        m_inventory;             // inventory (weight, size...)
    }

    public NewGamePlayerInfo[] m_NewGameCharaInfo;
}