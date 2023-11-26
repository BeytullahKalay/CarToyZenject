using UnityEngine;
using Zenject;

[RequireComponent(typeof(Collider))]
public class PlayerCircleDetection : MonoBehaviour
{
    private SignalBus _onTriggeredWithCircle;

    [Inject]
    private void Init(SignalBus onTriggeredWithCircle)
    {
        _onTriggeredWithCircle = onTriggeredWithCircle;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Circle"))
        {
            _onTriggeredWithCircle.Fire(new OnTriggeredWithCircleSignal());
        }
    }
}