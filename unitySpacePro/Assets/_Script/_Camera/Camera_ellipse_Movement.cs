using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_ellipse_Movement : MonoBehaviour
{
    public float m_pitchMax = 60.0f;
    public float m_pitchMin = -60.0f;
    public float m_pitch_Max_PlayerObj = 30.0f;
    public float m_pitch_Min_PlayerObj = -30.0f;


    public float m_xPosFixed = 0.5f;
    public float m_yPosCompensation = 1.5f;
    public float m_pitchCompensation = -45.0f;

    // ellipse point = ( radY * cos, radZ * sin )
    public float m_radY = 1.0f;
    public float m_radZ = 2.0f;

    private float m_pitch = 0f;
    private float m_pitch_playerObj = 0f;
    private float m_yaw = 0f;

    public float m_rotationSpeed = 1.0f;
    
    public void Camera_ellipse_Movement_Update(PlayerScriptInObject psio)
    {
        m_pitch += m_rotationSpeed * Input.GetAxis("Mouse Y");
        m_yaw += m_rotationSpeed * Input.GetAxis("Mouse X");

        // Clamp pitch:
        m_pitch = Mathf.Clamp(m_pitch, m_pitchMin, m_pitchMax);
        m_pitch_playerObj = Mathf.Clamp(m_pitch, m_pitch_Min_PlayerObj, m_pitch_Max_PlayerObj);

        // Wrap yaw:
        while (m_yaw < 0f)
        {
            m_yaw += 360f;
        }
        while (m_yaw >= 360f)
        {
            m_yaw -= 360f;
        }

        // Set cam rotation
        psio.m_pos_cam_3.transform.eulerAngles = new Vector3(-m_pitch, m_yaw, 0f);

        
        // Set cam position with ellipse
        float pitchConversion = -m_pitch + m_pitchCompensation;

        // ellipse point = ( radY * cos, radZ * sin )
        Vector3 newCamLocalPos = new Vector3(m_xPosFixed, m_radY * Mathf.Cos(Mathf.Deg2Rad * pitchConversion) + m_yPosCompensation, m_radZ * Mathf.Sin(Mathf.Deg2Rad * pitchConversion));
        psio.m_pos_cam_3.transform.localPosition = newCamLocalPos;

        // Set player rotation
        psio.m_playerRigidbody.transform.eulerAngles = new Vector3(-m_pitch_playerObj, m_yaw, 0f);
    }

}
