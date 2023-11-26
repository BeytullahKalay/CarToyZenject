using System;
using System.Diagnostics;
using DG.Tweening;
using UnityEngine;
using Zenject;

public class CircleManager : IInitializable, IDisposable
{
    private int _currentCircle;
    private SignalBus _onTriggeredWithCircle;
    private CircleTweenSettings _circleTweenSettings;
    private SpawnCircleFactory _spawnCircleFactory;
    private GameObject _circle;
    private TerrainPositionCalculation _positionCalculation;
    private CirclePositionShowerManager _circlePositionShowerManager;

    public void Initialize()
    {
        _onTriggeredWithCircle.Subscribe<OnTriggeredWithCircleSignal>(OnTriggeredActions);
    }

    public CircleManager(SignalBus onTriggeredWithCircle
        , CircleTweenSettings circleTweenSettings, SpawnCircleFactory spawnCircleFactory,
        TerrainPositionCalculation positionCalculation,CirclePositionShowerManager circlePositionShowerManager)
    {
        _circleTweenSettings = circleTweenSettings;
        _onTriggeredWithCircle = onTriggeredWithCircle;
        _spawnCircleFactory = spawnCircleFactory;
        _positionCalculation = positionCalculation;
        _circlePositionShowerManager = circlePositionShowerManager;

        InitializeCircle();
    }

    private void InitializeCircle()
    {
        // Create a circle from factory
        _circle = _spawnCircleFactory.Create().gameObject;

        // assign to navigation arrow shower target
        _circlePositionShowerManager.NavigateObject = _circle.transform;
        
        SetPositionOfCircle();

        // Use tween to make animation
        _circle.transform.DOScale(Vector3.one * _circleTweenSettings.ScalingAmount,
            _circleTweenSettings.ScalingDuration).SetRelative().SetLoops(-1, LoopType.Yoyo);
    }

    private void SetPositionOfCircle()
    {
        _circle.transform.position = _positionCalculation.GetRandomPositionOnTerrain();
    }

    private void OnTriggeredActions()
    {
        SetPositionOfCircle();
    }

    public void Dispose()
    {
        _onTriggeredWithCircle.Unsubscribe<OnTriggeredWithCircleSignal>(OnTriggeredActions);
    }
}