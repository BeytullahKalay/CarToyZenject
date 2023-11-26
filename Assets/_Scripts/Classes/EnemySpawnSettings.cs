using UnityEngine;
using System;

[Serializable]
public class EnemySpawnSettings
{
    public Settings Motorcycle;
    
    [Serializable]
    public class Settings
    {
        public Transform EnemyPrefab;
        public int SpawnAmount;
    }
}