using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

public class EnemyMotorcycleFlyingCar : FlyingCar
{
    [SerializeField] private float minCollisionForce;
    [SerializeField] private List<GameObject> particleFx;
    

    private VehicleDestroyAnimationManager _vehicleDestroyAnimationManager;
    private PoolerManager _poolerManager;

    [Inject]
    private void Constructor(CarSettings.EnemyMotorcycleSetting carSettings,
        VehicleDestroyAnimationManager vehicleDestroyAnimationManager,PoolerManager poolerManager)
    {
        var settings = carSettings.settings;
        Multiplier = settings.MoveSettings.Multiplier;
        MoveForce = settings.MoveSettings.MoveForce;
        TurnTorque = settings.MoveSettings.TurnForce;
        FlowingDistance = settings.FlowingSettings.FlowingDistance;
        FlowingFrequency = settings.FlowingSettings.FlowingFrequency;
        _vehicleDestroyAnimationManager = vehicleDestroyAnimationManager;
        _poolerManager = poolerManager;
    }


    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Enemy")) return;
        
        var collisionForce = other.impulse / Time.fixedDeltaTime;

        if (!(collisionForce.magnitude >= minCollisionForce)) return;
        
        _vehicleDestroyAnimationManager.PlayExplosionAnimation(transform, 1, 2);
        CarInput.DisableInput();

        var pool = _poolerManager.GetPoolWithType(PooledObjectType.ExplosionDustParticleFx);
        var objectFromPool = pool.Get();
        objectFromPool.transform.position = transform.position;
        ReleaseParticleObject(5, pool, objectFromPool);
        
        foreach (var particle in particleFx)
        {
            Destroy(particle);
        }
    }

    private async void ReleaseParticleObject(float duration,ObjectPool<GameObject> pool, GameObject pooledObj)
    {
        await Task.Delay((int)(duration * 1000));
        pool.Release(pooledObj);
    }
}