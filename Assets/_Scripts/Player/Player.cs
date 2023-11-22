using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector3 Position
    {
        get => transform.position;
        set => transform.position = value;
    }
}
