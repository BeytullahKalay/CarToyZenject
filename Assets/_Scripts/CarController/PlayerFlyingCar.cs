
using Zenject;

public class PlayerFlyingCar : FlyingCar
{
    [Inject]
    private void Constructor(GameSettingsInstaller.CarSettings carSettings)
    {
        var playerCarSettings = carSettings.PlayerCarSettings;
        
        Multiplier = playerCarSettings.MoveSettings.Multiplier;
        MoveForce = playerCarSettings.MoveSettings.MoveForce;
        TurnTorque = playerCarSettings.MoveSettings.TurnForce;
        FlowingDistance = playerCarSettings.FlowingSettings.FlowingDistance;
        FlowingFrequency = playerCarSettings.FlowingSettings.FlowingFrequency;
    }
}
