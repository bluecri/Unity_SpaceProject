using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
    public static PlayerManager instance = null;

    public Player m_player;

    public void Init_PlayerManager()
    {
        m_player.Init_Player();
    }

    public void LoadPlayerInfo()
    {
        // m_player.m_playerInfo
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
}
