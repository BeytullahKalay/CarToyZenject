using System;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Zenject;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera uiCamera;
    [SerializeField] private CinemachineVirtualCamera gameCamera;


    private float _xRotation, _yRotation;

    private CameraSettings.Settings _cameraSettings;

    private GameState _gameState;

    private SignalBus _onGameStartSignal;


    [Inject]
    private void Constructor(CameraSettings.Settings camSettings, GameState gameState, SignalBus onGameStartSignal)
    {
        _cameraSettings = camSettings;
        _gameState = gameState;
        _onGameStartSignal = onGameStartSignal;
    }

    private void OnEnable()
    {
        _onGameStartSignal.Subscribe<OnGameStartSignal>(ChangeCamera);
    }

    private void OnDisable()
    {
        _onGameStartSignal.Unsubscribe<OnGameStartSignal>(ChangeCamera);
    }

    private void Start()
    {
        uiCamera.Priority = 20;

        gameCamera.Priority = 10;
    }


    private void FixedUpdate()
    {
        if (_gameState.State != GameStates.Run) return;
        CameraRotation();
    }

    private void CameraRotation()
    {
        _xRotation += -Input.GetAxis("Mouse Y") * _cameraSettings.CameraRotateSpeed;
        _yRotation += Input.GetAxis("Mouse X") * _cameraSettings.CameraRotateSpeed;
        var minMaxClampPos = _cameraSettings.ClampPositionsMinMax;
        _xRotation = Mathf.Clamp(_xRotation, minMaxClampPos.x, minMaxClampPos.y);
        var rotation = Quaternion.Euler(_xRotation, _yRotation, 0);
        transform.rotation = rotation;
    }

    private void ChangeCamera()
    {
        gameCamera.Priority = 20;
        uiCamera.Priority = 10;
    }
}