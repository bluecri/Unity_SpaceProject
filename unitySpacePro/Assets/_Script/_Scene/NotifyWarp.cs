using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotifyWarp : MonoBehaviour {
    public Collider m_collider;
    public ASceneInfo m_targetASceneInfo;

    public GameObject m_enterPortalStart;
    public GameObject m_enterPortalEnd;
    public GameObject m_exitPortalStart;
    public GameObject m_exitPortalEnd;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("PlayerShip"))
        {
            // warp animation begin
        }
    }

    public void ActivatePotal(bool b)
    {
        m_collider.enabled = b;
    }

}
