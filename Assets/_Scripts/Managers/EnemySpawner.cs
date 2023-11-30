using System;
using Zenject;

public class EnemySpawner : IInitializable, IDisposable
{
    private readonly EnemySpawnSettings.Settings _enemy;
    private readonly TerrainPositionCalculation _positionCalculation;
    private readonly EnemySpawFactory _enemySpawnFactory;
    private readonly SignalBus _onEnemyDeadSignal;

    public EnemySpawner(EnemySpawnSettings.Settings enemy, TerrainPositionCalculation positionCalculation,
        EnemySpawFactory enemySpawnFactory, SignalBus onEnemyDeadSignal)
    {
        _enemy = enemy;
        _positionCalculation = positionCalculation;
        _enemySpawnFactory = enemySpawnFactory;
        _onEnemyDeadSignal = onEnemyDeadSignal;
        
        SpawnEnemies();
    }


    public void Initialize()
    {
        _onEnemyDeadSignal.Subscribe<OnEnemyDeadSignal>(SpawnAnEnemy);
    }

    public void Dispose()
    {
        _onEnemyDeadSignal.Unsubscribe<OnEnemyDeadSignal>(SpawnAnEnemy);
    }


    private void SpawnEnemies()
    {
        for (int i = 0; i < _enemy.SpawnAmount; i++)
        {
            SpawnAnEnemy();
        }
    }

    private void SpawnAnEnemy()
    {
        // create enemy
        var enemy = _enemySpawnFactory.Create();

        // get random position on terrain
        var enemyPosition = _positionCalculation.GetRandomPositionOnTerrain();

        // increase y axis for a bit
        enemyPosition.y += 10;

        // set crated enemy position
        enemy.transform.position = _positionCalculation.GetRandomPositionOnTerrain();
    }
}