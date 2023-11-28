using UnityEngine;

public abstract class FlyingCar : MonoBehaviour
{
    [SerializeField] private float stabilizationLerpSpeed = 3;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] Transform[] anchors = new Transform[4];
    
    private Rigidbody _rb;
    protected float Multiplier;
    protected float FlowingDistance;
    protected float MoveForce, TurnTorque;
    protected float FlowingFrequency = 3;

    
    private RaycastHit[] hits = new RaycastHit[4];


    protected ICarInput CarInput;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        CarInput = GetComponent<ICarInput>();
    }


    void FixedUpdate()
    {
        for (int i = 0; i < 4; i++)
        {
            ApplyForce(anchors[i], hits[i]);
            _rb.AddForce(CarInput.VerticalInput * MoveForce * transform.forward);
            _rb.AddTorque(CarInput.HorizontalInput * TurnTorque * transform.up);
        }
    }

    private void ApplyForce(Transform anchor, RaycastHit hit)
    {
        if (Physics.Raycast(anchor.position, -anchor.up * 3, out hit,whatIsGround))
        {
            var movingFlowDistance = FlowingDistance * Mathf.Sin(Time.time * FlowingFrequency);
            float force = Mathf.Abs(1 / (hit.point.y - anchor.position.y));
            _rb.AddForceAtPosition(transform.up * (force * (Multiplier + movingFlowDistance)), anchor.position,
                ForceMode.Acceleration);
        }
        else
        {
            var tRot = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, 0));
            transform.rotation = Quaternion.Slerp(transform.rotation, tRot, stabilizationLerpSpeed * Time.fixedDeltaTime);
        }
    }
}