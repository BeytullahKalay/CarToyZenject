using UnityEngine;

public abstract class FlyingCar : MonoBehaviour
{
    private Rigidbody _rb;

    protected float Multiplier;
    protected float FlowingDistance;
    protected float MoveForce, TurnTorque;
    protected float FlowingFrequency = 3;
    
    [SerializeField] Transform[] anchors = new Transform[4];
    RaycastHit[] hits = new RaycastHit[4];


    private ICarInput _carInput;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _carInput = GetComponent<ICarInput>();
    }



    void FixedUpdate()
    {
        for (int i = 0; i < 4; i++)
        {
            ApplyForce(anchors[i], hits[i]);
            _rb.AddForce(_carInput.VerticalInput * MoveForce * transform.forward);
            _rb.AddTorque(_carInput.HorizontalInput * TurnTorque * transform.up);
        }
    }

    private void ApplyForce(Transform anchor, RaycastHit hit)
    {
        if (Physics.Raycast(anchor.position, -anchor.up * 3, out hit))
        {
            var movingFlowDistance = FlowingDistance * Mathf.Sin(Time.time * FlowingFrequency);
            float force = Mathf.Abs(1 / (hit.point.y - anchor.position.y));
            _rb.AddForceAtPosition(transform.up * (force * (Multiplier + movingFlowDistance)), anchor.position,
                ForceMode.Acceleration);
        }
    }
}