using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnterNumberOfPlayersPanelController : PanelController
{
    [SerializeField] private TMP_InputField numberOfPlayersInput;
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button backButton;
    [SerializeField] private GameObject errorText;

    public int GetNumberOfPlayer()
    {
        return Convert.ToInt32(numberOfPlayersInput.text);
    }

    public void ShowError()
    {
        errorText.SetActive(true);
    }

    public override void OpenPanel()
    {
        numberOfPlayersInput.text = "";
        errorText.SetActive(false);
        base.OpenPanel();
    }

    protected override void SubscribeEvents()
    {
        confirmButton.onClick.AddListener(OnConfirmNumberOfPlayersButtonClick);
        backButton.onClick.AddListener(OnEnterNumberOfPlayersBackButtonClick);
    }

    private void OnConfirmNumberOfPlayersButtonClick()
    {
        if (numberOfPlayersInput.text == "")
        {
            errorText.SetActive(true);
            return;
        }
        
        InvokeContinueButtonClick();
    }

    private void OnEnterNumberOfPlayersBackButtonClick()
    {
        InvokeBackButtonClick();
    }

    protected override void UnsubscribeEvents()
    {
        confirmButton.onClick.RemoveListener(OnConfirmNumberOfPlayersButtonClick);
        backButton.onClick.RemoveListener(OnEnterNumberOfPlayersBackButtonClick);
    }
}
