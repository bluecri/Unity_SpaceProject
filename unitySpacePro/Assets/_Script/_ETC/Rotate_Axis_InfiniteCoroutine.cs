using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_Axis_InfiniteCoroutine : MonoBehaviour {
    public float m_rotateSpeed;
    public int m_rotateAxis;

    private Vector3 m_rotateVec3;

	void Start ()
    {
        m_rotateVec3 = new Vector3();
        StartCoroutine("RotateCoroutine");
    }

    IEnumerator RotateCoroutine()
    {
        while(true)
        {
            m_rotateVec3[m_rotateAxis] = m_rotateSpeed * Time.deltaTime;
            transform.Rotate(m_rotateVec3);
            yield return null;
        }
    }

}
