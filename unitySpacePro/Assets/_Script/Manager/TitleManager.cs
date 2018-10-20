using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour {

    public Animator titleAnimator;

    private int m_OpenParameterId;  // transition hash
    private Animator m_Open;        // for none copy & null check

    const string k_OpenTransitionName = "Open";         // transition name(AC param)
    const string k_ClosedStateName = "MoveOut";          // state name(Animation name)

    public void OnEnable()
    {
        m_OpenParameterId = Animator.StringToHash(k_OpenTransitionName);    
        
        if (titleAnimator != null)
        {
            OpenTitle(titleAnimator);
        }
    }

    public void OpenTitle(Animator anim)
    {
        m_Open = anim;
        if (m_Open == null)
            return;

        m_Open.gameObject.SetActive(true);
        m_Open.transform.SetAsLastSibling();
        m_Open.SetBool(m_OpenParameterId, true);        // animation param set
    }

    public void CloseTitle(Animator anim)
    {
        m_Open = anim;
        if (m_Open == null)
            return;

        m_Open.SetBool(m_OpenParameterId, false);
        StartCoroutine(DisablePanelDeleyed(m_Open));    // Make inactive if anim end
        m_Open = null;
    }

    IEnumerator DisablePanelDeleyed(Animator anim)
    {
        bool closedStateReached = false;
        bool wantToClose = true;
        while (!closedStateReached && wantToClose)
        {
            if (!anim.IsInTransition(0))
                closedStateReached = anim.GetCurrentAnimatorStateInfo(0).IsName(k_ClosedStateName);

            wantToClose = !anim.GetBool(m_OpenParameterId);

            yield return new WaitForEndOfFrame();
        }

        if (wantToClose)
            anim.gameObject.SetActive(false);
    }
}
