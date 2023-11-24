using System;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameSettingsInstaller", menuName = "Installers/GameSettingsInstaller")]
public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
{
    [SerializeField] private CarSettings carSettings;
    [SerializeField] private CircleSettings circleSettings;

    public override void InstallBindings()
    {
        Container.BindInstance(carSettings);
        Container.BindInstance(circleSettings);
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


    [Serializable]
    public class CircleSettings
    {
        [Serializable]
        public class Settings
        {
            public float ScalingSpeed;
            public float ScalingMultiplier;
        }
    }
}