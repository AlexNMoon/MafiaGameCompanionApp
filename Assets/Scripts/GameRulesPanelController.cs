using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameRulesPanelController : PanelController
{
    [SerializeField] private Button backButton;

    protected override void SubscribeEvents()
    {
        backButton.onClick.AddListener(OnGameRulesBackButtonClick);
    }
    
    private void OnGameRulesBackButtonClick()
    {
        InvokeOpenPreviousPanel();
    }

    protected override void UnsubscribeEvents()
    {
        backButton.onClick.RemoveListener(OnGameRulesBackButtonClick);
    }
}
