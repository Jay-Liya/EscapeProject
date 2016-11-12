using System.Timers;
using UnityEngine;

public class FakeAccelerometer : MonoBehaviour
{
    private int steps = 10;
    private float _xCurrent = 0;
    private float _yCurrent = Mathf.PI;
    private float _zCurrent = 2*Mathf.PI;
    private Timer _aTimer = new Timer();

    public float XCurrent
    {
        get
        {  
            return Mathf.Sin(_xCurrent);
        }
                
    }

    public float YCurrent
    {
        get
        {
            return _yCurrent;
        }
                
    }

    public void Start()
    {

    }

    public void NextValues()
    {
        _xCurrent = _xCurrent + (2 * Mathf.PI / steps);

        if (_xCurrent > 2 * Mathf.PI)
        {
            _xCurrent = _xCurrent - (2 * Mathf.PI);
        }

        _yCurrent = _yCurrent + (2 * Mathf.PI / steps);

        if (_yCurrent > 2 * Mathf.PI)
        {
            _yCurrent = _yCurrent - (2 * Mathf.PI);
        }
    }
}


