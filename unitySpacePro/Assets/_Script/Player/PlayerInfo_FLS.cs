using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerInfo_FLS : FileLoadSaveElem {

    public PlayerData  m_PlayerData;
    
    public int      m_iActivePlayer;        // active player (logic & render). 0 == Player, 1 == Ship, other == none
    public bool     m_bBattleMode;          // Is in Battle mode

    // bool state
    public bool m_bPlayerNewGame = false;    // check plyaer is new game

    // other state

    public string GetFileName()
    {
        return "playerInfo";
    }
}
