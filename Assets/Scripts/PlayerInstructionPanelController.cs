using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInstructionPanelController : PanelController
{
    [SerializeField] private TMP_Text playerInstructionText;
    [SerializeField] private Button continueButton;
    
    private string _playerInstruction = "Please give the phone to the player №{0}";

    public void SetCurrentPlayerIndex(int player)
    {
        playerInstructionText.text = String.Format(_playerInstruction, player);
    }

    protected override void SubscribeEvents()
    {
        continueButton.onClick.AddListener(OnPlayerInstructionContinueButtonClick);
    }

    private void OnPlayerInstructionContinueButtonClick()
    {
        InvokeOpenNextPanel();
    }

    protected override void UnsubscribeEvents()
    {
        continueButton.onClick.RemoveListener(OnPlayerInstructionContinueButtonClick);
    }
}
