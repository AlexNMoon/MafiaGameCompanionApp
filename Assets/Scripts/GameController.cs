using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    public event Action OpenMainMenu;
    
    [SerializeField] private EnterNumberOfPlayersPanelController enterNumberOfPlayersPanelController;
    [SerializeField] private RolesDistributionPanelController rolesDistributionPanelController;
    [SerializeField] private CreateTeamInstructionPanelController createTeamInstructionPanelController;
    [SerializeField] private RolesAssigmentController rolesAssigmentController;
    [SerializeField] private EndOfDistributionInstructionPanelController endOfDistributionInstructionPanelController;
    [SerializeField] private GamePanelController gamePanelController;

    private PanelController _currentPanel;
    private int _numberOfPlayers;
    private int _numberOfMafia;
    private int _numberOfCitizens;
    private Dictionary<string, RolesEnum> _rolesDistribution;

    public void StartNewGame()
    {
        _currentPanel = enterNumberOfPlayersPanelController;
        enterNumberOfPlayersPanelController.OpenPanel();
    }

    private void Awake()
    {
        enterNumberOfPlayersPanelController.ContinueButtonClick += OpenNextPanel;
        enterNumberOfPlayersPanelController.BackButtonClick += OpenPreviousPanel;
        rolesDistributionPanelController.ContinueButtonClick += OpenNextPanel;
        rolesDistributionPanelController.BackButtonClick += OpenPreviousPanel;
        createTeamInstructionPanelController.ContinueButtonClick += OpenNextPanel;
        createTeamInstructionPanelController.BackButtonClick += OpenPreviousPanel;
        rolesAssigmentController.DistributionFinished += OnDistributionFinished;
        endOfDistributionInstructionPanelController.ContinueButtonClick += OpenNextPanel;
        gamePanelController.PlayerEliminated += OnPlayerEliminated;
    }

    private void ChangePanel(PanelController nextPanel)
    {
        _currentPanel.ClosePanel();
        _currentPanel = nextPanel;
        _currentPanel.OpenPanel();
    }

    private void OpenNextPanel()
    {
        switch (_currentPanel)
        {
            case EnterNumberOfPlayersPanelController:
                CalculateRolesDistribution();
                break;
            case RolesDistributionPanelController:
                ChangePanel(createTeamInstructionPanelController);
                break;
            case CreateTeamInstructionPanelController:
                createTeamInstructionPanelController.ClosePanel();
                rolesAssigmentController.StartRolesDistribution(CreateListOfAvailableRoles());
                break;
            case EndOfDistributionInstructionPanelController:
                ShowGamePanel();
                break;
        }
    }

    private void CalculateRolesDistribution()
    {
        _numberOfPlayers = enterNumberOfPlayersPanelController.GetNumberOfPlayer();
        
        if (_numberOfPlayers < 3)
        {
            enterNumberOfPlayersPanelController.ShowError();
            return;
        }
    
        _numberOfMafia = Mathf.RoundToInt((float)(_numberOfPlayers * 28.0 / 100.0));
        _numberOfCitizens = _numberOfPlayers - _numberOfMafia;
        rolesDistributionPanelController.SetUpRoles(_numberOfMafia, _numberOfCitizens);
        ChangePanel(rolesDistributionPanelController);
    }

    private List<RolesEnum> CreateListOfAvailableRoles()
    {
        List<RolesEnum> availableRoles = new List<RolesEnum>();

        for (int i = 0; i < _numberOfMafia; i++)
        {
            availableRoles.Add(RolesEnum.Mafia);
        }

        for (int i = 0; i < _numberOfCitizens; i++)
        {
            availableRoles.Add(RolesEnum.Citizen);
        }

        return availableRoles;
    }

    private void OnDistributionFinished(Dictionary<string, RolesEnum> distribution)
    {
        _rolesDistribution = distribution;
        _currentPanel = endOfDistributionInstructionPanelController;
        endOfDistributionInstructionPanelController.OpenPanel();
    }

    private void ShowGamePanel()
    {
        gamePanelController.SetUpPlayers(_rolesDistribution);
        ChangePanel(gamePanelController);
    }

    private void OpenPreviousPanel()
    {
        switch (_currentPanel)
        {
            case EnterNumberOfPlayersPanelController:
                enterNumberOfPlayersPanelController.ClosePanel();
                OpenMainMenu?.Invoke();
                break;
            case RolesDistributionPanelController:
                ChangePanel(enterNumberOfPlayersPanelController);
                break;
            case CreateTeamInstructionPanelController:
                ChangePanel(rolesDistributionPanelController);
                break;
        }
    }

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
            gamePanelController.ShowGameEnd(RolesEnum.Citizen);
        }
        else if (_numberOfCitizens <= _numberOfMafia)
        {
            gamePanelController.ShowGameEnd(RolesEnum.Mafia);
        }
    }
}