using System;
using UnityEngine;
using Zenject;

public abstract class FlyingCar : MonoBehaviour
{
    [SerializeField] Transform[] anchors = new Transform[4];


    protected float Multiplier;
    protected float FlowingDistance;
    protected float MoveForce, TurnTorque;
    protected float FlowingFrequency;

    protected float StabilizationLerpForce;
    protected LayerMask WhatIsGround;

    protected float MinCrashForce;


    public bool Fly { get; set; } = true;


    private Rigidbody _rb;
    private RaycastHit[] hits = new RaycastHit[4];
    private ICarInput _carInput;

    private GameState _gameState;

    private SignalBus _onGameStart;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _carInput = GetComponent<ICarInput>();
    }

    [Inject]
    private void Constructor(GameState gameState, SignalBus onGameStart)
    {
        _gameState = gameState;
        _onGameStart = onGameStart;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            print("Fired!");
            _onGameStart.Fire<OnGameStartSignal>();
        }
    }


    private void FixedUpdate()
    {
        if (!Fly) return;

        for (int i = 0; i < 4; i++)
        {
            ApplyForce(anchors[i], hits[i]);

            if (_gameState.State != GameStates.Run) SetInputsTo0();


            _rb.AddForce(_carInput.VerticalInput * MoveForce * transform.forward);
            _rb.AddTorque(_carInput.HorizontalInput * TurnTorque * transform.up);
        }
    }

    private void SetInputsTo0()
    {
        _carInput.VerticalInput = 0;
        _carInput.HorizontalInput = 0;
    }

    private void ApplyForce(Transform anchor, RaycastHit hit)
    {
        if (Physics.Raycast(anchor.position, -anchor.up * 3, out hit, WhatIsGround))
        {
            var movingFlowDistance = FlowingDistance * Mathf.Sin(Time.time * FlowingFrequency);
            float force = Mathf.Abs(1 / (hit.point.y - anchor.position.y));
            _rb.AddForceAtPosition(transform.up * (force * (Multiplier + movingFlowDistance)), anchor.position,
                ForceMode.Acceleration);
        }
        else
        {
            var tRot = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, 0));
            transform.rotation =
                Quaternion.Slerp(transform.rotation, tRot, StabilizationLerpForce * Time.fixedDeltaTime);
        }
    }
}