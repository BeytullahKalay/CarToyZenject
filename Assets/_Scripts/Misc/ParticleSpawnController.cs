using UnityEngine;

public class ParticleSpawnController : MonoBehaviour
{
    [SerializeField] private float maxDustSpawnHeight = 3;
    [SerializeField] private int emissionRateOverTime = 500;
    [SerializeField] private LayerMask whatIsGround;
    private ParticleSystem _dustParticle;


    private void Awake()
    {
        _dustParticle = GetComponent<ParticleSystem>();
    }

    void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out var hit, Mathf.Infinity,
                whatIsGround))
        {
            var distanceToGround = transform.position.y - hit.point.y;
            var emission = _dustParticle.emission;

            if (distanceToGround > maxDustSpawnHeight)
            {
                emission.rateOverTime = 0;
            }
            else
            {
                var dustAmount = Mathf.Abs(1 / distanceToGround);
                emission.rateOverTime = dustAmount * emissionRateOverTime;
            }
        }
    }
}