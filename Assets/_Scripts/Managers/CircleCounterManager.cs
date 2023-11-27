using System;
using Zenject;

public class CircleCounterManager : IInitializable, IDisposable
{
    private int _collectedCirclesAmount;

    private readonly SignalBus _onTriggeredWithCircle;
    private readonly SignalBus _updateCircleCounterUITextSignal;


    public CircleCounterManager(SignalBus onTriggeredWithCircle,SignalBus updateCircleCounterUITextSignal)
    {
        _collectedCirclesAmount = 0;
        _onTriggeredWithCircle = onTriggeredWithCircle;
        _updateCircleCounterUITextSignal = updateCircleCounterUITextSignal;
    }

    public void Initialize()
    {
        _onTriggeredWithCircle.Subscribe<OnTriggeredWithCircleSignal>(UpdateCircleAmount);
    }

    private void UpdateCircleAmount()
    {
        // increase collected circles
        _collectedCirclesAmount++;

        UpdateUI();
    }

    private void UpdateUI()
    {
        // fire a signal
        _updateCircleCounterUITextSignal.Fire(new UpdateCircleCounterUITextSignal(_collectedCirclesAmount));
    }

    public void Dispose()
    {
        _onTriggeredWithCircle.Unsubscribe<OnTriggeredWithCircleSignal>(UpdateCircleAmount);
    }
}