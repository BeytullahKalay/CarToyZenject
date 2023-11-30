using TMPro;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private CarTrailsSettings carTrailsSettings;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Player player;
    [SerializeField] private Terrain terrain;
    [SerializeField] private TMP_Text counterText;
    [SerializeField] private PoolerManager poolerManager;


    private CarSettings _carSettings;
    private SpawnCircleSettings _spawnCircleSettings;
    private BorderSettings _borderSettings;
    private NavigationArrowSettings _navigationArrowSettings;
    private EnemySpawnSettings _enemySpawnSettings;
    private CameraSettings _cameraSettings;

    [Inject]
    private void Constructor(CarSettings carSettings, SpawnCircleSettings spawnCircleSettings,
        BorderSettings borderSettings, NavigationArrowSettings navigationArrowSettings,
        EnemySpawnSettings enemySpawnSettings, CameraSettings cameraSettings)
    {
        _carSettings = carSettings;
        _spawnCircleSettings = spawnCircleSettings;
        _borderSettings = borderSettings;
        _navigationArrowSettings = navigationArrowSettings;
        _enemySpawnSettings = enemySpawnSettings;
        _cameraSettings = cameraSettings;
    }


    public override void InstallBindings()
    {
        // Create a new instance of Foo for every class that asks for an IFoo
        //Container.Bind<IFoo>().To<Foo>().AsTransient();
        
        Container.Bind<ICarInput>().To<EnemyInput>().FromComponentInHierarchy().AsTransient()
            .WhenInjectedInto<MotorcycleEnemyHealthSystem>();
        Container.Bind<ICarInput>().To<PlayerInput>().FromComponentInHierarchy().AsTransient()
            .WhenInjectedInto<PlayerFlyingCarHealthSystem>();
        

        InstallSceneScripts();

        InstallManagers();

        InstallSignals();

        InstallObjects();

        InstallEnemy();

        InstallMisc();

        InstallFactories();
    }


    private void InstallSceneScripts()
    {
        Container.Bind<PlayerInput>().FromInstance(playerInput).AsSingle();
        Container.Bind<Player>().FromInstance(player).AsSingle();
        Container.Bind<CarTrailsSettings>().FromInstance(carTrailsSettings).AsSingle();
        Container.Bind<Terrain>().FromInstance(terrain).AsSingle();
        Container.Bind<TMP_Text>().FromInstance(counterText).AsSingle();
        Container.Bind<PoolerManager>().FromInstance(poolerManager).AsSingle();
    }

    private void InstallManagers()
    {
        Container.BindInterfacesAndSelfTo<CircleManager>().AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerCarTrailManager>().AsSingle();
        Container.BindInterfacesAndSelfTo<CirclePositionShowerManager>().AsSingle();
        Container.BindInterfacesAndSelfTo<EnemySpawner>().AsSingle();
        Container.BindInterfacesAndSelfTo<TimerManager>().AsSingle();
        Container.BindInterfacesAndSelfTo<CircleCounterManager>().AsSingle();
        Container.BindInterfacesAndSelfTo<CircleCounterTMPTextManager>().AsSingle();
        Container.BindInterfacesAndSelfTo<GameState>().AsSingle();
    }

    private void InstallObjects()
    {
        Container.BindInstance(_carSettings.PlayerFlyingCar);
        Container.BindInstance(_carSettings.EnemyMotorcycle);
    }

    private void InstallSignals()
    {
        SignalBusInstaller.Install(Container);
        Container.DeclareSignal<OnTriggeredWithCircleSignal>();
        Container.DeclareSignal<UpdateCircleCounterUITextSignal>();
        Container.DeclareSignal<OnEnemyDeadSignal>();
        Container.DeclareSignal<OnGameOverSignal>();
        Container.DeclareSignal<OnGameStartSignal>();
    }

    private void InstallMisc()
    {
        Container.Bind<TerrainPositionCalculation>().AsSingle();
        Container.Bind<VehicleDestroyAnimationManager>().AsTransient();
        Container.BindInstance(_borderSettings.Border);
        Container.BindInstance(_navigationArrowSettings.ArrowOffsetSettings);
        Container.BindInstance(_cameraSettings.Setting);
    }

    private void InstallFactories()
    {
        Container.BindFactory<Transform, SpawnCircleFactory>()
            .FromComponentInNewPrefab(_spawnCircleSettings.SpawnCircle);

        Container.BindFactory<Transform, NavigationArrowFactory>()
            .FromComponentInNewPrefab(_navigationArrowSettings.NavigationArrowPrefab);

        // It doesn't feel right
        Container.BindFactory<Transform, EnemySpawFactory>()
            .FromComponentInNewPrefab(_enemySpawnSettings.Motorcycle.EnemyPrefab);
    }

    private void InstallEnemy()
    {
        // It doesn't feel right
        Container.BindInstance(_enemySpawnSettings.Motorcycle);
    }
}