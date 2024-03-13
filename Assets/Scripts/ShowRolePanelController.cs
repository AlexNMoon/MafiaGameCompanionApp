using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowRolePanelController : PanelController
{
    [SerializeField] private Image roleImage;
    [SerializeField] private TMP_Text roleText;
    [SerializeField] private Button continueButton;
    
    private string _roleString = "You are {0}";

    protected override void SubscribeEvents()
    {
        continueButton.onClick.AddListener(OnShowRoleContinueButtonClick);
    }

    private void OnShowRoleContinueButtonClick()
    {
        InvokeOpenNextPanel();
        /*showRolePanel.SetActive(false);
        _currentPlayerAssigment++;

        if (_currentPlayerAssigment <= _numberOfPlayers)
        {
            ShowPlayerInstructionPanel();
        }
        else
        {
            endOfDistributionInstructionPanel.SetActive(true);
        }*/
    }

    protected override void UnsubscribeEvents()
    {
        continueButton.onClick.RemoveListener(OnShowRoleContinueButtonClick);
    }
}
