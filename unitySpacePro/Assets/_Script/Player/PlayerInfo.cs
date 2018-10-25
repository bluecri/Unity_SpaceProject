using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerInfo_FLS : FileLoadSaveElem {

    public PropInt m_propHP;
    public PropInt m_propShield;

    // bool state
    public bool m_bPlayerNewGame = false;    // check plyaer is new game
    public bool m_bInPlayerShip = false;

    // other state

    public string GetFileName()
    {
        return "playerInfo";
    }
}
