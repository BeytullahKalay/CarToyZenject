using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private CarTrailsSettings carTrailsSettings;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Player player;
    [SerializeField] private CircleManagerSettings circleManagerSettings;


    [Inject] private GameSettingsInstaller.CarSettings _carSettings;


    public override void InstallBindings()
    {
        // Create a new instance of Foo for every class that asks for an IFoo
        //Container.Bind<IFoo>().To<Foo>().AsTransient();


        InstallObjectsAndScripts();

        InstallManagers();

        InstallSignals();

        Container.BindInstance(_carSettings.PlayerFlyingCar);
        Container.BindInstance(_carSettings.EnemyMotorcycle);
    }

    private void InstallManagers()
    {
        Container.BindInterfacesAndSelfTo<CircleManager>().AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerCarTrailManager>().AsSingle();
    }

    private void InstallObjectsAndScripts()
    {
        Container.Bind<PlayerInput>().FromInstance(playerInput).AsSingle();
        Container.Bind<Player>().FromInstance(player).AsSingle();
        Container.Bind<CircleManagerSettings>().FromInstance(circleManagerSettings);
        Container.Bind<CarTrailsSettings>().FromInstance(carTrailsSettings).AsSingle();
    }

    private void InstallSignals()
    {
        SignalBusInstaller.Install(Container);

        Container.DeclareSignal<OnTriggeredWithCircle>();
    }
}

[System.Serializable]
public class CarTrailsSettings
{
    public List<TrailRenderer> Trails = new List<TrailRenderer>();
}

[System.Serializable]
public class CircleManagerSettings
{
    public List<GameObject> Circles = new List<GameObject>();
}