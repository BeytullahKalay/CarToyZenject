using Zenject;

public class EnemySpawner : IInitializable
{
    private readonly EnemySpawnSettings.Settings _enemy;
    private readonly TerrainPositionCalculation _positionCalculation;
    private readonly EnemySpawFactory _enemySpawnFactory;

    public EnemySpawner(EnemySpawnSettings.Settings enemy, TerrainPositionCalculation positionCalculation,
        EnemySpawFactory enemySpawnFactory)
    {
        _enemy = enemy;
        _positionCalculation = positionCalculation;
        _enemySpawnFactory = enemySpawnFactory;
    }


    public void Initialize()
    {
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < _enemy.SpawnAmount; i++)
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
}