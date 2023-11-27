using System;
using UnityEngine;

[Serializable]
public class CameraSettings
{
    public Settings Setting;
    
    [Serializable]
    public class Settings
    {
        public Vector2 ClampPositionsMinMax = new Vector2(-30, 70);
        public float CameraRotateSpeed = 5;
    }
}