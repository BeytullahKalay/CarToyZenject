using Zenject;

public class EnemyMotorcycleFlyingCar : FlyingCar
{
    [Inject]
    private void Constructor(CarSettings.EnemyMotorcycleSetting carSettings)
    {
        var settings = carSettings.settings;
        Multiplier = settings.MoveSettings.Multiplier;
        MoveForce = settings.MoveSettings.MoveForce;
        TurnTorque = settings.MoveSettings.TurnForce;
        FlowingDistance = settings.FlowingSettings.FlowingDistance;
        FlowingFrequency = settings.FlowingSettings.FlowingFrequency;
    }
}