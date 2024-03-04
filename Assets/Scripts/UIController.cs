using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Button = UnityEngine.UI.Button;

public class UIController : MonoBehaviour
{
    //start menu elements
    [SerializeField] private GameObject startMenuPanel;
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button showRulesButton;
    
    //game rules elements
    [SerializeField] private GameObject gameRulesPanel;
    [SerializeField] private Button gameRulesBackButton;
    
    //enter number of players elements
    [SerializeField] private GameObject enterNumberOfPlayersPanel;
    [SerializeField] private TMP_InputField numberOfPlayersTMPInputField;
    [SerializeField] private Button confirmNumberOfPlayersButton;
    [SerializeField] private Button enterNumberOfPlayersBackButton;
    
    //show roles distribution elements
    [SerializeField] private GameObject showRolesDistributionPanel;
    [SerializeField] private TMP_Text mafiaAmountText;
    [SerializeField] private TMP_Text citizensAmountText;
    [SerializeField] private Button showRolesDistributionBackButton;
    [SerializeField] private Button showRolesDistributionContinueButton;
    
    //create team instruction elements
    [SerializeField] private GameObject createTeamInstructionPanel;
    [SerializeField] private Button createTeamInstructionBackButton;
    [SerializeField] private Button createTeamInstructionContinueButton;

    private int _numberOfPlayers;
    private int _numberOfMafia;
    private int _numberOfCitizens;

    private void Awake()
    {
        showRulesButton.onClick.AddListener(OnShowRulesButtonClick);
        gameRulesBackButton.onClick.AddListener(OnGameRulesBackButtonClick);
        newGameButton.onClick.AddListener(OnStartGameButtonClick);
        confirmNumberOfPlayersButton.onClick.AddListener(OnConfirmNumberOfPlayersButtonClick);
        enterNumberOfPlayersBackButton.onClick.AddListener(OnEnterNumberOfPlayersBackButtonClick);
        showRolesDistributionBackButton.onClick.AddListener(OnShowRolesDistributionBackButtonClick);
        showRolesDistributionContinueButton.onClick.AddListener(OnShowRolesDistributionContinueButtonClick);
        createTeamInstructionBackButton.onClick.AddListener(OnCreateTeamInstructionBackButtonClick);
        createTeamInstructionContinueButton.onClick.AddListener(OnCreateTeamInstructionContinueButtonClick);
    }

    private void OnShowRulesButtonClick()
    {
        startMenuPanel.SetActive(false);
        gameRulesPanel.SetActive(true);
    }

    private void OnGameRulesBackButtonClick()
    {
        gameRulesPanel.SetActive(false);
        startMenuPanel.SetActive(true);
    }

    private void OnStartGameButtonClick()
    {
        startMenuPanel.SetActive(false);
        enterNumberOfPlayersPanel.SetActive(true);
    }

    private void OnConfirmNumberOfPlayersButtonClick()
    {
        if (numberOfPlayersTMPInputField.text == "") return;
        
        _numberOfPlayers =  Convert.ToInt32(numberOfPlayersTMPInputField.text);
            
        if (_numberOfPlayers >= 3)
        {
            enterNumberOfPlayersPanel.SetActive(false);
            _numberOfMafia = Mathf.RoundToInt((float)(_numberOfPlayers * 28.0 / 100.0));
            _numberOfCitizens = _numberOfPlayers - _numberOfMafia;
            mafiaAmountText.text = _numberOfMafia.ToString();
            citizensAmountText.text = _numberOfCitizens.ToString();
            showRolesDistributionPanel.SetActive(true);
        }
    }

    private void OnEnterNumberOfPlayersBackButtonClick()
    {
        enterNumberOfPlayersPanel.SetActive(false);
        startMenuPanel.SetActive(true);
    }

    private void OnShowRolesDistributionBackButtonClick()
    {
        showRolesDistributionPanel.SetActive(false);
        enterNumberOfPlayersPanel.SetActive(true);
    }

    private void OnShowRolesDistributionContinueButtonClick()
    {
        showRolesDistributionPanel.SetActive(false);
        createTeamInstructionPanel.SetActive(true);
    }

    private void OnCreateTeamInstructionBackButtonClick()
    {
        createTeamInstructionPanel.SetActive(false);
        showRolesDistributionPanel.SetActive(true);
    }

    private void OnCreateTeamInstructionContinueButtonClick()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
