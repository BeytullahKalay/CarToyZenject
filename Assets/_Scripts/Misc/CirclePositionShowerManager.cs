using UnityEngine;
using Zenject;

public class CirclePositionShowerManager : ITickable
{
    private Player _player;
    private NavigationArrowSettings.ArrowOffset _arrowOffset;
    private NavigationArrowFactory _navigationArrowFactory;

    private Transform _navigationArrowTransform;

    public Transform NavigateObject { private get; set; }

    public CirclePositionShowerManager(Player player, NavigationArrowSettings.ArrowOffset offset,
        NavigationArrowFactory navigationArrowFactory)
    {
        _player = player;
        _arrowOffset = offset;
        _navigationArrowFactory = navigationArrowFactory;

        SetNavigationArrow();
    }

    private void SetNavigationArrow()
    {
        _navigationArrowTransform = _navigationArrowFactory.Create().transform;
    }

    public void Tick()
    {
        HandlePosition();
        HandleRotation();
    }

    private void HandlePosition()
    {
        _navigationArrowTransform.position = _player.Position + _arrowOffset.Offset;
    }

    private void HandleRotation()
    {
        var dir = _player.Position - NavigateObject.position;
        dir.y = 0;
        dir = dir.normalized;
        var rotation = Quaternion.LookRotation(dir);

        _navigationArrowTransform.rotation =
            Quaternion.Slerp(_navigationArrowTransform.rotation, rotation, 5 * Time.deltaTime);
    }
}