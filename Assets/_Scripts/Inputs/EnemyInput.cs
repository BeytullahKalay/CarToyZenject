using UnityEngine;
using Zenject;

public class EnemyInput : MonoBehaviour, ICarInput
{
    public float HorizontalInput { get;  set; }
    public float VerticalInput { get;  set; }
    public bool InputActive { get; set; } = true;

    private Player _player;

    [Inject]
    private void Constructor(Player player)
    {
        _player = player;
    }

    private void Start()
    {
        HorizontalInput = 0;
        VerticalInput = 0;
    }

    private void LateUpdate()
    {
        if (!InputActive) return;
        HandleInput();
    }

    private void HandleInput()
    {
        var delta = _player.Position - transform.position;
        delta.y = 0;
        delta.Normalize();

        var dotProduct = Vector3.Dot(delta, transform.right);
        HorizontalInput = dotProduct;
        VerticalInput = 1;
    }
    
    public void DisableInput()
    {
        InputActive = false;
        VerticalInput = 0;
        HorizontalInput = 0;
    }
}