public interface ICarInput
{
    public float HorizontalInput { get; }
    public float VerticalInput { get; }
    public bool InputActive { get; set; }
    public void DisableInput();
}