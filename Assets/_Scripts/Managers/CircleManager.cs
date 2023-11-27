using System;
using DG.Tweening;
using UnityEngine;
using Zenject;

public class CircleManager : IInitializable, IDisposable
{
    private readonly SignalBus _onTriggeredWithCircle;
    private readonly CircleTweenSettings _circleTweenSettings;
    private readonly SpawnCircleFactory _spawnCircleFactory;
    private readonly TerrainPositionCalculation _positionCalculation;
    private readonly CirclePositionShowerManager _circlePositionShowerManager;
    
    private int _currentCircle;
    private GameObject _circle;
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
    
    public void Initialize()
    {
        _onTriggeredWithCircle.Subscribe<OnTriggeredWithCircleSignal>(OnTriggeredActions);
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