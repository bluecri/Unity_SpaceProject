using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CustumSceneManager : MonoBehaviour {
    public static CustumSceneManager instance = null;
    public const string m_middleLoadSceneName = "LoadingScene";

    public Camera m_screenShotCamera;               // will be main camera
    public Image m_loadingImage;                    // Big Loading Image
    public ProgressPanel m_loadingProgressPanel;    // Loading progress bar

    // save file screenshot size
    public int saveFileResWitdh = 360;
    public int saveFileResHeight = 360;

    private AsyncOperation m_loadSceneAsyncOp;

    enum Enum_ScreenShotFor
    {
        SaveFile = 0,
        LoadingScene
    }

    public void GotoLoadingScene(string targetSceneName)
    {
        // GetScreenShot(Enum_ScreenShotFor.LoadingScene, out screenShotBytes);
        // SaveScreenshotBytesWithPath(GetLoadingScreenShotPath(), screenShotBytes);

        // Do Screenshot
        Texture2D texture2D = null;
        GetScreenShotTexture2D(Enum_ScreenShotFor.LoadingScene, out texture2D);

        // Set m_loadingImage with prev screenshot
        m_loadingImage.sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));

        // Go to Loading Scene
        SceneManager.LoadScene(m_middleLoadSceneName);

        // Start Loading Coroutine
        UpdateProgressUI(0.0f);

        m_loadingImage.enabled = false;
        m_loadingProgressPanel.enabled = false;
        StartCoroutine(LoadScenenCoroutine(targetSceneName));
    }

    
    public void SaveScreenshotBytesWithPath(string path, byte[] screenShotBytes)
    {
        System.IO.File.WriteAllBytes(path, screenShotBytes);
        Debug.Log(string.Format("Took screenshot to: {0}", path));
    }

    private string GetLoadingScreenShotPath()
    {
        return string.Format("{0}/{1}/LoadingScreen.png",
                             Application.persistentDataPath,
                             "screenShot");
    }

    /*
     * Snapshot currnet game screen
     * Reference : https://answers.unity.com/questions/22954/how-to-save-a-picture-take-screenshot-from-a-camer.html
     */
    private void GetScreenShot(Enum_ScreenShotFor en, out byte[] screenShotBytes)
    {
        int resWidth;
        int resHeight;

        // setting resolution
        if(en == Enum_ScreenShotFor.SaveFile)
        {
            resWidth = saveFileResWitdh;
            resHeight = saveFileResHeight;
        }
        else if(en == Enum_ScreenShotFor.LoadingScene)
        {
            resHeight = Screen.height;
            resWidth = Screen.width;
        }
        else
        {
            resHeight = saveFileResWitdh;   // default value
            resWidth = saveFileResHeight;
        }

        // Create renderTexture & Render
        RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
        m_screenShotCamera.targetTexture = rt;          // camera renderTarget => renderTexture
        m_screenShotCamera.Render();
        m_screenShotCamera.targetTexture = null;        // camera renderTarget => Screen

        // Create Texture2D & Save texture as png file
        RenderTexture.active = rt;      // ReadPixel target = renderTextrue

        Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
        screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);

        RenderTexture.active = null; // JC: added to avoid errors
        Destroy(rt);

        screenShotBytes = screenShot.EncodeToPNG();
        return;

        /*
        string filename = ScreenShotName(resWidth, resHeight);
        System.IO.File.WriteAllBytes(filename, bytes);
        Debug.Log(string.Format("Took screenshot to: {0}", filename));
        takeHiResShot = false;
        */
    }

    private void GetScreenShotTexture2D(Enum_ScreenShotFor en, out Texture2D texture2D)
    {
        int resWidth;
        int resHeight;

        // setting resolution
        if (en == Enum_ScreenShotFor.SaveFile)
        {
            resWidth = saveFileResWitdh;
            resHeight = saveFileResHeight;
        }
        else if (en == Enum_ScreenShotFor.LoadingScene)
        {
            resHeight = Screen.height;
            resWidth = Screen.width;
        }
        else
        {
            resHeight = saveFileResWitdh;   // default value
            resWidth = saveFileResHeight;
        }


        // Create renderTexture & Render
        RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
        m_screenShotCamera.targetTexture = rt;          // camera renderTarget => renderTexture
        m_screenShotCamera.Render();
        m_screenShotCamera.targetTexture = null;        // camera renderTarget => Screen

        // Create Texture2D & Save texture as png file
        RenderTexture.active = rt;      // ReadPixel target = renderTextrue

        texture2D = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
        texture2D.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);

        RenderTexture.active = null; // JC: added to avoid errors
        Destroy(rt);

        return;
    }

    // reference : https://www.youtube.com/watch?v=H_0I9h4gHco
    IEnumerator LoadScenenCoroutine(string targetSceneName)
    {
        m_loadSceneAsyncOp = SceneManager.LoadSceneAsync(targetSceneName);

        while (!m_loadSceneAsyncOp.isDone)
        {
            UpdateProgressUI(m_loadSceneAsyncOp.progress);
            yield return null;
        }
        UpdateProgressUI(m_loadSceneAsyncOp.progress);

        m_loadSceneAsyncOp = null;

        // remove loading image & progress bar
        m_loadingImage.enabled = false;
        m_loadingImage.sprite = null;
        m_loadingProgressPanel.enabled = false;
    }

    private void UpdateProgressUI(float progressRatio)
    {
        m_loadingProgressPanel.SetProgressFloat(progressRatio, 1.0f);
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
