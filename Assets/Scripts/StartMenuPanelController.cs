using System;
using UnityEngine;
using UnityEngine.UI;

public class StartMenuPanelController : PanelController
{
    public event Action OpenGameRules;
    public event Action StartGame;
    
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button showRulesButton;

    protected override void SubscribeEvents()
    {
        newGameButton.onClick.AddListener(OnStartGameButtonClick);
        showRulesButton.onClick.AddListener(OnShowRulesButtonClick);
    }

    private void OnStartGameButtonClick()
    {
        StartGame?.Invoke();
    }

    private void OnShowRulesButtonClick()
    {
        OpenGameRules?.Invoke();
    }

    protected override void UnsubscribeEvents()
    {
        newGameButton.onClick.RemoveListener(OnStartGameButtonClick);
        showRulesButton.onClick.RemoveListener(OnShowRulesButtonClick);
    }
}
