using System;
using Zenject;


[Serializable]
public class GameState : IInitializable, IDisposable
{
    public GameStates State { get; private set; }

    private SignalBus _onGameOverSignal;
    private SignalBus _onGameStartSignal;

    public GameState(SignalBus onGameOverSignal, SignalBus onGameStartSignal)
    {
        State = GameStates.Stop;

        _onGameOverSignal = onGameOverSignal;
        _onGameStartSignal = onGameStartSignal;
    }

    public void Initialize()
    {
        _onGameStartSignal.Subscribe<OnGameStartSignal>(SetStateToRun);
        _onGameOverSignal.Subscribe<OnGameOverSignal>(SetStateToGameOver);
    }

    public void Dispose()
    {
        _onGameStartSignal.Unsubscribe<OnGameStartSignal>(SetStateToRun);
        _onGameOverSignal.Unsubscribe<OnGameOverSignal>(SetStateToGameOver);
    }

    private void SetStateToGameOver()
    {
        State = GameStates.GameOver;
    }

    private void SetStateToRun()
    {
        State = GameStates.Run;
    }
}


[Serializable]
public enum GameStates
{
    Stop,
    Run,
    GameOver
}