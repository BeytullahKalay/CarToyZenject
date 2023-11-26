using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameSettingsInstaller", menuName = "Installers/GameSettingsInstaller")]
public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
{
    [SerializeField] private SpawnCircleSettings spawnCircleSettings;
    [SerializeField] private CarSettings carSettings;
    [SerializeField] private CircleTweenSettings circleTweenSettings;
    [SerializeField] private BorderSettings borderSettings;
    [SerializeField] private NavigationArrowSettings navigationArrowSettings;
    [SerializeField] private EnemySpawnSettings enemySpawnSettings;


    public override void InstallBindings()
    {
        Container.BindInstance(carSettings);
        Container.BindInstance(circleTweenSettings);
        Container.BindInstance(spawnCircleSettings);
        Container.BindInstance(borderSettings);
        Container.BindInstance(navigationArrowSettings);
        Container.BindInstance(enemySpawnSettings);
    }
}