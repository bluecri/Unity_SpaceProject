using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public abstract class ASceneInfo : MonoBehaviour
{
    abstract public string GetSceneName();
    abstract public string GetSceneIDName();
}
