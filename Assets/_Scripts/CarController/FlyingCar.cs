using UnityEngine;

public class FlyingCar : MonoBehaviour
{
    private Rigidbody _rb;

    [SerializeField] private float multiplier;
    [SerializeField] private float flowingDistance;
    [SerializeField] float moveForce, turnTorque;
    [SerializeField] private float flowingFrequency = 3;


    [SerializeField] Transform[] anchors = new Transform[4];
    RaycastHit[] hits = new RaycastHit[4];

    private float _horizontalInput, _verticalInput;

    private CarTrailController _carTrailController;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _carTrailController = GetComponentInChildren<CarTrailController>();
    }

    private void Update()
    {
        GetInputs();

        HandleTrails();
    }

    private void HandleTrails()
    {
        if (_verticalInput != 0)
        {
            _carTrailController.OpenTrails();
        }
        else
        {
            _carTrailController.CloseTrails();
        }
    }

    private void GetInputs()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        for (int i = 0; i < 4; i++)
        {
            ApplyForce(anchors[i], hits[i]);
            _rb.AddForce(_verticalInput * moveForce * transform.forward);
            _rb.AddTorque(_horizontalInput * turnTorque * transform.up);
        }
    }

    private void ApplyForce(Transform anchor, RaycastHit hit)
    {
        if (Physics.Raycast(anchor.position, -anchor.up * 3, out hit))
        {
            var movingFlowDistance = flowingDistance * Mathf.Sin(Time.time * flowingFrequency);
            float force = Mathf.Abs(1 / (hit.point.y - anchor.position.y));
            _rb.AddForceAtPosition(transform.up * (force * (multiplier + movingFlowDistance)), anchor.position,
                ForceMode.Acceleration);
        }
    }
}