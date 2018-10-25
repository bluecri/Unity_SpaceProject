using UnityEngine;
using System.Collections;

/*
 * If class (no monobehavior) has this interface,
 * can save & load through FileManager
 */

[System.Serializable]
public class FileLoadSaveElem
{
    public string m_FLSStringID;

    public string GetStrID()
    {
        return m_FLSStringID;
    }
}
