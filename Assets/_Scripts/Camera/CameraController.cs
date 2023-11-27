using UnityEngine;
using Zenject;

public class CameraController : MonoBehaviour
{
    //[SerializeField] private float cameraRotateSpeed = 10;

    private float _xRotation, _yRotation;

    private CameraSettings.Settings _cameraSettings;

    [Inject]
    private void Constructor(CameraSettings.Settings camSettings)
    {
        _cameraSettings = camSettings;
    }

    private void FixedUpdate()
    {
        CameraRotation();
    }

    private void CameraRotation()
    {
        // _xRotation += -Input.GetAxis("Mouse Y") * cameraRotateSpeed;
        // _yRotation += Input.GetAxis("Mouse X") * cameraRotateSpeed;
        // _xRotation = Mathf.Clamp(_xRotation, -30, 70);
        // var rotation = Quaternion.Euler(_xRotation, _yRotation, 0);
        // transform.rotation = rotation;
        
        
        _xRotation += -Input.GetAxis("Mouse Y") * _cameraSettings.CameraRotateSpeed;
        _yRotation += Input.GetAxis("Mouse X") * _cameraSettings.CameraRotateSpeed;
        var minMaxClampPos = _cameraSettings.ClampPositionsMinMax;
        _xRotation = Mathf.Clamp(_xRotation, minMaxClampPos.x, minMaxClampPos.y);
        var rotation = Quaternion.Euler(_xRotation, _yRotation, 0);
        transform.rotation = rotation;

    }
}