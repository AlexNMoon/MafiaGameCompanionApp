using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RolesDistributionPanelController : PanelController
{
    [SerializeField] private TMP_Text mafiaAmountText;
    [SerializeField] private TMP_Text citizensAmountText;
    [SerializeField] private Button backButton;
    [SerializeField] private Button continueButton;

    protected override void SubscribeEvents()
    {
        continueButton.onClick.AddListener(OnShowRolesDistributionContinueButtonClick);
        backButton.onClick.AddListener(OnShowRolesDistributionBackButtonClick);
    }

    private void OnShowRolesDistributionContinueButtonClick()
    {
        InvokeOpenNextPanel();
        /*showRolesDistributionPanel.SetActive(false);
        createTeamInstructionPanel.SetActive(true);*/
    }

    private void OnShowRolesDistributionBackButtonClick()
    {
        InvokeOpenPreviousPanel();
        /*showRolesDistributionPanel.SetActive(false);
        enterNumberOfPlayersPanel.SetActive(true);*/
    }

    protected override void UnsubscribeEvents()
    {
        continueButton.onClick.RemoveListener(OnShowRolesDistributionContinueButtonClick);
        backButton.onClick.RemoveListener(OnShowRolesDistributionBackButtonClick);
    }
}
