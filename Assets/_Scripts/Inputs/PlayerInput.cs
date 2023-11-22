using UnityEngine;

public class PlayerInput : MonoBehaviour,ICarInput
{
    public float HorizontalInput { get; private set; }
    public float VerticalInput { get; private set; }

    private void GetInputs()
    {
        HorizontalInput = Input.GetAxis("Horizontal");
        VerticalInput = Input.GetAxis("Vertical");
    }
    
    public void Update()
    {
        GetInputs();
    }
}
