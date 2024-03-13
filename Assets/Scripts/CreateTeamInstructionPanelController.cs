using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateTeamInstructionPanelController : PanelController
{
    [SerializeField] private Button backButton;
    [SerializeField] private Button continueButton;

    protected override void SubscribeEvents()
    {
        continueButton.onClick.AddListener(OnCreateTeamInstructionContinueButtonClick);
        backButton.onClick.AddListener(OnCreateTeamInstructionBackButtonClick);
    }

    private void OnCreateTeamInstructionContinueButtonClick()
    {
        InvokeOpenNextPanel();
        /*createTeamInstructionPanel.SetActive(false);
        StartRolesDistribution();*/
    }

    private void OnCreateTeamInstructionBackButtonClick()
    {
        InvokeOpenPreviousPanel();
        /*createTeamInstructionPanel.SetActive(false);
        showRolesDistributionPanel.SetActive(true);*/
    }

    protected override void UnsubscribeEvents()
    {
        continueButton.onClick.AddListener(OnCreateTeamInstructionContinueButtonClick);
        backButton.onClick.RemoveListener(OnCreateTeamInstructionBackButtonClick);
    }
}
