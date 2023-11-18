using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float cameraRotateSpeed = 10;

    private float _xRotation, _yRotation;

    private void FixedUpdate()
    {
        CameraRotation();
    }

    private void CameraRotation()
    {
        _xRotation += -Input.GetAxis("Mouse Y") * cameraRotateSpeed;
        _yRotation += Input.GetAxis("Mouse X") * cameraRotateSpeed;
        _xRotation = Mathf.Clamp(_xRotation, -30, 70);
        var rotation = Quaternion.Euler(_xRotation, _yRotation, 0);
        transform.rotation = rotation;
    }
}