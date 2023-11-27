using UnityEngine;
using Zenject;

public class TimerManager : ITickable, IInitializable
{
    private float _second;
    private int _minute;
    
    public void Initialize()
    {
        _second = 0;
        _minute = 0;
    }

    public void Tick()
    {
        _second += Time.deltaTime;

        if (_second >= 60)
        {
            _minute++;
            _second %= 60;
        }

        Debug.Log($"[{_minute},{(int)_second}]");
    }
}