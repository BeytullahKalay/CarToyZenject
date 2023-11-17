using System;
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

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
    }

    void FixedUpdate()
    {
        for (int i = 0; i < 4; i++)
        {
            ApplyForce(anchors[i], hits[i]);

            _rb.AddForce(Input.GetAxis("Vertical") * moveForce * transform.forward);
            _rb.AddTorque(Input.GetAxis("Horizontal") * turnTorque * transform.up);
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