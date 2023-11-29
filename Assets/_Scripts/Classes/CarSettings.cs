using System;
using UnityEngine;

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
        public Stabillization StabillizationSettings;
        public Crash CrashSettings;

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
        
        [Serializable]
        public class Stabillization
        {
            public float StabilizationLerpForce;
            public LayerMask WhatIsGround;
        }
        
        [Serializable]
        public class Crash
        {
            public float MinCrashForce;
        }
    }
}