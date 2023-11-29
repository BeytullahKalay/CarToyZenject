using Zenject;

public class PlayerFlyingCar : FlyingCar
{
    [Inject]
    private void Constructor(CarSettings.PlayerFlyingCarSetting carSettings)
    {
        var settings = carSettings.settings;
        Multiplier = settings.MoveSettings.Multiplier;
        MoveForce = settings.MoveSettings.MoveForce;
        TurnTorque = settings.MoveSettings.TurnForce;
        FlowingDistance = settings.FlowingSettings.FlowingDistance;
        FlowingFrequency = settings.FlowingSettings.FlowingFrequency;
        StabilizationLerpForce = settings.StabillizationSettings.StabilizationLerpForce;
        WhatIsGround = settings.StabillizationSettings.WhatIsGround;
        MinCrashForce = settings.CrashSettings.MinCrashForce;
    }
}