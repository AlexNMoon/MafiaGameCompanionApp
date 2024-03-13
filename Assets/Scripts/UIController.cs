using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class UIController : MonoBehaviour
{
    [SerializeField] private StartMenuPanelController startMenuPanelController;
    [SerializeField] private GameRulesPanelController gameRulesPanelController;
    [SerializeField] private EnterNumberOfPlayersPanelController enterNumberOfPlayersPanelController;
    [SerializeField] private RolesDistributionPanelController rolesDistributionPanelController;
    [SerializeField] private CreateTeamInstructionPanelController createTeamInstructionPanelController;
    [SerializeField] private PlayerInstructionPanelController playerInstructionPanelController;
    [SerializeField] private EnterPlayerNamePanelController enterPlayerNamePanelController;
    [SerializeField] private ShowRolePanelController showRolePanelController;
    [SerializeField] private EndOfDistributionInstructionPanelController endOfDistributionInstructionPanelController;
    [SerializeField] private GamePanelController gamePanelController;

    private PanelController _currentPanel;
    private int _numberOfPlayers;
    private int _numberOfMafia;
    private int _numberOfCitizens;
    private int _currentPlayerAssigment;
    private List<RolesEnum> _availableRoles;
    private Dictionary<string, RolesEnum> _rolesDistribution;
    private Color _mafiaColor = Color.red;
    private Color _citizenColor = Color.green;

    private void StartRolesDistribution()
    {
        _rolesDistribution = new Dictionary<string, RolesEnum>();
        CreateListOfAvailableRoles();
        _currentPlayerAssigment = 1;
        ShowPlayerInstructionPanel();
    }

    private void CreateListOfAvailableRoles()
    {
        _availableRoles = new List<RolesEnum>();

        for (int i = 0; i < _numberOfMafia; i++)
        {
            _availableRoles.Add(RolesEnum.Mafia);
        }

        for (int i = 0; i < _numberOfCitizens; i++)
        {
            _availableRoles.Add(RolesEnum.Citizen);
        }
    }

    private void ShowPlayerInstructionPanel()
    {
        /*playerInstructionText.text = String.Format(_playerInstruction, _currentPlayerAssigment);
        playerInstructionPanel.SetActive(true);*/
    }

    /*private void OnGetRoleButtonClick()
    {
        if(nameInputField.text == "") return;
        
        if(_rolesDistribution.ContainsKey(nameInputField.text))
        {
            nameErrorText.SetActive(true);
            return;
        }
        
        enterPlayerNamePanel.SetActive(false);
        nameErrorText.SetActive(false);
        int i = Random.Range(0, _availableRoles.Count);
        RolesEnum role = _availableRoles[i];
        _availableRoles.RemoveAt(i);
        _rolesDistribution.Add(nameInputField.text, role);
        roleText.text = String.Format(_roleString, role);
        roleImage.color = GetRolesColor(role);
        nameInputField.text = "";
        showRolePanel.SetActive(true);
    }

    private Color GetRolesColor(RolesEnum role)
    {
        switch (role)
        {
            case RolesEnum.Mafia:
                return _mafiaColor;
            case RolesEnum.Citizen:
                return _citizenColor;
        }
        
        return Color.gray;
    }*/

    private void OnPlayerEliminated(RolesEnum playerRole)
    {
        switch (playerRole)
        {
            case RolesEnum.Mafia:
                _numberOfMafia--;
                break;
            case RolesEnum.Citizen:
                _numberOfCitizens--;
                break;
        }

        if (_numberOfMafia == 0)
        {
            /*endGameImage.color = _citizenColor;
            endGameText.text = "Citizens won!";
            endGamePanel.SetActive(true);*/
        }
        else if (_numberOfCitizens <= _numberOfMafia)
        {
            /*endGameImage.color = _mafiaColor;
            endGameText.text = "Mafia won!";
            endGamePanel.SetActive(true);*/
        }
    }

    private void Awake()
    {
        startMenuPanelController.StartGame += StartGame;
        startMenuPanelController.OpenGameRules += OpenGameRules;
        gameRulesPanelController.OpenPreviousPanel += OpenPreviousPanel;
        enterNumberOfPlayersPanelController.OpenNextPanel += OpenNextPanel;
        enterNumberOfPlayersPanelController.OpenPreviousPanel += OpenPreviousPanel;
        rolesDistributionPanelController.OpenNextPanel += OpenNextPanel;
        rolesDistributionPanelController.OpenPreviousPanel += OpenPreviousPanel;
        createTeamInstructionPanelController.OpenNextPanel += OpenNextPanel;
        playerInstructionPanelController.OpenNextPanel += OpenNextPanel;
        enterPlayerNamePanelController.OpenNextPanel += OpenNextPanel;
        showRolePanelController.OpenNextPanel += OpenNextPanel;
        endOfDistributionInstructionPanelController.OpenNextPanel += OpenNextPanel;
    }

    private void OpenGameRules()
    {
        ChangePanel(gameRulesPanelController);
    }

    private void ChangePanel(PanelController nextPanel)
    {
        _currentPanel.ClosePanel();
        _currentPanel = nextPanel;
        _currentPanel.OpenPanel();
    }

    private void StartGame()
    {
        ChangePanel(enterNumberOfPlayersPanelController);
    }

    private void Start()
    {
        _currentPanel = startMenuPanelController;
        _currentPanel.OpenPanel();
        OpenNextPanel();
    }

    private void OpenNextPanel()
    {
        switch (_currentPanel)
        {
            case EnterNumberOfPlayersPanelController:
                ChangePanel(rolesDistributionPanelController);
                break;
            case RolesDistributionPanelController:
                ChangePanel(createTeamInstructionPanelController);
                break;
            case CreateTeamInstructionPanelController:
                ChangePanel(playerInstructionPanelController);
                break;
            case PlayerInstructionPanelController:
                ChangePanel(enterPlayerNamePanelController);
                break;
            case EnterPlayerNamePanelController:
                ChangePanel(showRolePanelController);
                break;
            case ShowRolePanelController:
                ChangePanel(endOfDistributionInstructionPanelController);
                break;
            case EndOfDistributionInstructionPanelController:
                ChangePanel(gamePanelController);
                break;
            case GamePanelController:
                ChangePanel(startMenuPanelController);
                break;
        }
    }

    private void OpenPreviousPanel()
    {
        switch (_currentPanel)
        {
            case GameRulesPanelController:
                ChangePanel(startMenuPanelController);
                break;
            case EnterNumberOfPlayersPanelController:
                ChangePanel(startMenuPanelController);
                break;
            case RolesDistributionPanelController:
                ChangePanel(enterPlayerNamePanelController);
                break;
            case CreateTeamInstructionPanelController:
                ChangePanel(rolesDistributionPanelController);
                break;
            case GamePanelController:
                ChangePanel(startMenuPanelController);
                break;
        }
    }
}