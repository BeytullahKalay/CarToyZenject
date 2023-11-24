using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Zenject;

public class CircleManager : IInitializable, IDisposable
{
    private List<GameObject> _circles;
    private int _currentCircle;
    private SignalBus _onTriggeredWithCircle;
    private CircleTweenSettings _circleTweenSettings;

    public CircleManager(CircleManagerSettings circleManagerSettings, SignalBus onTriggeredWithCircle
        , CircleTweenSettings circleTweenSettings)
    {
        _circles = circleManagerSettings.Circles;
        _circleTweenSettings = circleTweenSettings;
        _onTriggeredWithCircle = onTriggeredWithCircle;
        _currentCircle = 0;

        InitCircles();
    }

    private void InitCircles()
    {
        foreach (var circle in _circles)
        {
            circle.transform.DOScale(Vector3.one * _circleTweenSettings.ScalingAmount,
                _circleTweenSettings.ScalingDuration).SetLoops(-1, LoopType.Yoyo).SetRelative();
            circle.SetActive(false);
        }

        _circles[0].SetActive(true);
    }

    public void Initialize()
    {
        _onTriggeredWithCircle.Subscribe<OnTriggeredWithCircle>(TriggeredActions);
    }

    public void Dispose()
    {
        _onTriggeredWithCircle.Unsubscribe<OnTriggeredWithCircle>(TriggeredActions);
    }

    private void TriggeredActions()
    {
        CloseCurrentCircle();
        OpenNextCircle();
    }


    private void CloseCurrentCircle()
    {
        _circles[_currentCircle].SetActive(false);
    }

    private void OpenNextCircle()
    {
        _currentCircle++;

        if (_circles.Count > _currentCircle)
            _circles[_currentCircle].SetActive(true);
        else
            Debug.Log("Last Circle Closed!");
    }
}