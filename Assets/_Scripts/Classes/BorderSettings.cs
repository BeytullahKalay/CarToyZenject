using UnityEngine;
using System;

[Serializable]
public class BorderSettings
{
    public Borders Border;
    
    [Serializable]
    public class Borders
    {
        public Vector2 UpperRight;
        public Vector2 LowerLeft;
    }
}