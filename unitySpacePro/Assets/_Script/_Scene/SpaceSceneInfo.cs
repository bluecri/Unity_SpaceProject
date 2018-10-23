using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceSceneInfo : ASceneInfo
{
    public string m_spaceSceneName;

    public override string GetSceneName()
    {
        return m_spaceSceneName;
    }

    public override string GetSceneIDName()
    {
        return m_spaceSceneName;
    }
}
