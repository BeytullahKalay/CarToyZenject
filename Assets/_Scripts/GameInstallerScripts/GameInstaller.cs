using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private CarTrailsSettings carTrailsSettings;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Player player;


    public override void InstallBindings()
    {
        // Create a new instance of Foo for every class that asks for an IFoo
        //Container.Bind<IFoo>().To<Foo>().AsTransient();


        Container.Bind<PlayerInput>().FromInstance(playerInput).AsSingle();
        Container.Bind<CarTrailsSettings>().FromInstance(carTrailsSettings).AsSingle();
        Container.Bind<Player>().FromInstance(player).AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerCarTrailController>().AsSingle();
    }
}

[System.Serializable]
public class CarTrailsSettings
{
    public List<TrailRenderer> Trails = new List<TrailRenderer>();
}