using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetScnenInfo : ASceneInfo
{
    public string m_bindedSpaceSceneName;
    public string m_planetSceneName;

    public override string GetSceneIDName()
    {
        return m_planetSceneName;
    }

    override public string GetSceneName()
    {
        return m_planetSceneName;
    }
}
