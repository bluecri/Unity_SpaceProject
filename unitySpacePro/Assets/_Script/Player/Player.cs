using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public PlayerInfo_FLS m_playerInfo;
    public Camera m_mainCam;
	
    public void Init_Player()
    {
        m_mainCam = Camera.main;
    }

    private void Start()
    {
    }

    // Update is called once per frame
    void Update () {
		
	}
}
