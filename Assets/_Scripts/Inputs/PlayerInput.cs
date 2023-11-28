using UnityEngine;

public class PlayerInput : MonoBehaviour, ICarInput
{
    public float HorizontalInput { get; private set; }
    public float VerticalInput { get; private set; }
    public bool InputActive { get; set; } = true;

    public void DisableInput()
    {
        HorizontalInput = 0;
        VerticalInput = 0;
        InputActive = false;
    }

    private void GetInputs()
    {
        HorizontalInput = Input.GetAxis("Horizontal");
        VerticalInput = Input.GetAxis("Vertical");
    }

    public void Update()
    {
        if (!InputActive) return;
        GetInputs();
    }
}