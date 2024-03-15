using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndOfDistributionInstructionPanelController : PanelController
{
    [SerializeField] private Button continueButton;

    protected override void SubscribeEvents()
    {
        continueButton.onClick.AddListener(OnEndOfDistributionContinueButtonClick);
    }

    private void OnEndOfDistributionContinueButtonClick()
    {
        InvokeOpenNextPanel();
    }

    protected override void UnsubscribeEvents()
    {
        continueButton.onClick.AddListener(OnEndOfDistributionContinueButtonClick);
    }
}
