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

    public void SetUpRoles(int mafia, int citizens)
    {
        mafiaAmountText.text = mafia.ToString();
        citizensAmountText.text = citizens.ToString();
    }

    protected override void SubscribeEvents()
    {
        continueButton.onClick.AddListener(OnShowRolesDistributionContinueButtonClick);
        backButton.onClick.AddListener(OnShowRolesDistributionBackButtonClick);
    }

    private void OnShowRolesDistributionContinueButtonClick()
    {
        InvokeContinueButtonClick();
    }

    private void OnShowRolesDistributionBackButtonClick()
    {
        InvokeBackButtonClick();
    }

    protected override void UnsubscribeEvents()
    {
        continueButton.onClick.RemoveListener(OnShowRolesDistributionContinueButtonClick);
        backButton.onClick.RemoveListener(OnShowRolesDistributionBackButtonClick);
    }
}
