using UnityEngine;
using System.Collections;

/*
 * If class (no monobehavior) has this interface,
 * can save & load through FileManager
 */

public interface IFileLoadSave
{
    string GetFileName();
}
