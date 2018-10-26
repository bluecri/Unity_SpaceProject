using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This class should be in human player object.
 * This class manage player object & communicate with player in playerManager!
 */

public class PlayerScriptInObject : MonoBehaviour {
    
    // position objects
    public GameObject m_pos_cam_1;
    public GameObject m_pos_cam_3;
    public GameObject m_pos_handWeapon_1;
    public GameObject m_pos_handWeapon_3;

    public Collider m_collider_hitbox;
    public Collider m_collider_base;

    // animator
    public Animator m_anim_1;
    public Animator m_anim_3;

    // camera script obeject
    public Camera_ellipse_Movement m_Camera_ellipse_Movement_TPS;

    [HideInInspector]
    public Transform m_transform;
    [HideInInspector]
    public Rigidbody m_playerRigidbody;

    private HumanInput m_HumanInput;
    
    [HideInInspector]
    public Player m_cache_player;
    [HideInInspector]
    public PlayerInfo_FLS m_cachePlayerInfo;

    // Use this for initialization
    void Start () {
        m_HumanInput = new HumanInput();
    }

    private void OnEnable()
    {
        m_playerRigidbody   = GetComponent<Rigidbody>();
        m_transform         = this.transform;
        m_cache_player      = PlayerManager.instance.m_player;
        m_cachePlayerInfo   = PlayerManager.instance.m_player.m_playerInfo;
    }

    // Update is called once per frame
    void Update () {
        m_HumanInput.Update_HumanInput_Key(this);
        m_Camera_ellipse_Movement_TPS.Camera_ellipse_Movement_Update(this);
    }

    private void FixedUpdate()
    {
        m_HumanInput.FixedUpdate_HumanInput(this);
    }
}
