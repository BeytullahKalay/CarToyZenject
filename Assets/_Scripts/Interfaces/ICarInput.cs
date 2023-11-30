public interface ICarInput
{
    public float HorizontalInput { get; set; }
    public float VerticalInput { get; set; }
    public bool InputActive { get; set; }
    public void DisableInput();
}