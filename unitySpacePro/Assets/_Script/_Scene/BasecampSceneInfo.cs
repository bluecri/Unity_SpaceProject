using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasecampSceneInfo : ASceneInfo
{
    public string m_bindedSpaceSceneName;  
    public const string m_baseSceneName = "Basecamp";
    public int m_index;                    // If in Basecamp, represent out portal index

    public override string GetSceneIDName()
    {
        return m_baseSceneName + "__" + m_index.ToString();
    }

    public override string GetSceneName()
    {
        return m_baseSceneName;
    }
}
