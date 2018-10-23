using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressPanel : MonoBehaviour {

    private Image   m_progressImage;
    private Text    m_progressText;

    public bool m_bTextPercent;     // set text with percent
    public bool m_bTextInt;         // text represent is int or float

    private void OnEnable()
    {
        m_progressImage = this.transform.Find("ProgressBarImage").GetComponent<Image>();
        m_progressText = this.transform.Find("ProgressText").GetComponent<Text>();
    }

    // set progress with percent( num / maxVal )
    public void SetProgressFloat(float num, float maxVal)
    {
        float ratio = (float)num / (float)maxVal;

        if (m_progressImage != null)
        {
            m_progressImage.fillAmount = ratio;
        }

        if(m_progressText != null)
            m_progressText.text = GetStringFromFloat(num, maxVal);
    }

    public void SetProgressInt(int num, int maxVal)
    {
        float ratio = (float)num / (float)maxVal;

        if (m_progressImage != null)
        {
            m_progressImage.fillAmount = ratio;
        }

        m_progressText.text = GetStringFromInt(num, maxVal);
    }



    private string GetStringFromFloat(float num, float maxVal)
    {
        if (m_progressText == null)
            return "";
        
        string retText;
        float ratio = num / maxVal;

        if (m_bTextPercent)
        {
            // use ratio
            if (m_bTextInt)
            {
                retText = ((int)(ratio * 100)).ToString() + "%";
            }
            else
            {
                retText = (ratio * 100).ToString("0.0") + "%";
            }
        }
        else
        {
            // use num
            if (m_bTextInt)
            {
                retText = num.ToString();
            }
            else
            {
                retText = num.ToString("0.0");
            }
        }

        return retText;
    }

    private string GetStringFromInt(int num, int maxVal)
    {
        if (m_progressText == null)
            return "";

        string retText;
        float ratio = (float)num / (float)maxVal;

        if (m_bTextPercent)
        {
            // use ratio
            if (m_bTextInt)
            {
                retText = ((int)(ratio * 100)).ToString() + "%";
            }
            else
            {
                retText = (ratio * 100).ToString("0.0") + "%";
            }
        }
        else
        {
            // use num
            if (m_bTextInt)
            {
                retText = num.ToString();
            }
            else
            {
                retText = num.ToString("0.0");
            }
        }

        return retText;
    }

}
