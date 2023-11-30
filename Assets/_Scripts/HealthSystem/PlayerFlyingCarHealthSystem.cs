using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

public class PlayerFlyingCarHealthSystem : VeryBasicHealthSystem
{
    private ICarInput _carInput;
    private FlyingCar _flyingCar;
    private GameState _gameState;
    private SignalBus _onGameOverSignal;

    private void Awake()
    {
        _flyingCar = GetComponent<FlyingCar>(); // <------ THIS IS REAL BAD
    }
    
    [Inject]
    private void Constructor(CarSettings.PlayerFlyingCarSetting playerFlyingCarSetting, ICarInput carInput,
        GameState gameState,SignalBus onGameOverSignal)
    {
        MinCollisionForce = playerFlyingCarSetting.settings.CrashSettings.MinCrashForce;
        _carInput = carInput;
        _gameState = gameState;
        _onGameOverSignal = onGameOverSignal;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        OnDeadActions += OnCrash;
        OnDeadActions += StopFlying;
        OnDeadActions += FireGameOverSignal;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        OnDeadActions -= StopFlying;
        OnDeadActions -= OnCrash;
        OnDeadActions -= FireGameOverSignal;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!IsAlive) return;

        if (!other.gameObject.CompareTag("Enemy")) return;

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

    private void StopFlying()
    {
        _flyingCar.Fly = false;
    }

    private async void ReleaseParticleObject(float duration, ObjectPool<GameObject> pool, GameObject pooledObj)
    {
        await Task.Delay((int)(duration * 1000));
        pool.Release(pooledObj);
    }

    private void FireGameOverSignal()
    {
        _onGameOverSignal.Fire(new OnGameOverSignal());
    }
}