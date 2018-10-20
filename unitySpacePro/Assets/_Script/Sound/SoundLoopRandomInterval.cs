using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SoundLoopRandomInterval : MonoBehaviour
{
    public AudioClip audioClip;
    public float randomFixedAcc;

    [HideInInspector]
    public float randomMin;
    [HideInInspector]
    public float randomMax;
    [HideInInspector]
    public bool randomInterval = false;

    private AudioSource audioSource;
    private float audioClipLen;
    private float playAdditionalSpace = 0.1f;

    // Use this for initialization
    void Start ()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        if(null != audioClip)
        {
            audioSource.clip = audioClip;
            audioSource.playOnAwake = false;
            audioClipLen = audioClip.length;

            StartCoroutine("PlayAudioCoroutine");
        }
    }
	

    IEnumerator PlayAudioCoroutine()
    {
        while(true)
        {
            // calc next play sound time = audio play length + fixed play length interval + real next interval
            float nextPlayTimeInterval = audioClipLen + playAdditionalSpace + CalcNextInterval();
            yield return new WaitForSeconds(nextPlayTimeInterval);

            audioSource.Stop();
            audioSource.Play();

            yield return new WaitForEndOfFrame();
        }
       
    }

    // Calculate interval
    private float CalcNextInterval()
    {
        if(randomInterval)
            return Random.Range(randomMin, randomMax) + randomFixedAcc;

        return randomFixedAcc;
    }

    private void OnDestroy()
    {
        StopCoroutine("PlayAudioCoroutine");
    }
}


[CustomEditor(typeof(SoundLoopRandomInterval))]
[CanEditMultipleObjects]
public class SoundLoopRandomInterval_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        serializedObject.Update();

        SoundLoopRandomInterval myScript = target as SoundLoopRandomInterval;

        myScript.randomInterval = GUILayout.Toggle(myScript.randomInterval, " randomInterval");

        if (myScript.randomInterval)
        {
            myScript.randomMin = EditorGUILayout.FloatField("randomMin", myScript.randomMin);
            myScript.randomMax = EditorGUILayout.FloatField("randomMax", myScript.randomMax);
        }

    }
}