using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

public class MotorcycleEnemyHealthSystem : VeryBasicHealthSystem
{
    [SerializeField] private List<GameObject> objectToCloseOnDead;


    private ICarInput _carInput;
    private FlyingCar _enemyMotorcycleFlyingCar;
    private SignalBus _onEnemyDeadSignal;
    private GameState _gameState;

    private void Awake()
    {
        _enemyMotorcycleFlyingCar = GetComponent<FlyingCar>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        OnDeadActions += OnCrash;
        OnDeadActions += DisableObjectOnDead;
        OnDeadActions += StopFlying;
        OnDeadActions += FireEnemyDeadSignal;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        OnDeadActions -= OnCrash;
        OnDeadActions -= DisableObjectOnDead;
        OnDeadActions -= StopFlying;
        OnDeadActions -= FireEnemyDeadSignal;
    }

    [Inject]
    private void Constructor(CarSettings.EnemyMotorcycleSetting playerFlyingCarSetting, ICarInput carInput,
        SignalBus onEnemyDeadSignal,GameState gameState)
    {
        MinCollisionForce = playerFlyingCarSetting.settings.CrashSettings.MinCrashForce;
        _carInput = carInput;
        _onEnemyDeadSignal = onEnemyDeadSignal;
        _gameState = gameState;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (_gameState.State != GameStates.Run) return;

        if (!IsAlive) return;

        if (!other.gameObject.CompareTag("Enemy") && !other.gameObject.CompareTag("Player")) return;

        var collisionForce = other.impulse / Time.fixedDeltaTime;

        if (collisionForce.magnitude < MinCollisionForce) return;
        
        OnDeadActions?.Invoke();
    }

    private void OnCrash()
    {
        VehicleDestroyAnimationManager.PlayExplosionAnimation(transform, 1, 2);

        _carInput.DisableInput();

        var pool = PoolerManager.GetPoolWithType(PooledObjectType.ExplosionDustParticleFx);
        var objectFromPool = pool.Get();
        objectFromPool.transform.position = transform.position;
        ReleaseParticleObject(5, pool, objectFromPool);
    }

    private async void ReleaseParticleObject(float duration, ObjectPool<GameObject> pool, GameObject pooledObj)
    {
        await Task.Delay((int)(duration * 1000));
        pool.Release(pooledObj);
    }

    private void StopFlying()
    {
        _enemyMotorcycleFlyingCar.Fly = false;
    }

    private void DisableObjectOnDead()
    {
        foreach (var obj in objectToCloseOnDead)
        {
            obj.SetActive(false);
        }
    }

    private void FireEnemyDeadSignal()
    {
        _onEnemyDeadSignal.Fire(new OnEnemyDeadSignal());
    }
}