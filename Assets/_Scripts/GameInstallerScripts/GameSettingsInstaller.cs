using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameSettingsInstaller", menuName = "Installers/GameSettingsInstaller")]
public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
{
    [SerializeField] private CarSettings carSettings;

    public override void InstallBindings()
    {
        Container.BindInstance(carSettings);
    }

    [System.Serializable]
    public class CarSettings
    {
        public Settings PlayerCarSettings;
        public Settings EnemyMotorcycleSettings;
    }

    [System.Serializable]
    public class Settings
    {
        public Move MoveSettings;
        public Flowing FlowingSettings;

        [System.Serializable]
        public class Move
        {
            public float Multiplier;
            public float MoveForce;
            public float TurnForce;
        }

        [System.Serializable]
        public class Flowing
        {
            public float FlowingDistance;
            public float FlowingFrequency;
        }
    }
}