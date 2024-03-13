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

    protected override void SubscribeEvents()
    {
        confirmButton.onClick.AddListener(OnConfirmNumberOfPlayersButtonClick);
        backButton.onClick.AddListener(OnEnterNumberOfPlayersBackButtonClick);
    }

    private void OnConfirmNumberOfPlayersButtonClick()
    {
        InvokeOpenNextPanel();
        /*if (numberOfPlayersTMPInputField.text == "")
        {
            numberOfPlayersErrorText.SetActive(true);
            return;
        }
        
        _numberOfPlayers =  Convert.ToInt32(numberOfPlayersTMPInputField.text);
            
        if (_numberOfPlayers < 3)
        {
            numberOfPlayersErrorText.SetActive(true);
            return;
        }
        
        enterNumberOfPlayersPanel.SetActive(false);
        numberOfPlayersErrorText.SetActive(false);
        _numberOfMafia = Mathf.RoundToInt((float)(_numberOfPlayers * 28.0 / 100.0));
        _numberOfCitizens = _numberOfPlayers - _numberOfMafia;
        mafiaAmountText.text = _numberOfMafia.ToString();
        citizensAmountText.text = _numberOfCitizens.ToString();
        showRolesDistributionPanel.SetActive(true);*/
    }

    private void OnEnterNumberOfPlayersBackButtonClick()
    {
        InvokeOpenPreviousPanel();
        /*enterNumberOfPlayersPanel.SetActive(false);
        startMenuPanel.SetActive(true);*/
    }

    protected override void UnsubscribeEvents()
    {
        confirmButton.onClick.RemoveListener(OnConfirmNumberOfPlayersButtonClick);
        backButton.onClick.RemoveListener(OnEnterNumberOfPlayersBackButtonClick);
    }
}
