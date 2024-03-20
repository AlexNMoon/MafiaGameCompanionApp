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

    private int _numberOfPlayers;
    private int _numberOfMafia;
    private int _numberOfCitizens;
    
    private Dictionary<string, RolesEnum> _rolesAssigment;

    public void StartNewGame()
    {
        enterNumberOfPlayersPanelController.OpenPanel();
    }

    private void Awake()
    {
        enterNumberOfPlayersPanelController.ContinueButtonClick += CalculateRolesDistribution;
        enterNumberOfPlayersPanelController.BackButtonClick += GoBackToMainMenu;
        
        rolesDistributionPanelController.ContinueButtonClick += ShowCreateTeamInstructions;
        rolesDistributionPanelController.BackButtonClick += GoBackToEnterNumberOfPlayers;
        
        createTeamInstructionPanelController.ContinueButtonClick += StartRolesAssigment;
        createTeamInstructionPanelController.BackButtonClick += GoBackToRolesDistribution;
        
        rolesAssigmentController.AssigmentFinished += OnAssigmentFinished;
        
        endOfDistributionInstructionPanelController.ContinueButtonClick += ShowGamePanel;
        
        gamePanelController.PlayerEliminated += OnPlayerEliminated;
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
        enterNumberOfPlayersPanelController.ClosePanel();
        rolesDistributionPanelController.OpenPanel();
    }

    private void GoBackToMainMenu()
    {
        enterNumberOfPlayersPanelController.ClosePanel();
        OpenMainMenu?.Invoke();
    }

    private void ShowCreateTeamInstructions()
    {
        rolesDistributionPanelController.ClosePanel();
        createTeamInstructionPanelController.OpenPanel();
    }

    private void GoBackToEnterNumberOfPlayers()
    {
        rolesDistributionPanelController.ClosePanel();
        enterNumberOfPlayersPanelController.OpenPanel();
    }

    private void StartRolesAssigment()
    {
        createTeamInstructionPanelController.ClosePanel();
        rolesAssigmentController.StartRolesAssigment(CreateListOfAvailableRoles());
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

    private void GoBackToRolesDistribution()
    {
        createTeamInstructionPanelController.ClosePanel();
        rolesDistributionPanelController.OpenPanel();
    }

    private void OnAssigmentFinished(Dictionary<string, RolesEnum> distribution)
    {
        _rolesAssigment = distribution;
        endOfDistributionInstructionPanelController.OpenPanel();
    }

    private void ShowGamePanel()
    {
        gamePanelController.SetUpPlayers(_rolesAssigment);
        endOfDistributionInstructionPanelController.ClosePanel();
        gamePanelController.OpenPanel();
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