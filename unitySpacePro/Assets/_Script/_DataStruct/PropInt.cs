using UnityEngine;
using System.Collections;

[System.Serializable]
public class PropInt
{
    [SerializeField]
    private int _curValue;

    [SerializeField]
    private int _curMaxValue;
    [SerializeField]
    private int _originMaxValue;
    [SerializeField]
    private int _curMinValue;
    [SerializeField]
    private int _originMinValue;

    [SerializeField]
    private bool _bMaxLimit;     // use limit value if out boundary
    [SerializeField]
    private bool _bMinLimit;

    public bool m_BMaxLimit { get { return _bMaxLimit; } set { _bMaxLimit = value; } }
    public bool m_BMinLimit { get { return _bMinLimit; } set { _bMinLimit = value; } }

    public int m_CurValue { get { return _curValue; } }
    public int m_CurMaxValue { get { return _curMaxValue; } }
    public int m_OriginMaxValue { get { return _originMaxValue; } }
    public int m_CurMinValue { get { return _curMinValue; } }
    public int m_OriginMinValue { get { return _originMinValue; } }

    // Init all with max value
    PropInt(int maxValue)
    {
        _curValue = maxValue;
        _curMaxValue = maxValue;
        _originMaxValue = maxValue;
    }

    // Init (maxValue = curMaxValue) & curValue
    PropInt(int curValue, int originMaxValue)
    {
        _curValue = curValue;
        _curMaxValue = originMaxValue;
        _originMaxValue = originMaxValue;
    }

    PropInt(int curValue, int curMaxValue, int originMaxValue)
    {
        _curValue = curValue;
        _curMaxValue = curMaxValue;
        _originMaxValue = originMaxValue;
    }

    /*
    public void SetAllValueWithData(DataPropInt inData)
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

    public void SetCurValue(int curValue, bool goLimit = true)
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

    public void SetCurValueAcc(int accValue, bool goLimit = true)
    {
        int curValue = _curValue + accValue;
        SetCurValue(curValue);
    }

    public void SetCurValueDec(int decValue, bool goLimit = true)
    {
        int curValue = _curValue - decValue;
        SetCurValue(curValue);
    }

    public void SetCurMinValue(int curMinValue)
    {
        if (curMinValue <= _curMaxValue)
        {
            _curMinValue = curMinValue;
        }
        else
        {
            Debug.Log("class PropInt<int>::SetCurMinValue(int curMinValue) : range out");
        }
    }

    public void SetCurMaxValue(int curMaxValue)
    {
        if (curMaxValue >= _curMinValue)
        {
            _curMaxValue = curMaxValue;
        }
        else
        {
            Debug.Log("class PropInt<int>::SetCurMaxValue(int curMaxValue) : range out");
        }
    }

    public void SetOriginMinValue(int originMinValue)
    {
        if (originMinValue <= _originMaxValue)
        {
            _originMinValue = originMinValue;
        }
        else
        {
            Debug.Log("class PropInt<int>::SetCurMinValue(int curMinValue) : range out");
        }
    }

    public void SetOriginMaxValue(int originMaxValue)
    {
        if (originMaxValue >= _originMinValue)
        {
            _originMaxValue = originMaxValue;
        }
        else
        {
            Debug.Log("class PropInt<int>::SetOriginMaxValue(int originMaxValue) : range out");
        }
    }
}
