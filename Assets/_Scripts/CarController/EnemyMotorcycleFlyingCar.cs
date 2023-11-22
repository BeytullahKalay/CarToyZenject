using Zenject;

public class EnemyMotorcycleFlyingCar : FlyingCar
{
    [Inject]
    private void Constructor(GameSettingsInstaller.CarSettings carSettings)
    {
        var enemyMotorcycleSettings = carSettings.EnemyMotorcycleSettings;
        
        Multiplier = enemyMotorcycleSettings.MoveSettings.Multiplier;
        MoveForce = enemyMotorcycleSettings.MoveSettings.MoveForce;
        TurnTorque = enemyMotorcycleSettings.MoveSettings.TurnForce;
        FlowingDistance = enemyMotorcycleSettings.FlowingSettings.FlowingDistance;
        FlowingFrequency = enemyMotorcycleSettings.FlowingSettings.FlowingFrequency;
    }
}
