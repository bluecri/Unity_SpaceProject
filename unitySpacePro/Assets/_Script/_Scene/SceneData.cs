using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * SceneData has ASceneInfo & has only 1 in SceneData GameObject in each Scene
 * m_gameObjectHasSceneInfo that has SceneInfo. Use for position of portal.
 */

public class SceneData : MonoBehaviour
{
    public ASceneInfo       m_curSceneInfo;
    
    public GameObject[]     m_warpGameObjectArr;   
    private Dictionary<string, ASceneInfo> m_sceneNameToSceneInfo_map;

    private void Awake()
    {
        m_sceneNameToSceneInfo_map = new Dictionary<string, ASceneInfo>();
    }

    public void Init_SceneData()
    {
       // init m_sceneNameToSceneInfo_map
       foreach(GameObject go in m_warpGameObjectArr)
        {
            ASceneInfo elem = go.GetComponent<NotifyWarp>().m_targetASceneInfo;
            m_sceneNameToSceneInfo_map.Add(elem.GetSceneIDName(), elem);    // Use IDName
        }
    }

    public ASceneInfo GetCurrentASceneInfo()
    {
        return m_curSceneInfo;
    }
    
    public void GetOtherASceneInfoWithIDName(string idName, out ASceneInfo info)
    {
        if(m_sceneNameToSceneInfo_map.TryGetValue(idName, out info))
        {
            return;
        }
        info = null;
    }
    
}
