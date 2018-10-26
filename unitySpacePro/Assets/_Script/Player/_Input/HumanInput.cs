using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanInput {
    public float m_basicFBSpeed = 1.0f;
    public float m_basicLRSpeed = 1.0f;

    public float m_FBSpeed = 0.0f;     // front back speed
    public float m_LRSpeed = 0.0f;     // left right speed
    public float m_runSpeedFBMult = 1.6f;
    public float m_runSpeedLRMult = 1.3f;

    // anim parameter. Only update if param is changed
    public bool m_paramMoveMode = false;
    public bool m_paramRunMode = false;
    public bool m_paramBattleMode = false;
    public int  m_paramDirection = 0;            // WSAD

    // Include mouse key
    public void Update_HumanInput_Key(PlayerScriptInObject psio)
    {
        bool frontKeyOn = Input.GetKey(KeyCode.W);
        bool backKeyOn  = Input.GetKey(KeyCode.S);
        bool leftKeyOn  = Input.GetKey(KeyCode.A);
        bool rightKeyOn = Input.GetKey(KeyCode.D);
        bool runKeyOn   = Input.GetKey(KeyCode.LeftShift);

        // init value
        m_FBSpeed = 0.0f;
        m_LRSpeed = 0.0f;

        if (frontKeyOn)
        {
            m_FBSpeed += m_basicFBSpeed;
        }
        if (backKeyOn)
        {
            m_FBSpeed -= m_basicFBSpeed;
        }
        if (leftKeyOn)
        {
            m_LRSpeed -= m_basicLRSpeed;
        }
        if (rightKeyOn)
        {
            m_LRSpeed += m_basicLRSpeed;
        }

        if(runKeyOn)
        {
            m_FBSpeed *= m_runSpeedFBMult;
            m_LRSpeed *= m_runSpeedLRMult;
        }

        // if move, set animation
        bool checkMove = false;
        int newDirection = -1;

        /*
         * If Keys are { Forward, Forward + Left, Forward + Right }, then Use Forward animation
         * If Keys are { Left, Right, Backward + Right}, then Use Left, Right animation
         * If Keys are { Backward },  then Use Backward animation
         */

        // animation front
        if(m_FBSpeed > float.Epsilon)
        {
            checkMove = true;
            newDirection = 0;
        }
        else
        {
            // animation LR
            if (m_LRSpeed < -float.Epsilon)
            {
                // Left
                checkMove = true;
                newDirection = 2;
            }
            else if(m_LRSpeed > float.Epsilon)
            {
                // Left
                checkMove = true;
                newDirection = 3;
            }
            else
            {
                // Back move
                if (m_FBSpeed < -float.Epsilon)
                {
                    checkMove = true;
                    newDirection = 1;
                }
            }
        }

        // animation set with mode ( battle, run, move )

        if (m_paramDirection != newDirection)
        {
            m_paramDirection = newDirection;
            psio.m_anim_1.SetInteger(AnimHashClass.instance.m_hash_iDirection, m_paramDirection);
            psio.m_anim_3.SetInteger(AnimHashClass.instance.m_hash_iDirection, m_paramDirection);
        }

        if (m_paramBattleMode != psio.m_cachePlayerInfo.m_bBattleMode)
        {
            m_paramBattleMode = psio.m_cachePlayerInfo.m_bBattleMode;
            psio.m_anim_1.SetBool(AnimHashClass.instance.m_hash_bModeBattle, m_paramBattleMode);
            psio.m_anim_3.SetBool(AnimHashClass.instance.m_hash_bModeBattle, m_paramBattleMode);
        }

        // If no arrow key input, Run Mode is Off.
        if(!checkMove)
        {
            runKeyOn = false;
        }

        // If Run Mode, should turn off Move Mode
        if (runKeyOn)
        {
            if(m_paramRunMode)
            {
                // set move (off)
                checkMove = false;
            }
            else
            {
                // set move (on/off)
            }
        }

        if (m_paramRunMode != runKeyOn)
        {
            m_paramRunMode = runKeyOn;

            psio.m_anim_1.SetBool(AnimHashClass.instance.m_hash_bModeRun, m_paramRunMode);
            psio.m_anim_3.SetBool(AnimHashClass.instance.m_hash_bModeRun, m_paramRunMode);

        }

        if (m_paramMoveMode != checkMove)
        {
            m_paramMoveMode = checkMove;
            psio.m_anim_1.SetBool(AnimHashClass.instance.m_hash_bModeMove, m_paramMoveMode);
            psio.m_anim_3.SetBool(AnimHashClass.instance.m_hash_bModeMove, m_paramMoveMode);
        }
    }

    public void Update_HumanInput_MouseMove(PlayerScriptInObject psio)
    {
        // TODO : Use option manager for mouse sensitivity

    }

        public void FixedUpdate_HumanInput(PlayerScriptInObject psio)
    {
        Rigidbody rg = psio.m_playerRigidbody;
        if (rg == null)
            Debug.Log("HumanInput::FixedUpdate_HumanInput : Rigidbody is null");

        rg.velocity = psio.m_transform.forward * m_FBSpeed + 
                            psio.m_transform.right * m_LRSpeed;
        //rg.velocity = new Vector3(, 0.0f, m_FBSpeed);
    }
}
