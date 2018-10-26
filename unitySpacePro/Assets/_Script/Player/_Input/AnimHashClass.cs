using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimHashClass : MonoBehaviour{
    public static AnimHashClass instance = null;

    // player hash
    public readonly int m_hash_iDirection    = Animator.StringToHash("MoveDirection_int");

    public readonly int m_hash_bModeRun      = Animator.StringToHash("RunMode_Bool");
    public readonly int m_hash_bModeBattle   = Animator.StringToHash("BattleMode_Bool");
    public readonly int m_hash_bModeMove     = Animator.StringToHash("MoveMode_Bool");

    public readonly int m_hash_fMotionMult   = Animator.StringToHash("Motion_Multiply");
    public readonly int m_hash_fMotionIdleMult = Animator.StringToHash("IdleMotion_Multiply");

    public readonly int m_hash_iShotRandom   = Animator.StringToHash("ShotRandom_Int");  // 0~3
    public readonly int m_hash_tShotTrigger  = Animator.StringToHash("ShotTrigger");


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
