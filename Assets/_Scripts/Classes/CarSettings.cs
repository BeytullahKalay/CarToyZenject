using System;

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