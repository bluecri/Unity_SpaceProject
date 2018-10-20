using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoDestroyOnLoad : MonoBehaviour {

    private static bool m_created = false;

    void Awake()
    {
        if (!m_created)
        {
            DontDestroyOnLoad(this.gameObject);
            m_created = true;
            Debug.Log("NoDestroyOnLoad: " + this.gameObject);
        }
    }
}