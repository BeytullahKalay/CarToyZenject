using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

public class UIController : MonoBehaviour
{
    private VisualElement _playButton;
    private VisualElement _quiteButton;
    private VisualElement _githubButton;
    private VisualElement _linkedButton;
    private VisualElement _discordButton;
    private Label _scoreText;

    private readonly List<VisualElement> _closingElementsList = new List<VisualElement>();

    private SignalBus _onGameStartSignal;
    private SignalBus _updateCircleCounterUITextSignal;


    [Inject]
    private void Constructor(SignalBus onGameStartSignal, SignalBus updateCircleCounterUITextSignal)
    {
        _onGameStartSignal = onGameStartSignal;
        _updateCircleCounterUITextSignal = updateCircleCounterUITextSignal;
    }

    private void OnEnable()
    {
        _updateCircleCounterUITextSignal.Subscribe<UpdateCircleCounterUITextSignal>(UpdateUI);
    }

    private void OnDisable()
    {
        _updateCircleCounterUITextSignal.Unsubscribe<UpdateCircleCounterUITextSignal>(UpdateUI);
    }

    private void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        // get visual elements
        _playButton = root.Q<VisualElement>("PlayButton");
        _quiteButton = root.Q<VisualElement>("QuiteButton");
        _githubButton = root.Q<VisualElement>("Github");
        _linkedButton = root.Q<VisualElement>("Linkedin");
        _discordButton = root.Q<VisualElement>("Discord");
        
        // get score label
        _scoreText = root.Q<Label>("Score");


        // bind events
        _playButton.RegisterCallback<ClickEvent>(OnPlayButtonClicked);
        _quiteButton.RegisterCallback<ClickEvent>(OnQuiteButtonClicked);
        _githubButton.RegisterCallback<ClickEvent>(OnGithubButtonClicked);
        _linkedButton.RegisterCallback<ClickEvent>(OnLinkedinButtonClicked);
        _discordButton.RegisterCallback<ClickEvent>(OnDiscordButtonClicked);

        // add elements to close list
        _closingElementsList.Add(_playButton);
        _closingElementsList.Add(_quiteButton);
        _closingElementsList.Add(_githubButton);
        _closingElementsList.Add(_linkedButton);
        _closingElementsList.Add(_discordButton);

        // get and disable score text for menu ui
        _scoreText = root.Q<Label>("Score");
        _scoreText.style.opacity = 0;
        _scoreText.text = 0.ToString();
    }

    private void OnDiscordButtonClicked(ClickEvent evt)
    {
        Application.OpenURL("https://discord.com/users/373648215599349760");
    }

    private void OnLinkedinButtonClicked(ClickEvent evt)
    {
        Application.OpenURL("https://www.linkedin.com/in/beytullah-kalay/");
    }

    private void OnGithubButtonClicked(ClickEvent evt)
    {
        Application.OpenURL("https://github.com/BeytullahKalay/MagaraJam");
    }

    private void OnQuiteButtonClicked(ClickEvent evt)
    {
        Application.Quit();
    }

    private void OnPlayButtonClicked(ClickEvent evt)
    {
        DisableElements(_closingElementsList);
        _onGameStartSignal.Fire(new OnGameStartSignal());
        _scoreText.style.opacity = 100;
    }

    private void DisableElements(List<VisualElement> disableElementsList)
    {
        foreach (var element in disableElementsList)
        {
            element.style.display = DisplayStyle.None;
        }
    }

    private void UpdateUI(UpdateCircleCounterUITextSignal signal)
    {
        _scoreText.text = signal.CircleAmount.ToString();
    }
}