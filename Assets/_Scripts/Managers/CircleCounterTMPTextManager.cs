using System;
using TMPro;
using Zenject;

public class CircleCounterTMPTextManager:IInitializable,IDisposable
{
    private readonly TMP_Text _counterUI;
    private readonly SignalBus _updateCircleCounterUITextSignal;
    
    
    public CircleCounterTMPTextManager(TMP_Text counterUI, SignalBus updateCircleCounterUITextSignal)
    {
        _counterUI = counterUI;
        _updateCircleCounterUITextSignal = updateCircleCounterUITextSignal;
    }

    public void Initialize()
    {
        _updateCircleCounterUITextSignal.Subscribe<UpdateCircleCounterUITextSignal>(UpdateUI);
        _updateCircleCounterUITextSignal.Fire(new UpdateCircleCounterUITextSignal(0));

    }

    private void UpdateUI(UpdateCircleCounterUITextSignal signal )
    {
        _counterUI.text = signal.CircleAmount.ToString();
    }

    public void Dispose()
    {
        _updateCircleCounterUITextSignal.Unsubscribe<UpdateCircleCounterUITextSignal>(UpdateUI);

    }
}