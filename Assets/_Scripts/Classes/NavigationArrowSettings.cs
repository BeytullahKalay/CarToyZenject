using UnityEngine;
using System;

[Serializable]
public class NavigationArrowSettings
{
    public Transform NavigationArrowPrefab;
    public ArrowOffset ArrowOffsetSettings;
    
    [Serializable]
    public class ArrowOffset
    {
        public Vector3 Offset;
    }
}