using Zenject;

public class PlayerCarTrailController : ITickable
{
    private PlayerInput _playerInput;

    private CarTrailsSettings _carTrailsSettings;

    public PlayerCarTrailController(PlayerInput playerInput,CarTrailsSettings carTrailsSettings)
    {
        _playerInput = playerInput;
        _carTrailsSettings = carTrailsSettings;
    }

    public void Tick()
    {
        HandleTrails();
    }

    private void HandleTrails()
    {
        if (_playerInput.VerticalInput != 0)
            OpenTrails();
        else
            CloseTrails();
    }

    private void OpenTrails()
    {
        foreach (var trail in _carTrailsSettings.Trails)
        {
            trail.emitting = true;
        }
    }

    private void CloseTrails()
    {
        foreach (var trail in _carTrailsSettings.Trails)
        {
            trail.emitting = false;
        }
    }
}