using UnityEngine;
using System.Collections;

[System.Serializable]
public class PropFloat
{
    [SerializeField]
    private float _curValue;

    [SerializeField]
    private float _curMaxValue;
    [SerializeField]
    private float _originMaxValue;
    [SerializeField]
    private float _curMinValue;
    [SerializeField]
    private float _originMinValue;

    [SerializeField]
    private bool _bMaxLimit;     // use limit value if out boundary
    [SerializeField]
    private bool _bMinLimit;

    public bool m_BMaxLimit { get { return _bMaxLimit; } set { _bMaxLimit = value; } }
    public bool m_BMinLimit { get { return _bMinLimit; } set { _bMinLimit = value; } }

    public float m_CurValue { get { return _curValue; } }
    public float m_CurMaxValue { get { return _curMaxValue; } }
    public float m_OriginMaxValue { get { return _originMaxValue; } }
    public float m_CurMinValue { get { return _curMinValue; } }
    public float m_OriginMinValue { get { return _originMinValue; } }

    // Init all with max value
    PropFloat(float maxValue)
    {
        _curValue = maxValue;
        _curMaxValue = maxValue;
        _originMaxValue = maxValue;
    }

    // Init (maxValue = curMaxValue) & curValue
    PropFloat(float curValue, float originMaxValue)
    {
        _curValue = curValue;
        _curMaxValue = originMaxValue;
        _originMaxValue = originMaxValue;
    }

    PropFloat(float curValue, float curMaxValue, float originMaxValue)
    {
        _curValue = curValue;
        _curMaxValue = curMaxValue;
        _originMaxValue = originMaxValue;
    }

    /*
    public void SetAllValueWithData(DataPropFloat inData)
    {
        _curValue = inData._curValue;

        _curMaxValue = inData._curMaxValue;
        _originMaxValue = inData._originMaxValue;
        _curMinValue = inData._curMinValue;
        _originMinValue = inData._originMinValue;

        _bMaxLimit = inData._bMaxLimit;     // use limit value if out boundary
        _bMinLimit = inData._bMinLimit;
    }
    */

    public void SetCurValue(float curValue, bool goLimit = true)
    {
        bool b1 = (!_bMaxLimit) || (curValue <= _curMaxValue);
        bool b2 = (!_bMinLimit) || (curValue >= _curMinValue);

        if (b1 && b2)
        {
            _curValue = curValue;
            return;
        }

        if (goLimit)
        {
            if (!b1)
            {
                _curValue = _curMaxValue;
                return;
            }

            if (!b2)
            {
                _curValue = _curMinValue;
                return;
            }
        }
    }

    public void SetCurValueAcc(float accValue, bool goLimit = true)
    {
        float curValue = _curValue + accValue;
        SetCurValue(curValue);
    }

    public void SetCurValueDec(float decValue, bool goLimit = true)
    {
        float curValue = _curValue - decValue;
        SetCurValue(curValue);
    }

    public void SetCurMinValue(float curMinValue)
    {
        if (curMinValue <= _curMaxValue)
        {
            _curMinValue = curMinValue;
        }
        else
        {
            Debug.Log("class PropFloat<float>::SetCurMinValue(float curMinValue) : range out");
        }
    }

    public void SetCurMaxValue(float curMaxValue)
    {
        if (curMaxValue >= _curMinValue)
        {
            _curMaxValue = curMaxValue;
        }
        else
        {
            Debug.Log("class PropFloat<float>::SetCurMaxValue(float curMaxValue) : range out");
        }
    }

    public void SetOriginMinValue(float originMinValue)
    {
        if (originMinValue <= _originMaxValue)
        {
            _originMinValue = originMinValue;
        }
        else
        {
            Debug.Log("class PropFloat<float>::SetCurMinValue(float curMinValue) : range out");
        }
    }

    public void SetOriginMaxValue(float originMaxValue)
    {
        if (originMaxValue >= _originMinValue)
        {
            _originMaxValue = originMaxValue;
        }
        else
        {
            Debug.Log("class PropFloat<float>::SetOriginMaxValue(float originMaxValue) : range out");
        }
    }
}
