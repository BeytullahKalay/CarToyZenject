using System;
using UnityEngine;
using Zenject;

public class VeryBasicHealthSystem : MonoBehaviour
{
    protected Action OnDeadActions;
    protected bool IsAlive = true;

    protected float MinCollisionForce;
    protected VehicleDestroyAnimationManager VehicleDestroyAnimationManager;
    protected PoolerManager PoolerManager;


    [Inject]
    private void Constructor(VehicleDestroyAnimationManager vehicleDestroyAnimationManager,
        PoolerManager poolerManager)
    {
        VehicleDestroyAnimationManager = vehicleDestroyAnimationManager;
        PoolerManager = poolerManager;
    }


    protected virtual void OnEnable()
    {
        OnDeadActions += OnDead;
    }

    protected virtual void OnDisable()
    {
        OnDeadActions -= OnDead;
    }

    private void OnDead()
    {
        IsAlive = false;
        //_flyingCar.Fly = false;
    }
}