using UnityEngine;
using Zenject;

public class DetectPlayer : MonoBehaviour
{
    [SerializeField] private float radius = 2;
    [SerializeField] private LayerMask whatIsPlayer;

    private SignalBus _onTriggeredWithCircle;

    [Inject]
    private void Constrcutor(SignalBus onTriggeredWithCircle)
    {
        _onTriggeredWithCircle = onTriggeredWithCircle;
    }

    private void Update()
    {
        if (Physics.OverlapSphere(transform.position,radius,whatIsPlayer).Length > 0)
        {
            _onTriggeredWithCircle.Fire(new OnTriggeredWithCircleSignal());
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
