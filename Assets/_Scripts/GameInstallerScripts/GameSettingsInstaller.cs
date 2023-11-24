using System;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameSettingsInstaller", menuName = "Installers/GameSettingsInstaller")]
public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
{
    [SerializeField] private CarSettings carSettings;
    [SerializeField] private CircleTweenSettings circleTweenSettings;


    public override void InstallBindings()
    {
        Container.BindInstance(carSettings);
        Container.BindInstance(circleTweenSettings);
    }

    [Serializable]
    public class CarSettings
    {
        public EnemyMotorcycleSetting EnemyMotorcycle;
        public PlayerFlyingCarSetting PlayerFlyingCar;

        [Serializable]
        public class EnemyMotorcycleSetting
        {
            public Settings settings;
        }

        [Serializable]
        public class PlayerFlyingCarSetting
        {
            public Settings settings;
        }

        [Serializable]
        public class Settings
        {
            public Move MoveSettings;
            public Flowing FlowingSettings;

            [Serializable]
            public class Move
            {
                public float Multiplier;
                public float MoveForce;
                public float TurnForce;
            }

            [Serializable]
            public class Flowing
            {
                public float FlowingDistance;
                public float FlowingFrequency;
            }
        }
    }
}

[Serializable]
public class CircleTweenSettings
{
    public float ScalingDuration;
    public float ScalingAmount;
}